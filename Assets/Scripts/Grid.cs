using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    EMPTY,
    NORMAL,
    COUNT
}

public class Grid : MonoBehaviour
{
    private Element[,] _element;
    public ElementPrefab[] _elementPrefab;

    [SerializeField] private int _xDim;
    [SerializeField] private int _yDim;
    [SerializeField] private float _fillTime;

    [SerializeField] private GameObject _prefabBackground;

    private Dictionary<ElementType, GameObject> _elementPrefabDictionary;

    private void Start()
    {
        _elementPrefabDictionary = new Dictionary<ElementType, GameObject>();
        for (int i = 0; i < _elementPrefab.Length; i++)
        {
            if (!_elementPrefabDictionary.ContainsKey(_elementPrefab[i]._type))
            {
                _elementPrefabDictionary.Add(_elementPrefab[i]._type, _elementPrefab[i]._prefab);
            }
        }

        for (int x = 0; x < _xDim; x++)
        {
            for (int y = 0; y < _yDim; y++)
            {
                GameObject background = (GameObject)Instantiate(_prefabBackground, GetWorldPosition(x, y), Quaternion.identity);
                background.transform.parent = transform;
            }
        }

        _element = new Element[_xDim, _yDim];
        for (int x = 0; x < _xDim; x++)
        {
            for (int y = 0; y < _yDim; y++)
            {
                SpawnNewPiece(x, y, ElementType.EMPTY);
            }
        }
        StartCoroutine(Fill());
    }
    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(transform.position.x - _xDim / 2.0f + x, transform.position.y + _yDim / 2.0f - y);
    }

    public Element SpawnNewPiece(int x, int y, ElementType type)
    {
        GameObject newElement = (GameObject)Instantiate(_elementPrefabDictionary[type], GetWorldPosition(x, y), Quaternion.identity);
        newElement.transform.parent = transform;

        _element[x, y] = newElement.GetComponent<Element>();
        _element[x, y].Init(x, y, this, type);

        return _element[x, y];
    }

    private IEnumerator Fill()
    {
        while (FillStep())
        {
            yield return new WaitForSeconds(_fillTime);
        }
    }

    private bool FillStep()
    {
        bool movedElement = false;

        for (int y = _yDim - 2; y >= 0; y--)
        {
            for (int x = 0; x < _xDim; x++)
            {
                Element filledElement = _element[x, y];

                if (filledElement.IsMovable())
                {
                    Element elementBelow = _element[x, y + 1];

                    if (elementBelow.Type == ElementType.EMPTY)
                    {
                        filledElement.MovableComponent.Move(x, y + 1, _fillTime);
                        _element[x, y + 1] = filledElement;
                        SpawnNewPiece(x, y, ElementType.EMPTY);
                        movedElement = true;
                    }
                }
            }
        }
        for (int x = 0; x < _xDim; x++)
        {
            Element elementBelow = _element[x, 0];
            if (elementBelow.Type == ElementType.EMPTY)
            {
                GameObject newElement = (GameObject)Instantiate(_elementPrefabDictionary[ElementType.NORMAL], GetWorldPosition(x, -1), Quaternion.identity);
                newElement.transform.parent = transform;

                _element[x, 0] = newElement.GetComponent<Element>();
                _element[x, 0].Init(x, -1, this, ElementType.NORMAL);
                _element[x, 0].MovableComponent.Move(x, 0, _fillTime);
                _element[x, 0].ColorComponent.SetColor((ColorElement.ColorType)Random.Range(0, _element[x, 0].ColorComponent.NumColor));
                movedElement = true;
            }
        }
        return movedElement;
    }
}

[System.Serializable]
public struct ElementPrefab
{
    [SerializeField] internal ElementType _type;
    [SerializeField] internal GameObject _prefab;
}
