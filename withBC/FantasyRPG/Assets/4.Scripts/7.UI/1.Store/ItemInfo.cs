using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public enum ItemType { NONE,Portion,Weapon };
    public enum AttachedType { NONE, Store, Inventory }; //소속된 곳 인벤에 있냐, 상점에 있냐

    public ItemType itemType = ItemType.NONE;
    public AttachedType WhereAttached = AttachedType.NONE; 


    public string ItemName = null;

    private GameObject InventoryObj;
    //private GameObject StoreObj = GameObject.Find("Store");

    private void Start()
    {
        InventoryObj = GameObject.Find("Inventory");
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
            StartCoroutine(OnMouseDown());
        }

       
    }

    IEnumerator OnMouseDown()
    {
        //Vector3 scrSpace = Camera.main.WorldToScreenPoint(transform.position);
        //Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(
        //    new Vector3(Input.mousePosition.x, Input.mousePosition.y, scrSpace.z));

        while (Input.GetMouseButton(0))
        {
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));

            Vector3 curPosition = new Vector3(MousePos.x, MousePos.y, transform.position.z);  
            transform.localPosition = curPosition;
            yield return null;
        }
    }

}
