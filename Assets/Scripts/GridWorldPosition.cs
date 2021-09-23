using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWorldPosition : MonoBehaviour
{
    private Vector2 _dimensions;
    private Grid _grid;

    private void Start()
    {
        _grid = GetComponent<Grid>();
        _dimensions = _grid.GetDimensions();
    }

    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(_grid.transform.position.x - _dimensions.x / 2.0f + x, _grid.transform.position.y + _dimensions.y / 2.0f - y);
    }
}
