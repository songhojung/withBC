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

        //임시 
        playerJob = PlayerJob.WIZARD;

    }

    // ===============  선언부 =====================//
    public enum PlayerJob { NONE, WARRIOR, ARCHER, WIZARD };
    public enum SCENE { NONE,TitleScene, SelectScene, WaitScene, InGameScene, BossScene };
    //[HideInInspector]

    private ItemDatabase itemDatabase = new ItemDatabase();
    public GameObject PlayerObject;
    public GameObject CameraObject;
    public GameObject Npc1= null;
    public GameObject Npc2= null;

    public PlayerJob playerJob = PlayerJob.NONE;
    public SCENE NowScene = SCENE.NONE; // 현재 씬이 어디 있는지 알기 위함
    
    public bool isOnUIWindow = false; // 상점창, 필드선택창 열릴시 캐릭터 카메라 회전을 잠그기 위한 변수
    public bool isOnUIStore = false; // 상점창이 켜져잇냐 ? -> 아이템팔기 위한 변수
    public bool OnUIMouseRight = false;

    
    public int Gold = 5000;

    public List<Item> list_EquipItem = new List<Item>();

    public List<Item> list_Items = new List<Item>();


    private void Start()
    {
        DontDestroyOnLoad(this);

        itemDatabase.IistAddAll();


        //SettingUISelectWindow();

       // PlayerObject = GameObject.FindGameObjectWithTag("Player");
       //if (PlayerObject)
       //{
       //     if (!PlayerObject.GetComponent<CharacterInformation>())
       //     {
       //         CharacterInformation parent = PlayerObject.GetComponentInParent<CharacterInformation>();
       //         PlayerObject = parent.gameObject;

       //     }

       //}
        

    }

    // 씬전환이 완료되면 발생되는 이벤트
    void OnLevelWasLoaded(int level)
    {
        Debug.Log("로드완료");
        SettingUISelectWindow();
        StartSettingObjects();
    }

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
        for (int i = 0; i < list_EquipItem.Count; i++)
        {
            if (pItem.Perioty == list_EquipItem[i].Perioty)
            {
                list_EquipItem.Remove(list_EquipItem[i]);
                
                break;
            }
        }
        list_EquipItem.Add(pItem);
    }

   
    void CreatPlayer()
    {
        if (NowScene == SCENE.WaitScene)
        {

        }
    }

    void SettingUISelectWindow()
    {
        if (NowScene == SCENE.WaitScene)
        {
            if (playerJob == PlayerJob.WIZARD)
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
        else if(NowScene == SCENE.InGameScene)
        {
            GameObject UISelectWindow = (GameObject)Instantiate(Resources.Load("UI/UI_SelectWindow"));
            GameObject UIHp= (GameObject)Instantiate(Resources.Load("UI/UI_Hp"));
        }
    }
    
    void SettingWhenMainMap()
    {

    }

    void StartSettingObjects()
    {
        if (NowScene == SCENE.WaitScene)
        {
            CameraObject = (GameObject)Instantiate(Resources.Load("Camera/LookAt", typeof(GameObject)));
            DontDestroyOnLoad(CameraObject);
            if (playerJob == PlayerJob.WIZARD)
            {
                Vector3 characterPos = new Vector3(2.27f, -3.14f, -12.95f);
                PlayerObject = (GameObject)Instantiate(Resources.Load("Character/Wizard Girl",typeof(GameObject))
                    , characterPos, Quaternion.identity);
                DontDestroyOnLoad(PlayerObject);
            }
        }
        else if (NowScene == SCENE.InGameScene)
        {
            //Instantiate(CameraObject);
            if (playerJob == PlayerJob.WIZARD)
            {
                Vector3 characterPos = new Vector3(930.77f, 66.38f, 416.42f);
                //Instantiate(PlayerObject, characterPos, Quaternion.identity);
                PlayerObject.gameObject.transform.position = characterPos;
            }
        }
    }

    private void Update()
    {
        
    }


}
