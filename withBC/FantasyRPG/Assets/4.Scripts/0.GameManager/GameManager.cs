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

    // ===============  선언부 =====================//

    public enum SCENE { TitleScene, SelectScene, WaitScene, InGameScene};

    public SCENE NowScene = SCENE.TitleScene; // 현재 씬이 어디 있는지 알기 위함

    public bool isOnUIWindow = false; // 상점창, 필드선택창 열릴시 캐릭터 카메라 회전을 잠그기 위한 변수

    public int Gold = 5000;

    public List<GameObject> list_InvenItem = new List<GameObject>();

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.F3))
    //    {
    //        --Gold;
    //    }
    //}


}
