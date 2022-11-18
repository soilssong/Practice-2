using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventoryBar : MonoBehaviour
{
    private RectTransform rectTransform;
    private bool _isInventoryBarPositionBottomRight = true;

    public bool isInventoryBarPositionBottomRight { get => _isInventoryBarPositionBottomRight; set => _isInventoryBarPositionBottomRight = value; }



    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        SwitchInventoryBarPosition();
    }

    private void SwitchInventoryBarPosition()
    {
        Vector3 playerViewPortPosition = Player.Instance.GetPlayerViewPortPosition();
        if (playerViewPortPosition.y > 0.3f && _isInventoryBarPositionBottomRight == false)
        {
            rectTransform.pivot = new Vector2(1.0f, 0f);
            rectTransform.anchorMin = new Vector2(1.0f, 0f);
            rectTransform.anchorMax = new Vector2(1.0f, 0f);
            rectTransform.anchoredPosition = new Vector2(1.0f, 0f);
            _isInventoryBarPositionBottomRight = true;
        }
        else if(playerViewPortPosition.y <= 0.3f && _isInventoryBarPositionBottomRight == true)
        {
            rectTransform.pivot = new Vector2(1f, 1f);
            rectTransform.anchorMin = new Vector2(1f, 1f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
            rectTransform.anchoredPosition = new Vector2(1f, 1f);
            _isInventoryBarPositionBottomRight = false;
        }
    }
}
