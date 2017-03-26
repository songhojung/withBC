using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMap : MonoBehaviour
{
    public GameObject missionLabel;
	// Use this for initialization
	
 // 미션 내용을 껏다 켯다 하는거임
    void OnOffMissionScript()
    {
       if(missionLabel && !missionLabel.activeSelf)
        {
            missionLabel.SetActive(true);
        }
       else if(missionLabel && missionLabel.activeSelf)
        {
            missionLabel.SetActive(false);
        }
    }

  
    void CheckConfirmButton()
    {
        // 미션 내용 객체가 있고 미션내용이 활성화 되어있다면 버튼클릭시 씬전환
        if (missionLabel && missionLabel.activeSelf)
        {
            SceneManager.LoadScene("map_1");
            GameManager.Instance.NowScene = GameManager.SCENE.InGameScene;
        }
    }

    // 닫기 버튼누를떄 창없어짐
    void CloseButton()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject.transform.root.gameObject);
    }

}
