using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorElement : MonoBehaviour
{
    public enum ColorType
    {
        BLACK,
        BROWN,
        GREY,
        ORANGE,
        WHITE,
        ANY,
        COUNT
    }

    private SpriteRenderer _sprite;
    private Dictionary<ColorType, Sprite> _colorSpriteDictionary;

    [SerializeField] private ColorSprite[] _colorSprites;


    private ColorType color;
    
    public ColorType Color {
        get { return color; }
        set { SetColor(value); }
    }

    public int NumColor {
        get { return _colorSprites.Length; }
    }


    private void Awake()
    {
        _sprite = transform.Find("Element").GetComponent<SpriteRenderer>();
        _colorSpriteDictionary = new Dictionary<ColorType, Sprite>();

        for (int i = 0; i < _colorSprites.Length; i++)
        {
            if (!_colorSpriteDictionary.ContainsKey(_colorSprites[i].color))
            {
                _colorSpriteDictionary.Add(_colorSprites[i].color, _colorSprites[i].sprite);
            }
        }
    }

    public void SetColor(ColorType newColor)
    {
        color = newColor;

        if (_colorSpriteDictionary.ContainsKey(newColor))
        {
            _sprite.sprite = _colorSpriteDictionary[newColor];
        }
    }

    [System.Serializable]
    public struct ColorSprite
    {
        public ColorType color;
        public Sprite sprite;
    }
}
