using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaceStore : MonoBehaviour {

    public List<GameObject> list_Item = new List<GameObject>();

    private List<GameObject> List_StoreSlots = new List<GameObject>();

    public Transform InfoItemPos;

    private GameObject infoObj; // 동적으로 생성된 info 오브젝트 담고 파괴하기위한 변수

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


    // 아이템 정보창 열기
    void showInfoItem(GameObject Item)
    {
        ItemInfo.ItemType itemType = Item.GetComponent<ItemInfo>().itemType;
        switch (itemType)
        {
            case ItemInfo.ItemType.Portion:
                infoObj = (GameObject)Instantiate(Resources.Load("UI/Info_Hp",typeof(GameObject)), InfoItemPos, false);
                infoObj.transform.position = InfoItemPos.position;
                infoObj.gameObject.transform.parent = gameObject.transform;
                break;

            case ItemInfo.ItemType.Sword:
                infoObj = (GameObject)Instantiate(Resources.Load("UI/Info_Sword", typeof(GameObject)), InfoItemPos, false);
                infoObj.transform.position = InfoItemPos.position;
                infoObj.gameObject.transform.parent = gameObject.transform;
                break;

        }
    }

    // 아이템 정보창 닫기 
    void DontShowInfoItem()
    {
        Destroy(infoObj);
    }

    void CloseButton()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject.transform.root.gameObject);
    }


}
