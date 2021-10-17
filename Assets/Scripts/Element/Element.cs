using UnityEngine;

public class Element : MonoBehaviour
{
    private int _x;
    private int _y;

    public int X
    {
        get { return _x; }
        set {
            if (IsMovable())
            {
                _x = value;
            }
        }
    }
    public int Y
    {
        get { return _y; }
        set
        {
            if (IsMovable())
            {
                _y = value;
            }
        }
    }
    
    private ElementType _type;
    public ElementType Type {
        get { return _type; }
    }

    private Grid _grid;
    public Grid GridRef {
        get { return _grid; }
    }

    private MovableElement _movableComponent;
    public MovableElement MovableComponent
    {
        get { return _movableComponent; }
    }

    private ColorElement _colorComponent;
    public ColorElement ColorComponent
    {
        get { return _colorComponent; }
    }

    private void Awake()
    {
        _movableComponent = GetComponent<MovableElement>();
        _colorComponent = GetComponent<ColorElement>();
    }
    public void Init(int x, int y, Grid grid, ElementType type) {
        _x = x;
        _y = y;
        _grid = grid;
        _type = type;
    }

    public bool IsMovable()
    {
        return _movableComponent != null;
    }
    public bool IsColored()
    {
        return _colorComponent != null;
    }
}
