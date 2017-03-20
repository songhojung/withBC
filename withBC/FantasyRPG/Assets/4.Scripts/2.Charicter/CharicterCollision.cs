using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharicterCollision : MonoBehaviour
{
    private GameObject UINotice;
    private GameObject UISelectMap;
    private GameObject UIInven;


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("SelectMap")) // 맵선택 종이 충돌시
        {
            GameObject UIObj = GameObject.FindGameObjectWithTag("UI_Notice");
            if (UIObj == null)
            {
                UINotice = (GameObject)Instantiate(Resources.Load("UI/UI_Notice",typeof(GameObject)));
               
            }
        }
        else if (collider.gameObject.CompareTag("Store_NPC")) // 엔피시 충돌시
        {
            GameObject UIObj = GameObject.FindGameObjectWithTag("UI_Notice");
            if (UIObj == null)
            {
                UINotice = (GameObject)Instantiate(Resources.Load("UI/UI_Notice", typeof(GameObject)));
            }
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.CompareTag("SelectMap"))
        {
            if (UINotice != null)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    if(UISelectMap == null)
                    UISelectMap = (GameObject)Instantiate(Resources.Load("UI/UI_SelectMapRoot", typeof(GameObject)));

                    Destroy(UINotice);
                }
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("SelectMap")) // 맵선택 종이 충돌시
        {
            Destroy(UINotice);
            Destroy(UISelectMap);
        }
        else if (collider.gameObject.CompareTag("Store_NPC"))
        {
            Destroy(UINotice);
        }
    }
}
