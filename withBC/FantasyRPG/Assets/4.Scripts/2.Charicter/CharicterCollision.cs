using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharicterCollision : MonoBehaviour
{
    // 아래 오브젝트 들은 동적으로 생성되는 객체를 담고 파괴시키기 위한 변수들임
    private GameObject UINotice;
    private GameObject UISelectMap;
    private GameObject UIStore;
    private GameObject UIIven;

    private CharacterInformation charaterInfo;

    private void Start()
    {
        charaterInfo = GetComponent<CharacterInformation>();
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (charaterInfo._mode == CharacterInformation.MODE.PLAYER)
        {
            if (collider.gameObject.CompareTag("SelectMap")) // 맵선택 종이 충돌시
            {
                GameObject UIObj = GameObject.FindGameObjectWithTag("UI_Notice");
                if (UIObj == null) // 단한번 생성하기 위해
                {
                    UINotice = (GameObject)Instantiate(Resources.Load("UI/UI_Notice", typeof(GameObject)));

                }
            }
            else if (collider.gameObject.CompareTag("Store_NPC")) // 상점엔피시 충돌시
            {
                GameObject UIObj = GameObject.FindGameObjectWithTag("UI_Notice");
                if (UIObj == null)
                {
                    UINotice = (GameObject)Instantiate(Resources.Load("UI/UI_Notice", typeof(GameObject)));
                }
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (charaterInfo._mode == CharacterInformation.MODE.PLAYER)
        {
            if (collider.gameObject.CompareTag("SelectMap"))
            {
                if (UINotice != null) // F키누르라는 UI 창이 떳다면(있다면)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (UISelectMap == null) // 단한번 생성하기 위해
                            UISelectMap = (GameObject)Instantiate(Resources.Load("UI/UI_SelectMapRoot", typeof(GameObject)));

                        Destroy(UINotice);
                        GameManager.Instance.isOnUIWindow = true;
                    }
                }
            }
            else if (collider.gameObject.CompareTag("Store_NPC"))
            {
                if (UINotice != null) // F키누르라는 UI 창이 떳다면(있다면)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (UIStore == null) // 단한번 생성하기 위해
                            UIStore = (GameObject)Instantiate(Resources.Load("UI/UI_StoreRoot", typeof(GameObject)));
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
        if (charaterInfo._mode == CharacterInformation.MODE.PLAYER)
        {
            if (collider.gameObject.CompareTag("SelectMap"))
            {
                Destroy(UINotice);
                Destroy(UISelectMap);
                GameManager.Instance.isOnUIWindow = false;
            }
            else if (collider.gameObject.CompareTag("Store_NPC"))
            {
                Destroy(UINotice);
                Destroy(UIStore);
                Destroy(UIIven);
                GameManager.Instance.isOnUIWindow = false;
            }
        }
    }
}
