using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class Item 
{
    public enum ItemType { NONE, Portion, Sword, Shield, Staff, Dagger };

    public ItemType itemType = ItemType.NONE;
    public string name;
    public Vector3 Position;
    public int Gold;

    public Item()
    { }
    public Item(string pName , ItemType pItemType, Vector3 pPosition, int pGold)
    {
        name = pName;
        itemType = pItemType;
        Position = pPosition;
        Gold = pGold;
    }

}

public class ItemDatabase
{
    public List<Item> list_DItem = new List<Item>();


    Item Portion = new Item("Portion_Hp", Item.ItemType.Portion, Vector3.zero,50);
    Item Sword = new Item("Sword1", Item.ItemType.Sword, Vector3.zero,500);

    public void IistAdd()
    {
        list_DItem.Add(Portion);
        list_DItem.Add(Sword);
    }



}

