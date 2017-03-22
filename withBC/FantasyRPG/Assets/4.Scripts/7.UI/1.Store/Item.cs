using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class Item 
{
    public enum ItemType { NONE, Portion, Sword, Sword2, Sword3,Axe, Shield, Staff, Dagger };

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
    Item Sword2 = new Item("Sword2", Item.ItemType.Sword2, Vector3.zero, 1000);
    Item Sword3 = new Item("Sword3", Item.ItemType.Sword3, Vector3.zero, 2000);
    Item Dagger = new Item("Dagger", Item.ItemType.Dagger, Vector3.zero, 800);
    Item Axe = new Item("Axe", Item.ItemType.Axe, Vector3.zero, 1500);
    Item Staff = new Item("Staff", Item.ItemType.Staff, Vector3.zero, 2000);
    Item Shield = new Item("Shield", Item.ItemType.Shield, Vector3.zero, 1500);

    public void IistAdd()
    {
        list_DItem.Add(Portion);
        list_DItem.Add(Sword);
        list_DItem.Add(Sword2);
        list_DItem.Add(Sword3);
        list_DItem.Add(Dagger);
        list_DItem.Add(Axe);
        list_DItem.Add(Staff);
        list_DItem.Add(Shield);
    }



}

