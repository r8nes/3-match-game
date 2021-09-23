using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    NORMAL,
    COUNT
}

public class Grid : MonoBehaviour
{
    [SerializeField] private int _xDim;
    [SerializeField] private int _yDim;

    [SerializeField] private GameObject _prefabBackground;
    [SerializeField] private GridWorldPosition _gridWorldPosition;

    public  ElementPrefab[] _elementPrefab;
    private Element[,] _element;

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
                GameObject background = (GameObject)Instantiate(_prefabBackground, _gridWorldPosition.GetWorldPosition(x,y), Quaternion.identity);
                background.transform.parent = transform;
            }
        }

        _element = new Element[_xDim, _yDim];
        for (int x = 0; x < _xDim; x++)
        {
            for (int y = 0; y < _yDim; y++)
            {
                GameObject newElement = (GameObject)Instantiate(_elementPrefabDictionary[ElementType.NORMAL], _gridWorldPosition.GetWorldPosition(x, y), Quaternion.identity);
                newElement.name = "Element(" + x + "," + y + ")";
                newElement.transform.parent = transform;

                _element[x, y] = newElement.GetComponent<Element>();
                _element[x, y].Init(x,y, this, ElementType.NORMAL);
            }
        }
    }

    public Vector2 GetDimensions() {
        return new Vector2(_xDim, _yDim);
    }
}

[System.Serializable]
public struct ElementPrefab
{
    [SerializeField] internal ElementType _type;
    [SerializeField] internal GameObject _prefab;
}
