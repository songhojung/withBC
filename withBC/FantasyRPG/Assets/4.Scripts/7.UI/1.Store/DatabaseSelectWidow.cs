using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseSelectWidow : MonoBehaviour
{
    private List<GameObject> List_SelectSlots = new List<GameObject>();
    private List<GameObject> List_EquipItem = new List<GameObject>();
    bool isOver = false;
    private UIButtonScale FirstEquipItemButton;
    private UIButtonScale SecondEquipItemButton;

    private void Start()
    {
        Transform[] slots = GetComponentsInChildren<Transform>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].gameObject.tag == "Item Slot")
            {
                List_SelectSlots.Add(slots[i].gameObject);
            }
        }

        SettingInitEquip();
    }

    void ClickSelectWindow(KeyCode keycode)
    {

        for(int i = 0; i <List_EquipItem.Count; i++)
        {
            Item pitem = List_EquipItem[i].GetComponent<ItemInfo>().item;
            if(pitem.Perioty == Item.PeriotyWeapon.Main)
            {
                FirstEquipItemButton = List_EquipItem[i].GetComponent<UIButtonScale>();
            }
            else if (pitem.Perioty == Item.PeriotyWeapon.Sub1)
            {
                SecondEquipItemButton = List_EquipItem[i].GetComponent<UIButtonScale>();
            }
        }

        bool isover1 = true;
        bool isover2 = false;

        switch (keycode)
        {

            case KeyCode.Alpha1:
                FirstEquipItemButton.SendMessage("OnHover", isover1);
                SecondEquipItemButton.SendMessage("OnHover", isover2);
                break;

            case KeyCode.Alpha2:
                FirstEquipItemButton.SendMessage("OnHover", isover2);
                SecondEquipItemButton.SendMessage("OnHover", isover1);
                break;

        }

        
      
    }

    void AddSelectWindow(Item pItem)
    {
       
        //ItemInfo itemInfo = item.GetComponent<ItemInfo>();
        //itemInfo.item.Perioty
        GameObject UIEquipItem = (GameObject)Instantiate(Resources.Load("UI/Equip_" + pItem.name, typeof(GameObject)));
        UIEquipItem.GetComponent<ItemInfo>().item = pItem;

        RemoveEquioItem(pItem);

        if (pItem.Perioty == Item.PeriotyWeapon.Main)
        {
            
            UIEquipItem.transform.position = List_SelectSlots[0].transform.position;
            UIEquipItem.transform.parent = List_SelectSlots[0].transform.parent;
            UIEquipItem.transform.localScale = Vector3.one;
        }
        else if (pItem.Perioty == Item.PeriotyWeapon.Sub1)
        {
            UIEquipItem.transform.position = List_SelectSlots[1].transform.position;
            UIEquipItem.transform.parent = List_SelectSlots[1].transform.parent;
            UIEquipItem.transform.localScale = Vector3.one;
        }
        else if (pItem.Perioty == Item.PeriotyWeapon.Sub2)
        {
            UIEquipItem.transform.position = List_SelectSlots[2].transform.position;
            UIEquipItem.transform.parent = List_SelectSlots[2].transform.parent;
            UIEquipItem.transform.localScale = Vector3.one;
        }
      
        List_EquipItem.Add(UIEquipItem);
    }

    void RemoveEquioItem(Item pItem)
    {
      
        if(List_EquipItem.Count >0)
        {
            for (int i = 0; i < List_EquipItem.Count; i++)
            {
                ItemInfo EquipItemInfo = List_EquipItem[i].GetComponent<ItemInfo>();
                if (pItem.Perioty == EquipItemInfo.item.Perioty)
                {
                    Destroy(List_EquipItem[i]);
                }
            }
        }
    }


    void SettingInitEquip()
    {
        List<Item> playerEquip = GameManager.Instance.list_EquipItem;
        for (int i = 0; i < playerEquip.Count; i++)
        {
            AddSelectWindow(playerEquip[i]);
        }
    }
}