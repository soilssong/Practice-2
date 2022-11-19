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

    /*Basically fetching the Item informations from SO_List that we created, and allocate them to the
    Dictionary declared above*/
    public void CreateDictionary()
    {
        itemListDictionary = new Dictionary<int, ItemDetails>();
        foreach (ItemDetails item in itemsList.itemDetails)
        {
            itemListDictionary.Add(item.ItemCode, item);
        }
    }

    /* Just because there is a 2 different types of inventory (player and chest) i created 2 different inventory list
     and we are keeping them in array.There is a another list for keeping the info about inventory capacities.
    And with the method beneath, we are implementing this arrays.*/
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

    /*This is the overloaded method that doing extra work for delete the object that we collected and holding reference to other addItem method
     which is doing real "adding item" thing*/
    public void addItem(InventoryLocation inventorylocation,Item item,GameObject gameObjectToDelete)
    {
        addItem(inventorylocation, item);
        Destroy(gameObjectToDelete);
    }

    /*This Method doing all thing about Adding item to inventory.Checking if there is already the same item have been placed in inventory
     and according that processing through 2 different method.(They are overloaded also)*/
    public void addItem(InventoryLocation inventorylocation, Item item)
    {
        int itemCode = item.ItemCode;
        List<InventoryItem> inventoryList = InventoryItemsLists[(int)inventorylocation];

        int itemPosition = FindItemInInventory(inventorylocation, itemCode);

        if (itemPosition !=-1)
        {
            AddItemAtPosition(inventoryList, itemCode, itemPosition );
        }

        else
        {
            AddItemAtPosition(inventoryList, itemCode);
        }
        EventHandler.CallInventoryUpdatedEvent(inventorylocation, InventoryItemsLists[(int)inventorylocation]);
            
      }

    private void AddItemAtPosition(List<InventoryItem> inventoryList ,int itemCode)
    {
        InventoryItem inventoryItem = new InventoryItem();

        inventoryItem.ItemCode = itemCode;
        inventoryItem.ItemQuantity = 1;
        inventoryList.Add(inventoryItem);
        DebugPrintInventoryList(inventoryList);
    }
    private void AddItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();
        int quantity = inventoryList[position].ItemQuantity + 1;
        inventoryItem.ItemQuantity = quantity;
        inventoryItem.ItemCode = itemCode;
        inventoryList[position] = inventoryItem;
        Debug.ClearDeveloperConsole();
        DebugPrintInventoryList(inventoryList);
    }

    public int FindItemInInventory(InventoryLocation inventorylocation,int itemCode)
    {
        List<InventoryItem> inventoryList = InventoryItemsLists[(int)inventorylocation];

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].ItemCode == itemCode)
            {
                return i;
            }
        }
        return -1;
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

    public void RemoveItem(InventoryLocation inventorylocation, int Itemcode)
    {
        List<InventoryItem> inventoryList = InventoryItemsLists[(int)inventorylocation];

        int ItemPosition = FindItemInInventory(inventorylocation,Itemcode);

        if (ItemPosition != -1)
        {
            RemoveItemAtPosition(inventoryList, Itemcode, ItemPosition);
        }

        EventHandler.CallInventoryUpdatedEvent(inventorylocation, InventoryItemsLists[(int)inventorylocation]);
    }

    public void RemoveItemAtPosition(List<InventoryItem> inventoryList, int ItemCode, int ItemPosition)
    {
        InventoryItem inventoryItem = new InventoryItem();
        int quantity = inventoryList[ItemPosition].ItemQuantity - 1;
        if (quantity>0)
        {
            inventoryItem.ItemQuantity = quantity;
            inventoryItem.ItemCode = ItemCode;
            inventoryList[ItemPosition] = inventoryItem;

        }
        else
        {
            inventoryList.RemoveAt(ItemPosition);
        }
    }

    public void DebugPrintInventoryList(List<InventoryItem> inventoryList)
    {
        foreach (InventoryItem inventoryItem in inventoryList)
        {
            Debug.Log("Item Description : " + InventoryManager.Instance.GetItemDetails(inventoryItem.ItemCode).ItemDescription +
                "Item Quantity : " + inventoryItem.ItemQuantity);
        }
        Debug.Log("**************************");
    }












}
