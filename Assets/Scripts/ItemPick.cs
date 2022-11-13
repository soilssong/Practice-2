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
            InventoryManager.Instance.addItem(InventoryLocation.player, item,other.gameObject);
        }
    }
}
