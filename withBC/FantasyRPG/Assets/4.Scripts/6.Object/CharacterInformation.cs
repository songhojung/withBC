using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInformation : MonoBehaviour {

    public int hp;

    public enum STATE
    {
        ATTACK, STAY, HIT
    };

    public bool isDie = false;
    public bool isHit = false;
    public bool isAttack = false;

    public STATE MonsterState = STATE.STAY;

    public bool isOnceAttack = false;

    public int damage;

    public enum MODE
    {
        NPC, PLAYER
    };

    public enum PlayerJob { NONE, WARRIOR, ARCHER, WIZARD };


    public PlayerJob Job = PlayerJob.NONE;
    public MODE _mode;

    private GameObject UIIven;
    private PlayerCtrl pPlayerCtrl;
    private GameObject WeaponSpot; // 각캐릭터마다 무기 장착하는 곳(오브젝트)
    
    // Use this for initialization
    void Start ()
    {
        pPlayerCtrl = GetComponent<PlayerCtrl>();
        WeaponSpot = GameObject.FindGameObjectWithTag("EquipWeaponSpot");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_mode == MODE.PLAYER)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (UIIven == null)
                {
                    UIIven = UIIven = (GameObject)Instantiate(Resources.Load("UI/UI_Inventory", typeof(GameObject)));
                    GameManager.Instance.isOnUIWindow = true;
                }
            }
            else if (pPlayerCtrl.IsNumKey_1)
            {
                //아이템 바꾸기
                CreatNewWeapon(KeyCode.Alpha1);

                // 단축창 아이콘 바꾸기
                GameObject.Find("Selectwindow").SendMessage("ClickSelectWindow", KeyCode.Alpha1);
            }
            else if (pPlayerCtrl.IsNumKey_2)
            {
                //아이템 바꾸기
                CreatNewWeapon(KeyCode.Alpha2);

                // 단축창 아이콘 바꾸기
                GameObject.Find("Selectwindow").SendMessage("ClickSelectWindow", KeyCode.Alpha2);
            }
        }
        else if (_mode == MODE.NPC)
        {
            //if (pPlayerCtrl.IsNumKey_1)
            //{
            //    //아이템 바꾸기
            //
            //
            //   
            //}
            //else if (pPlayerCtrl.IsNumKey_2)
            //{
            //    //아이템 바꾸기
            //
            //
            //    
            //}
        }

    }

    void CreatNewWeapon(GameObject weapon)
    {
        ItemInfo newWeaponInfo = weapon.GetComponent<ItemInfo>();

        Transform[] WeaponSpotChilds =  WeaponSpot.transform.GetComponentsInChildren<Transform>();
        Transform EquipWeapn = null;
        for(int i = 0; i < WeaponSpotChilds.Length; i++)
        {
            if(WeaponSpotChilds[i].gameObject.tag =="Weapon")
            {
                EquipWeapn = WeaponSpotChilds[i]; // 하이라키에 있는 무기 오브젝트가져오기
            }
        }
       

        List<Item> pEquipItems = GameManager.Instance.list_EquipItem;
        List<Item> pInvenItems = GameManager.Instance.list_Items;

        //인벤에 있는 아이템이랑 하이라키있는 무기랑 같은게 있냐
        for(int i  = 0; i < pInvenItems.Count; i++)
        {
            if (EquipWeapn != null)
            {
                if (EquipWeapn.gameObject.name == "Weapon_" + pInvenItems[i].name)
                {
                    // 현재 하이라키 있는 무기 오브젝트 지우자
                    Destroy(EquipWeapn.gameObject);

                    GameObject newWeapon = (GameObject)Instantiate(Resources.Load("Weapon/Weapon_" + newWeaponInfo.item.name, typeof(GameObject))
                        , WeaponSpot.transform);
                    newWeapon.gameObject.name = "Weapon_" + newWeaponInfo.item.name;

                    newWeapon.transform.parent = WeaponSpot.transform;

                    break;
                }
            }
        }
    }

    void CreatNewWeapon(KeyCode key)
    {

        Transform[] WeaponSpotChilds = WeaponSpot.transform.GetComponentsInChildren<Transform>();
        Transform EquipWeapn = null;
        for (int i = 0; i < WeaponSpotChilds.Length; i++)
        {
            if (WeaponSpotChilds[i].gameObject.tag == "Weapon")
            {
                EquipWeapn = WeaponSpotChilds[i]; // 하이라키에 있는 무기 오브젝트가져오기
            }
        }


        List<Item> pEquipItems = GameManager.Instance.list_EquipItem;
        List<Item> pInvenItems = GameManager.Instance.list_Items;
        int index = 0;
        //인벤에 있는 아이템이랑 하이라키있는 무기랑 같은게 있냐
        for (int i = 0; i < pEquipItems.Count; i++)
        {
            if (EquipWeapn != null)
            {
                if (key == KeyCode.Alpha1)
                {
                    if(EquipWeapn.gameObject.name != "Weapon_" + pEquipItems[i].name)
                    {
                        index = i;
                        if (pEquipItems[i].Perioty == Item.PeriotyWeapon.Main)
                        {
                            Destroy(EquipWeapn.gameObject);

                            GameObject newWeapon = (GameObject)Instantiate(Resources.Load("Weapon/Weapon_" + pEquipItems[i].name, typeof(GameObject))
                                , WeaponSpot.transform);
                            newWeapon.gameObject.name = "Weapon_" + pEquipItems[i].name;

                            newWeapon.transform.parent = WeaponSpot.transform;
                        }
                    }
                }
                else if (key == KeyCode.Alpha2)
                {
                    if (EquipWeapn.gameObject.name != "Weapon_" + pEquipItems[i].name)
                    {
  
                        if (pEquipItems[i].Perioty == Item.PeriotyWeapon.Sub1)
                        {
                            Destroy(EquipWeapn.gameObject);

                            GameObject newWeapon = (GameObject)Instantiate(Resources.Load("Weapon/Weapon_" + pEquipItems[i].name, typeof(GameObject))
                                , WeaponSpot.transform);
                            newWeapon.gameObject.name = "Weapon_" + pEquipItems[i].name;

                            newWeapon.transform.parent = WeaponSpot.transform;
                            break;
                        }
                    }
                }
            }
        } // end of for
    }// end of funtion
}
