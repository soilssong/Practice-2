using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIInventoryBar : MonoBehaviour
{

    [SerializeField] private Sprite blankSprite;
    [SerializeField] private UIInventorySlot[] uiInventorySlot = null;
    private RectTransform rectTransform;
    private bool _isInventoryBarPositionBottomRight = true;
    public bool isInventoryBarPositionBottomRight { get => _isInventoryBarPositionBottomRight; set => _isInventoryBarPositionBottomRight = value; }
 
    public GameObject InventoryBarDraggedItem;



    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        SwitchInventoryBarPosition();
    }


    private void OnEnable()
    {
        EventHandler.InventoryUpdatedEvent += InventoryUpdated;
    }


    private void OnDisable()
    {
        EventHandler.InventoryUpdatedEvent -= InventoryUpdated;

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
        else if (playerViewPortPosition.y <= 0.3f && _isInventoryBarPositionBottomRight == true)
        {
            rectTransform.pivot = new Vector2(1f, 1f);
            rectTransform.anchorMin = new Vector2(1f, 1f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
            rectTransform.anchoredPosition = new Vector2(1f, 1f);
            _isInventoryBarPositionBottomRight = false;
        }
    }


    private void InventoryUpdated(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        if (inventoryLocation == InventoryLocation.player)
        {
            ClearInventorySlots();
            if (uiInventorySlot.Length > 0 && inventoryList.Count > 0)
            {
                for (int i = 0; i < uiInventorySlot.Length; i++)
                {
                    if (i < inventoryList.Count)
                    {
                        int ItemCode = inventoryList[i].ItemCode;

                        ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(ItemCode);

                        if (itemDetails != null)
                        {
                            uiInventorySlot[i].inventorySlotImage.sprite = itemDetails.ItemSprite;
                            uiInventorySlot[i].textMeshProGui.text = inventoryList[i].ItemQuantity.ToString();
                            uiInventorySlot[i].itemDetails = itemDetails;
                            uiInventorySlot[i].itemQuantity = inventoryList[i].ItemQuantity;
                            SetHighlightedInventorySlot(i);
                        }
                    }
                    else { break; }
                }
            }
        }
    }

    private void ClearInventorySlots()
    {
        if (uiInventorySlot.Length >0)
        {
            for (int i = 0; i < uiInventorySlot.Length; i++)
            {
                uiInventorySlot[i].inventorySlotImage.sprite = blankSprite;
                uiInventorySlot[i].textMeshProGui.text = "";
                uiInventorySlot[i].itemDetails = null;
                uiInventorySlot[i].itemQuantity = 0;
                SetHighlightedInventorySlot(i);
            }
        }
    }


    public void ClearHighlightOnInventorySlots()
    {
        if (uiInventorySlot.Length>0)
        {
            for (int i = 0; i < uiInventorySlot.Length; i++)
            {
                if (uiInventorySlot[i].isSelected)
                {
                    uiInventorySlot[i].isSelected = false;
                    uiInventorySlot[i].inventorySlotHighLight.color = new Color(0f, 0f, 0f, 0f);

                    InventoryManager.Instance.ClearSelectedInventoryItem(InventoryLocation.player);
                }
            }
        }
    }

    public void SetHighlightedInventorySlot ()
    {
        if (uiInventorySlot.Length > 0)
        {
            for(int i = 0; i < uiInventorySlot.Length; i++)
            {
                SetHighlightedInventorySlot(i);
            }
        }
    }
    public void SetHighlightedInventorySlot(int itemPosition)
    {
        if (uiInventorySlot.Length > 0 && uiInventorySlot[itemPosition].itemDetails != null)
        {
            if (uiInventorySlot[itemPosition].isSelected)
            {
                uiInventorySlot[itemPosition].inventorySlotHighLight.color = new Color(1f, 0f, 0f, 0.3f);


                InventoryManager.Instance.SetSelectedInventoryItem(InventoryLocation.player, uiInventorySlot[itemPosition].itemDetails.ItemCode);
            }
        }
    }
}
