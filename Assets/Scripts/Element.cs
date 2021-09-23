using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    private int _x;
    private int _y;

    public int X 
    {
        get { return _x; }
    }
    public int Y
    {
        get { return _y; }
    }
    
    private ElementType _type;

    public ElementType Type {
        get { return _type; }
    }

    private Grid _grid;

    public Grid GridRef {
        get { return _grid; }
    }
    public void Init(int x, int y, Grid grid, ElementType type) {
        _x = x;
        _y = y;
        _grid = grid;
        _type = type;
    }
}
