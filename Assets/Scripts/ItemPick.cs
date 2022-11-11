using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPick : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Item item = other.GetComponent<Item>();

        if (item!=null)
        {
            string ItemName = InventoryManager.Instance.GetItemName(item.ItemCode);
            Debug.Log(ItemName);
        }
    }
}
