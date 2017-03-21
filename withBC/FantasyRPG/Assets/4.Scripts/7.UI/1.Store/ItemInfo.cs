using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public enum ItemType { NONE,Portion,Sword,Shield,Staff,Dagger };
    public enum AttachedType { NONE, Store, Inventory }; //소속된 곳 인벤에 있냐, 상점에 있냐

    public ItemType itemType = ItemType.NONE;
    public AttachedType WhereAttached = AttachedType.NONE; 


    public string ItemName = null;

    private GameObject InventoryObj;
    private GameObject StoreObj;
    //private GameObject StoreObj = GameObject.Find("Store");

    private void Start()
    {
        InventoryObj = GameObject.Find("Inventory");
        StoreObj = GameObject.Find("Store");
    }



    void ItemAction()
    {
        if(WhereAttached == AttachedType.Store)
        {
            InventoryObj.SendMessage("AddItem", gameObject , SendMessageOptions.RequireReceiver);
        }
        else if(WhereAttached == AttachedType.Inventory)
        {
            InventoryObj.SendMessage("SellItem", gameObject ,SendMessageOptions.RequireReceiver);
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

    void showInfoItem()
    {
        StoreObj.SendMessage("showInfoItem", gameObject, SendMessageOptions.RequireReceiver);
    }

    void DontShowInfoItem()
    {
        StoreObj.SendMessage("DontShowInfoItem", gameObject, SendMessageOptions.RequireReceiver);
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
