using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPaperCollison : MonoBehaviour
{
    private GameObject UINotice;
    private GameObject UISelectMap;


    private void OnTriggerEnter(Collider collider)
    {
        CharacterInformation charaterInfo = collider.transform.root.GetComponent<CharacterInformation>();
        if (charaterInfo._mode == CharacterInformation.MODE.PLAYER)
        {
            if (collider.gameObject.CompareTag("Player") && collider.gameObject.layer == LayerMask.NameToLayer("Player")) // 맵선택 종이 충돌시 F키 생성
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
                        if (UISelectMap == null) // 단한번 생성하기 위해
                            UISelectMap = (GameObject)Instantiate(Resources.Load("UI/UI_SelectMapRoot", typeof(GameObject)));

                        Destroy(UINotice);
                        GameManager.Instance.isOnUIWindow = true;
                    }
                }
            }
           
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
                Destroy(UISelectMap);
                GameManager.Instance.isOnUIWindow = false;
            }
        }
    }
}
