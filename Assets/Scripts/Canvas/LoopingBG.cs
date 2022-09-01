using System;
using UnityEngine;
using UnityEngine.UI;

public class LoopingBG : MonoBehaviour
{
    public float speed;

    private RectTransform _rectTransform;
    private float xStartPos;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        xStartPos = _rectTransform.anchoredPosition.x;
    }

    private void Update()
    {
        _rectTransform.anchoredPosition += Vector2.left * speed * Time.deltaTime;

        if (_rectTransform.anchoredPosition.x < -1238)
        {
            _rectTransform.anchoredPosition = new Vector2(xStartPos, _rectTransform.anchoredPosition.y);
        }
    }
}