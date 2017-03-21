using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public Item item = new Item();

    public Item.ItemType E_itemType = Item.ItemType.NONE;
    public enum AttachedType { NONE, Store, Inventory }; //소속된 곳 인벤에 있냐, 상점에 있냐
   

    public AttachedType WhereAttached = AttachedType.NONE;
 


    public string ItemName = null;

    private GameObject InventoryObj;
    private GameObject StoreObj;
    private GameObject infoObj; // 동적으로 생성된 아이템info 오브젝트 담고 파괴하기위한 변수
    //private GameObject StoreObj = GameObject.Find("Store");

    private void Start()
    {
       
        InventoryObj = GameObject.Find("Inventory");
        StoreObj = GameObject.Find("Store");
    }

    public void SetItemInfo(Item pItem)
    {
        item.name = pItem.name;
        item.itemType = pItem.itemType;
        item.Position = pItem.Position;
        item.Gold = pItem.Gold;
    }


    void ItemAction()
    {
        if(WhereAttached == AttachedType.Store)
        {
            Destroy(infoObj);
            InventoryObj.SendMessage("AddItem", gameObject , SendMessageOptions.RequireReceiver);
            GameManager.Instance.SendMessage("addItem", item, SendMessageOptions.RequireReceiver);
            GameManager.Instance.Gold -= item.Gold;



        }
        else if(WhereAttached == AttachedType.Inventory)
        {
           
            InventoryObj.SendMessage("SellItem", gameObject ,SendMessageOptions.RequireReceiver);
            GameManager.Instance.Gold += (item.Gold / 2);
        }
    }



    //아이템이 마우스 오버 됫을때 호출
    void showInfoItem()
    {
        if (WhereAttached == AttachedType.Store)
        {
            //StoreObj.SendMessage("showInfoItem", gameObject, SendMessageOptions.RequireReceiver);
            Item.ItemType ItemType = this.GetComponent<ItemInfo>().item.itemType;
            Transform InfoPos = StoreObj.GetComponent<DatabaceStore>().InfoItemPos;

            switch (ItemType)
            {
                case Item.ItemType.Portion:
                    infoObj = (GameObject)Instantiate(Resources.Load("UI/Info_Hp", typeof(GameObject)), InfoPos, false);
                    infoObj.transform.position = InfoPos.position;
                    infoObj.gameObject.transform.parent = StoreObj.gameObject.transform;
                    break;

                case Item.ItemType.Sword:
                    infoObj = (GameObject)Instantiate(Resources.Load("UI/Info_Sword", typeof(GameObject)), InfoPos, false);
                    infoObj.transform.position = InfoPos.position;
                    infoObj.gameObject.transform.parent = StoreObj.gameObject.transform;
                    break;

            }
        }
        else if (WhereAttached == AttachedType.Inventory)
        {
            
        }
        
    }

    // //아이템이 마우스 아웃 됫을때 아이템 정보창 닫기 
    void DontShowInfoItem()
    {
        if (WhereAttached == AttachedType.Store)
        {
            Destroy(infoObj);
        }
    }




    void ItemChange()
    {

        if (WhereAttached == AttachedType.Inventory)
        {
            //InventoryObj.SendMessage("ChangingItem", gameObject, SendMessageOptions.RequireReceiver);
            //StartCoroutine(OnMouseDown());
        }
    }


    IEnumerator OnMouseDown()
    {
        Vector3 scrSpace = Camera.main.WorldToScreenPoint(transform.position); // 오브젝트를 스크린좌표 로변환
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z)); // 오브젝트와 마우스 월드 차이=오브젝트 월드좌표 - 마우스의 월드 좌표

        while (Input.GetMouseButton(0))
        {
            //현재 마우스의 스크린좌표
            Vector3 curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z);
           
            Vector3 curWorldSpace = Camera.main.ScreenToWorldPoint(curScreenSpace);
            Debug.Log(curWorldSpace);
            // 마우스 월드좌표 = 스크린좌표에서 월드좌표 변환된 마우스 위치 + 오브젝트와 마우스 월드 차이
            Vector3 curPosition = curWorldSpace + offset;
            transform.position = Vector3.MoveTowards(transform.position, curPosition, 1 * Time.deltaTime);
            yield return null;
        }

    }
}
