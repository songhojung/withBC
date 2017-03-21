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
    public bool isOnUIStore = false; // 상점창이 켜져잇냐 ? -> 아이템팔기 위한 변수

    public int Gold = 5000;

    public List<GameObject> list_InvenItem = new List<GameObject>();

    public List<Item> list_Items = new List<Item>();


    void addItem(Item pItem)
    {
        bool isNotFull = false;
        int index = 99;
        for (int i = 0; i < list_Items.Count; i++)
        {
            if (list_Items[i] == null)
            {
                isNotFull = true;
                index = i;
                break;
            }
        }

        if(isNotFull)
        {
            list_Items[index] = pItem;
        }
        else
            list_Items.Add(pItem);
    }
    private void Update()
    {
        if (list_Items.Count > 1)
        {
            string na = list_Items[0].name;
        }
    }


}
