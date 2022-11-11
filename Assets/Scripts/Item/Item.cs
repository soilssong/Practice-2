using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int itemCode;

    private SpriteRenderer itemSpriteRenderer;

    public int ItemCode { get { return itemCode; } set { itemCode = value; } }

    
    void Awake()
    {
        itemSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }


    void Start()
    {
        if (ItemCode != 0)
        {
            Init(ItemCode);
        }
    }

    public void Init(int itemCodeParam)
    {
        if (itemCodeParam != 0)
        {
            itemCode = itemCodeParam;
            ItemDetails itemdetails = InventoryManager.Instance.GetItemDetails(itemCode);

            itemSpriteRenderer.sprite = itemdetails.ItemSprite;

            if (itemdetails.isBreakable == true)
            {
                gameObject.AddComponent<ItemWubble>();
            }
        }
    }
}
