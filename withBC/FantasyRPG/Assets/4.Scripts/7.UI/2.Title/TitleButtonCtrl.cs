using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonCtrl : MonoBehaviour
{

  

    void PressStartButton()
    {

        GameManager.Instance.NowScene = GameManager.SCENE.WaitScene;
        GameManager.Instance.playerJob = GameManager.PlayerJob.WIZARD;

        SceneManager.LoadScene("InnerMap");
        
        //StartCoroutine( SendMassageforWait());


    }

    void PressInfoButton()
    {
        
    }

    void PressExitButton()
    {

    }
   
 

}
