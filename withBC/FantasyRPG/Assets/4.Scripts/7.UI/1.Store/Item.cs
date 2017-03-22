using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;

public class Item 
{
    public enum ItemType { NONE, Portion, Sword, Sword2, Sword3,Axe, Shield, Staff, Staff2, Dagger,Dagger2 };
    public enum PeriotyWeapon { None,Main , Sub1, Sub2, Sub3};

    public PeriotyWeapon Perioty = PeriotyWeapon.None;

    public ItemType itemType = ItemType.NONE;
    public string name;
    public Vector3 Position;
    public int Gold;

    public Item()
    { }
    public Item(string pName , ItemType pItemType, PeriotyWeapon pPerioty,Vector3 pPosition, int pGold)
    {
        name = pName;
        itemType = pItemType;
        Perioty = pPerioty;
        Position = pPosition;
        Gold = pGold;
    }

}

public class ItemDatabase
{
    public List<Item> list_DItem = new List<Item>();
    public List<Item> list_WholeItem = new List<Item>();


    Item Portion = new Item("Portion_Hp", Item.ItemType.Portion,Item.PeriotyWeapon.Sub2, Vector3.zero,50);
    Item Sword = new Item("Sword1", Item.ItemType.Sword, Item.PeriotyWeapon.Main, Vector3.zero,500);
    Item Sword2 = new Item("Sword2", Item.ItemType.Sword2, Item.PeriotyWeapon.Main, Vector3.zero, 1000);
    Item Sword3 = new Item("Sword3", Item.ItemType.Sword3, Item.PeriotyWeapon.Main , Vector3.zero, 2000);
    Item Dagger = new Item("Dagger", Item.ItemType.Dagger, Item.PeriotyWeapon.Sub1, Vector3.zero, 800);
    Item Axe = new Item("Axe", Item.ItemType.Axe, Item.PeriotyWeapon.Main, Vector3.zero, 1500);
    Item Staff = new Item("Staff", Item.ItemType.Staff, Item.PeriotyWeapon.Main, Vector3.zero, 2000);
    Item Shield = new Item("Shield", Item.ItemType.Shield, Item.PeriotyWeapon.Sub1 , Vector3.zero, 1500);

    Item Staff2 = new Item("Staff2", Item.ItemType.Staff2, Item.PeriotyWeapon.Main, Vector3.zero, 0);
    Item Dagger2 = new Item("Dagger2", Item.ItemType.Dagger2, Item.PeriotyWeapon.Sub1, Vector3.zero, 0);

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

    public void IistAddAll()
    {
        list_WholeItem.Add(Portion);
        list_WholeItem.Add(Sword);
        list_WholeItem.Add(Sword2);
        list_WholeItem.Add(Sword3);
        list_WholeItem.Add(Dagger);
        list_WholeItem.Add(Axe);
        list_WholeItem.Add(Staff);
        list_WholeItem.Add(Shield);

        list_WholeItem.Add(Staff2);
        list_WholeItem.Add(Dagger2);
    }

    public void EquipListAdd(Item item)
    {

    }



}

