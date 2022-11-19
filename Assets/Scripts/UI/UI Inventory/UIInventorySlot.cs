using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class UIInventorySlot : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    private Camera mainCamera;
    private Transform parentItem;
    private GameObject draggedItem;
    public Image inventorySlotHighLight;

    public Image inventorySlotImage;
    public TextMeshProUGUI textMeshProGui;
    [HideInInspector] public ItemDetails itemDetails;
    [SerializeField] private UIInventoryBar inventoryBar = null;

    [HideInInspector] public int itemQuantity;
    [SerializeField] private GameObject itemPrefab = null;

    private void Start()
    {
        mainCamera = Camera.main;
        parentItem = GameObject.FindGameObjectWithTag(Tags.ItemsParentTransform).transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemDetails != null)
        {
            Player.Instance.DisablePlayerInputAndResetMovement();

            draggedItem = Instantiate(inventoryBar.InventoryBarDraggedItem, inventoryBar.transform);

            Image draggedItemImage = draggedItem.GetComponentInChildren<Image>();
            draggedItemImage.sprite = inventorySlotImage.sprite;
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedItem !=null)
        {
            draggedItem.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            Destroy(draggedItem);

            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<UIInventorySlot>() != null)
            {

            }
            else
            {
                DropSelectedItemAtMousePosition();
            }
        }
        Player.Instance.EnablePlayerInput();
    }

    private void DropSelectedItemAtMousePosition()
    {
        if (itemDetails != null)
        {
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));

            GameObject itemGameObject = Instantiate(itemPrefab, worldPosition, Quaternion.identity, parentItem);
            Item item = itemGameObject.GetComponent<Item>();
            item.ItemCode = itemDetails.ItemCode;


            InventoryManager.Instance.RemoveItem(InventoryLocation.player, item.ItemCode);
        }
    }
}


