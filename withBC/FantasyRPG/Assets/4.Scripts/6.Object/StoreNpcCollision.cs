using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreNpcCollision : MonoBehaviour
{

    private GameObject UINotice;
    private GameObject UIStore;
    private GameObject UIIven;


    private void OnTriggerEnter(Collider collider)
    {
        CharacterInformation charaterInfo = collider.transform.root.GetComponent<CharacterInformation>();
        if (charaterInfo._mode == CharacterInformation.MODE.PLAYER)
        {
            if (collider.gameObject.CompareTag("Player") && collider.gameObject.layer == LayerMask.NameToLayer("Player")) // NPC 충돌시 F키 생성
            {
                GameObject UIObj = GameObject.FindGameObjectWithTag("UI_Notice");
                if (UIObj == null) // 단한번 생성하기 위해
                {
                    UINotice = (GameObject)Instantiate(Resources.Load("UI/UI_Notice", typeof(GameObject)));

                }
            }

        }
    }


    private void OnTriggerStay(Collider collider)
    {
        CharacterInformation charaterInfo = collider.transform.root.GetComponent<CharacterInformation>();
        if (charaterInfo._mode == CharacterInformation.MODE.PLAYER)
        {
            if (collider.gameObject.CompareTag("Player") && collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                if (UINotice != null) // F키누르라는 UI 창이 떳다면(있다면)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (UIStore == null) // 단한번 생성하기 위해
                        {
                            UIStore = (GameObject)Instantiate(Resources.Load("UI/UI_StoreRoot", typeof(GameObject)));
                            GameManager.Instance.isOnUIStore = true;
                        }
                        if (UIIven == null)
                            UIIven = (GameObject)Instantiate(Resources.Load("UI/UI_Inventory", typeof(GameObject)));
                        Destroy(UINotice);
                        GameManager.Instance.isOnUIWindow = true;
                    }
                }//end of if (UINotice != null)
            } //endof if(collider.gameObject.CompareTag("Store_NPC"))
        }// end of if(charaterInfo._mode == CharacterInformation.MODE.PLAYER)
    }

    // 충돌에서 이탈시 ui 창 꺼준다.
    private void OnTriggerExit(Collider collider)
    {
        CharacterInformation charaterInfo = collider.transform.root.GetComponent<CharacterInformation>();
        if (charaterInfo._mode == CharacterInformation.MODE.PLAYER)
        {
            if (collider.gameObject.CompareTag("Player") && collider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Destroy(UINotice);
                Destroy(UIStore);
                Destroy(UIIven);
                GameManager.Instance.isOnUIWindow = false;
            }
        }
    }
}
