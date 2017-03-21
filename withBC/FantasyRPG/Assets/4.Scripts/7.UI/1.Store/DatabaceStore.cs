using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaceStore : MonoBehaviour {

    ItemDatabase itemDatabase = new ItemDatabase();
    public List<GameObject> list_Item = new List<GameObject>();

    private List<GameObject> List_StoreSlots = new List<GameObject>();

    public Transform InfoItemPos;

  

    private void Start()
    {
        itemDatabase.IistAdd();
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
        for (int i = 0; i < itemDatabase.list_DItem.Count; i++)
        {
            GameObject ItemObj = (GameObject)Instantiate(Resources.Load("UI/"+itemDatabase.list_DItem[i].name, typeof(GameObject)),
                List_StoreSlots[i].transform, false);
            ItemObj.GetComponent<ItemInfo>().SetItemInfo(itemDatabase.list_DItem[i]);
            ItemObj.GetComponent<ItemInfo>().ItemName = list_Item[i].name;
            ItemObj.GetComponent<ItemInfo>().WhereAttached = ItemInfo.AttachedType.Store; // 이아이템은 상점에 소속됨
            ItemObj.transform.position = List_StoreSlots[i].transform.position; // 아이템을 상점 슬롯위치에
            ItemObj.gameObject.transform.parent = List_StoreSlots[i].transform; // 아이템 을 각 상점슬롯 자식으로
        }


    }

    

    void CloseButton()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject.transform.root.gameObject);
        GameManager.Instance.isOnUIStore = false;
    }


}
