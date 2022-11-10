using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    [SerializeField] SO_Item_List itemList = null;
    private Dictionary<int, ItemDetails> itemDetailsDictionary;


    void Start()
    {
        CreateItemDetailsDictionary();
    }

    public void CreateItemDetailsDictionary()
    {
        itemDetailsDictionary = new Dictionary<int, ItemDetails>();

        foreach (ItemDetails itemDetails in itemList.itemDetails)
        {
            itemDetailsDictionary.Add(itemDetails.ItemCode, itemDetails);
        }
    }

    public ItemDetails GetItemDetails(int itemCode)
    {
        ItemDetails itemdetails;

        if (itemDetailsDictionary.TryGetValue(itemCode, out itemdetails))
        {
            return itemdetails;
        }
        else
        {
            return null;
        }
    }





}
