using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{

    [SerializeField] SO_Item_List itemsList = null;

    private Dictionary<int, ItemDetails> itemListDictionary;

    public List<InventoryItem>[] InventoryItemsLists;

    public int[] inventoryListsCapacities;

    protected override void Awake()
    {
        base.Awake();
        CreateDictionary();
        CreateInventoryLists();
    }

    
    public void CreateDictionary()
    {
        itemListDictionary = new Dictionary<int, ItemDetails>();
        foreach (ItemDetails item in itemsList.itemDetails)
        {
            itemListDictionary.Add(item.ItemCode, item);
        }
    }

    public void CreateInventoryLists()
    {
        InventoryItemsLists = new List<InventoryItem>[(int)InventoryLocation.count];

        for (int i = 0; i < (int)InventoryLocation.count; i++)
        {
            InventoryItemsLists[i] = new List<InventoryItem>();
        }

        inventoryListsCapacities = new int[(int)InventoryLocation.count];
        inventoryListsCapacities[(int)InventoryLocation.player] = 10;
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
