using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonCtrl : MonoBehaviour
{

  

    void PressStartButton()
    {
        SceneManager.LoadScene("InnerMap");
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("Manager"));
    }

    void PressInfoButton()
    {
        
    }

    void PressExitButton()
    {

    }
}
