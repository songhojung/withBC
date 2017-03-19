using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaceStore : MonoBehaviour {

    public List<GameObject> list_Item = new List<GameObject>();

    private List<GameObject> List_StoreSlots = new List<GameObject>();

    private void Start()
    {
        // 상점창에 있는 슬롯을 리스트에 담기
        Transform[] slots =  GetComponentsInChildren<Transform>();

        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].gameObject.tag =="Item Slot")
            {
                List_StoreSlots.Add(slots[i].gameObject);
            }
        }

        // 아이템을 상점창에 등록하기.
        for(int i = 0; i <list_Item.Count; i++)
        {
            GameObject ItemObj = Instantiate(list_Item[i], List_StoreSlots[i].transform, false);
            ItemObj.GetComponent<ItemInfo>().ItemName = list_Item[i].name;
            ItemObj.GetComponent<ItemInfo>().WhereAttached = ItemInfo.AttachedType.Store; // 이아이템은 상점에 소속됨
            ItemObj.transform.position = List_StoreSlots[i].transform.position; // 아이템을 상점 슬롯위치에
            ItemObj.gameObject.transform.parent = List_StoreSlots[i].transform; // 아이템 을 각 상점슬롯 자식으로
        }


    }

    void SellItem()
    {

        for(int i = 0; i < list_Item.Count; i ++)
        {
            
        }
    }

    
}
