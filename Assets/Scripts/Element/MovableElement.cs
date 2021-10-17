using System;
using System.Collections;
using UnityEngine;

public class MovableElement : MonoBehaviour
{
    private Element _element;
    private IEnumerator _moveCoroutine;

    private void Awake()
    {
        _element = GetComponent<Element>();
    }

    public void Move(int newX, int newY, float time)
    {
        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }
            _moveCoroutine = MoveCoroutine(newX, newY, time);
            StartCoroutine(_moveCoroutine);
    }

    private IEnumerator MoveCoroutine(int newX, int newY, float time)
    {
        _element.X = newX;
        _element.Y = newY;

        Vector3 startPos = transform.position;
        Vector3 endPos = _element.GridRef.GetWorldPosition(newX, newY);

        for (float t = 0; t <= 1 * time; t += Time.deltaTime)
        {
            _element.transform.position = Vector3.Lerp(startPos, endPos, t / time);
            yield return 0;
        }
        _element.transform.position = endPos;
    }
}
