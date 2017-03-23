using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseSelectWidow : MonoBehaviour
{
    private List<GameObject> List_SelectSlots = new List<GameObject>();
    private List<GameObject> List_EquipItem = new List<GameObject>();

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

        // 초기에 선택창 첫번째아이템  아이콘 오버 되게 함.
        ClickSelectWindow(KeyCode.Alpha1);
    }

    // 선택창 아이템 클릭시 아이콘 오버 되게 함.
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

    void AddSelectWindow(GameObject pItem)
    {
       
        ItemInfo pitemInfo = pItem.GetComponent<ItemInfo>();
        //itemInfo.item.Perioty
        GameObject UIEquipItem = (GameObject)Instantiate(Resources.Load("UI/Equip_" + pitemInfo.item.name, 
            typeof(GameObject)));
        UIEquipItem.GetComponent<ItemInfo>().item = pitemInfo.item;

        RemoveEquipItem(pItem);

        if (pitemInfo.item.Perioty == Item.PeriotyWeapon.Main)
        {
            
            UIEquipItem.transform.position = List_SelectSlots[0].transform.position;
            UIEquipItem.transform.parent = List_SelectSlots[0].transform.parent;
            UIEquipItem.transform.localScale = Vector3.one;
        }
        else if (pitemInfo.item.Perioty == Item.PeriotyWeapon.Sub1)
        {
            UIEquipItem.transform.position = List_SelectSlots[1].transform.position;
            UIEquipItem.transform.parent = List_SelectSlots[1].transform.parent;
            UIEquipItem.transform.localScale = Vector3.one;
        }
        else if (pitemInfo.item.Perioty == Item.PeriotyWeapon.Sub2)
        {
            UIEquipItem.transform.position = List_SelectSlots[2].transform.position;
            UIEquipItem.transform.parent = List_SelectSlots[2].transform.parent;
            UIEquipItem.transform.localScale = Vector3.one;
        }
        
        List_EquipItem.Add(UIEquipItem);
    }

    // 선택창에서 인벤으로 빠질떄
    void RemoveEquipItem(GameObject pItem)
    {
      
        if(List_EquipItem.Count >0)
        {
            ItemInfo pitemInfo = pItem.GetComponent<ItemInfo>();
            int index = 99;
            bool IsthereSame = false;
            for (int i = 0; i < List_EquipItem.Count; i++)
            {
                ItemInfo EquipItemInfo = List_EquipItem[i].GetComponent<ItemInfo>();
                
                if (pitemInfo.item.Perioty == EquipItemInfo.item.Perioty)
                {
                    index = i;
                    IsthereSame = true;
                    Destroy(List_EquipItem[i]);
                    break;
                }
                
            }

            if (IsthereSame)
            {
                GameObject.Find("Inventory").SendMessage("AddItem", List_EquipItem[index], SendMessageOptions.RequireReceiver);
                GameManager.Instance.gameObject.SendMessage("addItem", List_EquipItem[index].GetComponent<ItemInfo>().item);
                List_EquipItem.Remove(List_EquipItem[index]);
                
            }
        }
    }

    // 선택창 생성시 플레이어가 가지고있는 무기 아이콘 생성
    void SettingInitEquip()
    {
        List<Item> playerEquip = GameManager.Instance.list_EquipItem;
        for (int i = 0; i < playerEquip.Count; i++)
        {
           
            GameObject UIEquipItem = (GameObject)Instantiate(Resources.Load("UI/Equip_" + playerEquip[i].name,
                typeof(GameObject)));
            UIEquipItem.GetComponent<ItemInfo>().item = playerEquip[i];


            if (playerEquip[i].Perioty == Item.PeriotyWeapon.Main)
            {

                UIEquipItem.transform.position = List_SelectSlots[0].transform.position;
                UIEquipItem.transform.parent = List_SelectSlots[0].transform.parent;
                UIEquipItem.transform.localScale = Vector3.one;
            }
            else if (playerEquip[i].Perioty == Item.PeriotyWeapon.Sub1)
            {
                UIEquipItem.transform.position = List_SelectSlots[1].transform.position;
                UIEquipItem.transform.parent = List_SelectSlots[1].transform.parent;
                UIEquipItem.transform.localScale = Vector3.one;
            }
            else if (playerEquip[i].Perioty == Item.PeriotyWeapon.Sub2)
            {
                UIEquipItem.transform.position = List_SelectSlots[2].transform.position;
                UIEquipItem.transform.parent = List_SelectSlots[2].transform.parent;
                UIEquipItem.transform.localScale = Vector3.one;
            }

            List_EquipItem.Add(UIEquipItem);
        }
    }
}