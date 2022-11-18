using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIInventorySlot : MonoBehaviour
{
    public Image inventorySlotHighLight;

    public Image inventorySlotImage;
    public TextMeshProUGUI textMeshProGui;
    [HideInInspector] public ItemDetails itemDetails;
    [HideInInspector] public int itemQuantity;
}
