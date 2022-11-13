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

    public void addItem(InventoryLocation inventorylocation,Item item,GameObject gameObjectToDelete)
    {
        addItem(inventorylocation, item);
        Destroy(gameObjectToDelete);
    }

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
