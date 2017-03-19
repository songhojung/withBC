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
    }



    void AddItem(GameObject Item)
    {
        if (list_InvenItem.Count < 20)
        {
            string ItemName = Item.GetComponent<ItemInfo>().ItemName;

            GameObject ItemObj = (GameObject)Instantiate(Resources.Load("UI/" + ItemName, typeof(GameObject)),
                List_IvenSlots[0].transform, false);
            ItemObj.GetComponent<ItemInfo>().WhereAttached = ItemInfo.AttachedType.Inventory;


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

            if (isNotFull)
            {
                list_InvenItem[index] = ItemObj;
                ItemObj.transform.position = List_IvenSlots[index].transform.position; // 아이템을 인벤 슬롯위치에 놓기
                ItemObj.gameObject.transform.parent = List_IvenSlots[index].transform; // 아이템 을 각 인벤슬롯의    자식으로
            }
            else
            {


                list_InvenItem.Add(ItemObj);
                ItemObj.transform.position = List_IvenSlots[list_InvenItem.Count - 1].transform.position; // 아이템을 인벤 슬롯위치에 놓기
                ItemObj.gameObject.transform.parent = List_IvenSlots[list_InvenItem.Count - 1].transform; // 아이템 을 각 인벤슬롯의    자식으로
                Debug.Log(list_InvenItem.Count);

            }
        }



    }


    void SellItem(GameObject Item)
    {
        for (int i = 0; i < list_InvenItem.Count; i++)
        {
            if (list_InvenItem[i] == Item)
            {
                Destroy(list_InvenItem[i]);
                list_InvenItem[i] = null;
                break;
            }
        }

    }

    void ChangingItem(GameObject Item)
    {

    }

   

    
}
