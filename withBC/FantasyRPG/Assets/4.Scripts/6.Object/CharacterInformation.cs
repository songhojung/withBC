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


                // 단축창 아이콘 바꾸기
                GameObject.Find("Selectwindow").SendMessage("ClickSelectWindow", KeyCode.Alpha1);
            }
            else if (pPlayerCtrl.IsNumKey_2)
            {
                //아이템 바꾸기


                // 단축창 아이콘 바꾸기
                GameObject.Find("Selectwindow").SendMessage("ClickSelectWindow", KeyCode.Alpha2);
            }
        }
        else if (_mode == MODE.NPC)
        {
            if (pPlayerCtrl.IsNumKey_1)
            {
                //아이템 바꾸기


               
            }
            else if (pPlayerCtrl.IsNumKey_2)
            {
                //아이템 바꾸기


                
            }
        }

    }

    void CreatNewWeapon()
    {
        Transform[] WeaponSpotChilds =  WeaponSpot.transform.GetComponentsInChildren<Transform>();
        Transform EquipWeapn = null;
        for(int i = 0; i < WeaponSpotChilds.Length; i++)
        {
            if(WeaponSpotChilds[i].gameObject.tag =="Weapon")
            {
                EquipWeapn = WeaponSpotChilds[i];
            }
        }


        List<Item> pEquipItems = GameManager.Instance.list_EquipItem;

        for(int i  = 0; i < pEquipItems.Count; i++)
        {
            if (EquipWeapn != null)
            {
                if (EquipWeapn.gameObject.name == "Weapon_" + pEquipItems[i].name)
                {
                    Destroy(EquipWeapn.gameObject);
                    GameObject newWeapon = (GameObject)Instantiate(Resources.Load("UI/Weapon/Weapon_" + pEquipItems[i].name, typeof(GameObject))
                        , WeaponSpot.transform);

                    newWeapon.transform.parent = WeaponSpot.transform;

                    break;
                }
            }
        }
    }
}
