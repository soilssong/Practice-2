using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
  
    [SerializeField] SO_Item_List itemsList = null;

    private Dictionary<int, ItemDetails> itemListDictionary;



   

    protected override void Awake()
    {
        base.Awake();
        CreateDictionary();
    }

    public void CreateDictionary()
    {
        itemListDictionary = new Dictionary<int, ItemDetails>();
        foreach (ItemDetails item in itemsList.itemDetails)
        {
            itemListDictionary.Add(item.ItemCode, item);
        }
    }

    public ItemDetails GetItemDetails(int itemCode)
    {
        ItemDetails itemdetails;

        if (itemListDictionary.TryGetValue(itemCode,out itemdetails))
        {
            return itemdetails;
        }
        else
        {
            return null;
        }
    }












}
