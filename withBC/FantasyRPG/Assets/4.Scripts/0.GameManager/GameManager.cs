using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager sInstance;

    public static GameManager Instance
    {
        get
        {
            if (sInstance == null)
            {
                
                GameObject newGameManager = new GameObject("GameManager");
                sInstance = newGameManager.GetComponent<GameManager>();
            }
            return
                sInstance;
        }
    }

    private void Awake()
    {
        sInstance = this;
        DontDestroyOnLoad(gameObject);
    }


   public enum SCENE { TitleScene, SelectScene, WaitScene, InGameScene};

    public  SCENE NowScene = SCENE.TitleScene;

    // ===============  선언부 =====================//
}
