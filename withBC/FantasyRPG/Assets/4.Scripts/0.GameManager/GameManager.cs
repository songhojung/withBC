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
    public enum PlayerJob { NONE, WARRIOR, ARCHER, WIZARD };
    public enum SCENE { TitleScene, SelectScene, WaitScene, InGameScene };
  

    ItemDatabase itemDatabase = new ItemDatabase();


    public PlayerJob playerJob = PlayerJob.NONE;
    public SCENE NowScene = SCENE.TitleScene; // 현재 씬이 어디 있는지 알기 위함

    public bool isOnUIWindow = false; // 상점창, 필드선택창 열릴시 캐릭터 카메라 회전을 잠그기 위한 변수
    public bool isOnUIStore = false; // 상점창이 켜져잇냐 ? -> 아이템팔기 위한 변수

    public int Gold = 5000;

    public List<Item> list_EquipItem = new List<Item>();

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

    void addEquipItem(Item pItem)
    {
        list_EquipItem.Add(pItem);
    }

    private void Start()
    {
        itemDatabase.IistAddAll();

        //임시 
        playerJob = PlayerJob.WIZARD;
        NowScene = SCENE.WaitScene;
        SettingUISelectWindow();

    }

    void SettingUISelectWindow()
    {
        if(playerJob == PlayerJob.WIZARD)
        {
                GameObject UISelectWindow = (GameObject)Instantiate(Resources.Load("UI/UI_SelectWindow"));
                // UISelectWindow.gameObject.name = "UI_SelectWindow";

            for (int i = 0; i < itemDatabase.list_WholeItem.Count; i++)
            {
                if (itemDatabase.list_WholeItem[i].itemType == Item.ItemType.Staff2)
                {
                    list_EquipItem.Add(itemDatabase.list_WholeItem[i]);
 
                }
                else if (itemDatabase.list_WholeItem[i].itemType == Item.ItemType.Dagger2)
                {
                    list_EquipItem.Add(itemDatabase.list_WholeItem[i]);
  
                }
            }
        }
    }
    
    void SettingWhenMainMap()
    {

    }

    //private void Update()
    //{
    //    if (list_Items.Count > 1)
    //    {
    //        string na = list_Items[0].name;
    //    }
    //}


}
