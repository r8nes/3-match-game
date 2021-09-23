using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ElementType
{
    NORMAL,
    COUNT
}

[System.Serializable]
public struct ElementPrefab
{
    [SerializeField] internal ElementType _type;
    [SerializeField] internal GameObject _prefab;
}

public class Grid : MonoBehaviour
{
    [SerializeField] private float _xDim;
    [SerializeField] private float _yDim;

    public  ElementPrefab[] _elementPrefabs;

    private Dictionary<ElementType, GameObject> _elementPrefabDictionary;

    private void Start()
    {
        _elementPrefabDictionary = new Dictionary<ElementType, GameObject>();
        for (int i = 0; i < _elementPrefabs.Length; i++)
        {
            if (!_elementPrefabDictionary.ContainsKey(_elementPrefabs[i]._type))
            {

            }
        }
    }
}
