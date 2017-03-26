using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonCtrl : MonoBehaviour
{

  

    void PressStartButton()
    {
        
        SceneManager.LoadScene("InnerMap");
        
        GameManager.Instance.NowScene = GameManager.SCENE.WaitScene;
        GameManager.Instance.playerJob = GameManager.PlayerJob.WIZARD;
        //StartCoroutine( SendMassageforWait());


    }

    void PressInfoButton()
    {
        
    }

    void PressExitButton()
    {

    }
   
    IEnumerator SendMassageforWait()
    {
        AsyncOperation async = Application.LoadLevelAsync("InnerMap");
        yield return async;

        if(async.isDone)
        {
            GameManager.Instance.SendMessage("SettingUISelectWindow", SendMessageOptions.RequireReceiver);
        }

       
    }

}
