using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaceInventory : MonoBehaviour
{
    private List<GameObject> list_InvenItem = new List<GameObject>();

    private List<GameObject> List_IvenSlots = new List<GameObject>();




    private void Start()
    {
        Transform[] slots = GetComponentsInChildren<Transform>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].gameObject.tag == "Item Slot")
            {
                List_IvenSlots.Add(slots[i].gameObject);
            }
        }

        // 게임매니저에 들고있는 아이템이 있다면 인벤토리 생성시 아이템 셋팅
        SettingItemOnIven();
    }


    void SettingItemOnIven()
    {
        // 게임매니저에 들고있는 아이템이 있다면 인벤토리 생성시 아이템 셋팅
        List<Item> pItems = GameManager.Instance.list_Items;
        for(int i = 0; i < pItems.Count; i++)
        {
            if (pItems[i] == null)
            {
                list_InvenItem.Add((GameObject)null);
                continue;
            }
            GameObject Item = (GameObject)Instantiate(Resources.Load("UI/" + pItems[i].name, typeof(GameObject)),
                List_IvenSlots[0].transform, false);
            Item.transform.position = List_IvenSlots[i].transform.position; // 아이템을 인벤 슬롯위치에 놓기
            Item.gameObject.transform.parent = List_IvenSlots[i].transform;
            Item.GetComponent<ItemInfo>().WhereAttached = ItemInfo.AttachedType.Inventory;
            Item.GetComponent<ItemInfo>().item = pItems[i];
            list_InvenItem.Add(Item);
        }

    }


    // 인벤에 아이템추가 
    void AddItem(GameObject Item)
    {
        if (list_InvenItem.Count < 20)
        {
            string ItemName = Item.GetComponent<ItemInfo>().item.name;

            GameObject ItemObj = (GameObject)Instantiate(Resources.Load("UI/" + ItemName, typeof(GameObject)),
                List_IvenSlots[0].transform, false);
            ItemObj.GetComponent<ItemInfo>().WhereAttached = ItemInfo.AttachedType.Inventory;
            ItemObj.GetComponent<ItemInfo>().item = Item.GetComponent<ItemInfo>().item; // 인벤에 생성시 아이템정보 넘기기

            bool isNotFull = false;
            int index = 99;
            for (int i = 0; i < list_InvenItem.Count; i++)
            {
                if (list_InvenItem[i] == null)
                {
                    isNotFull = true;
                    index = i;
                    break;
                }
            }

            if (isNotFull) //빈칸에 채우기
            {
                list_InvenItem[index] = ItemObj;
                ItemObj.transform.position = List_IvenSlots[index].transform.position; // 아이템을 인벤 슬롯위치에 놓기
                ItemObj.gameObject.transform.parent = List_IvenSlots[index].transform; // 아이템 을 각 인벤슬롯의    자식으로
                
            }
            else // 다채워져있으면 뒤부터 채우기
            {
                list_InvenItem.Add(ItemObj);
                ItemObj.transform.position = List_IvenSlots[list_InvenItem.Count - 1].transform.position; // 아이템을 인벤 슬롯위치에 놓기
                ItemObj.gameObject.transform.parent = List_IvenSlots[list_InvenItem.Count - 1].transform; // 아이템 을 각 인벤슬롯의    자식으로
                
              

            }
        }



    }


    void SellItem(GameObject Item)
    {
        if (GameManager.Instance.isOnUIStore) // 상점이 켜져있으면 팔기 가능
        {
            for (int i = 0; i < list_InvenItem.Count; i++)
            {
                if (list_InvenItem[i] == Item)
                { // 선택된 아이템을 리스트에 널시키기
                    Destroy(list_InvenItem[i]);
                    list_InvenItem[i] = null;
                    //게임매니저에 아이템정보 리스트도 널시킨다.
                    GameManager.Instance.list_Items[i] = null;
                    break;
                }
            }
        }
    }

    //인벤에서 무기 장착시 ...
    void RemoveItem(GameObject Item)
    {
        for (int i = 0; i < list_InvenItem.Count; i++)
        {
            if (list_InvenItem[i] == Item)
            { // 선택된 아이템을 리스트에 널시키기
                Destroy(list_InvenItem[i]);
                list_InvenItem[i] = null;
                //게임매니저에 아이템정보 리스트도 널시킨다.
                GameManager.Instance.list_Items[i] = null;
                break;
            }
        }
    }

    void CloseButton()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject.transform.root.gameObject);
        GameManager.Instance.isOnUIWindow = false;
    }




}
