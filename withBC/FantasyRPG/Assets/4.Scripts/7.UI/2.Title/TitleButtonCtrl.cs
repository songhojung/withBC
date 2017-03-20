using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonCtrl : MonoBehaviour
{

  

    void PressStartButton()
    {
        SceneManager.LoadScene("InnerMap");
        GameManager.Instance.NowScene = GameManager.SCENE.TitleScene;
        
    }

    void PressInfoButton()
    {
        
    }

    void PressExitButton()
    {

    }
}
