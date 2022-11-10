using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="So_ITEM",menuName ="ScriptableObjects/Item/ItemList")]
public class SO_Item_List : ScriptableObject
{
    [SerializeField]
    public List<ItemDetails> itemDetails;
}
