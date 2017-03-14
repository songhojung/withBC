using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFire : MonoBehaviour {

    private CharacterInformation Information;
    public GameObject ThrownObj;
    public Transform FirePos;

    private PlayerCtrl player;
    private MoveNPC Npc;
    private Transform playerTr;

    // Use this for initialization
    void Start ()
    {
        Information = GetComponent<CharacterInformation>();
        playerTr = GetComponent<Transform>();
        switch (Information._mode)
        {
            case CharacterInformation.MODE.PLAYER:
                player = GetComponent<PlayerCtrl>();
                Npc = null;
                break;
            case CharacterInformation.MODE.NPC:
                player = null;
                Npc = GetComponent<MoveNPC>();
                break;
        }
        
    }
	
	// Update is called once per frame
	void Update ()
    {

        switch (Information._mode)
        {
            case CharacterInformation.MODE.PLAYER:
                bool IsLeftMouseUp = player.IsLeftMouseUp;
                bool IsLeftMouseDown = player.IsLeftMouseDown;
                bool IsKey_Shift = player.IsKey_Shift;
                bool IsKey_Q = player.IsKey_Q;
                
                //Quaternion rotate = Quaternion.AngleAxis(90, Vector3.up);
                Quaternion arrowrotate = Quaternion.AngleAxis(90, Vector3.up);

                if (Information.Job == CharacterInformation.PlayerJob.ARCHER)
                {
                    if (IsLeftMouseUp)
                    {
                        Instantiate(ThrownObj, FirePos.position, playerTr.rotation);

                    }
                }
                else if (Information.Job == CharacterInformation.PlayerJob.WIZARD)
                {
                    if (IsLeftMouseDown)
                    {
                        StartCoroutine(EffectManager.Instance.CreatEffect("ArrowFX_Fire",
                            FirePos.position, playerTr.rotation, 0.7f, 4.0f));
                        //StartCoroutine(EffectManager.Instance.CreatEffect("DragonFire",
                        //   FirePos.position, playerTr.rotation, 0.7f, 4.0f));

                    }
                    else if (IsKey_Q)
                    {
                        
                        StartCoroutine(EffectManager.Instance.CreatEffect("Fireball", 
                            FirePos.position, playerTr.rotation, 1.0f,4.0f));
                    }
                    else if(IsKey_Shift)
                    {
                        Vector3 revice_firePos = new Vector3(FirePos.position.x, FirePos.position.y- 2, FirePos.position.z );
                        StartCoroutine(EffectManager.Instance.CreatEffect("Lightning",
                               revice_firePos, playerTr.rotation, 1.0f, 4.0f));
                    }
                    
                }
                break;

            case CharacterInformation.MODE.NPC:
                bool IsLeftMouseUp2 = Npc.IsLeftMouseUp;
                Transform playerTr2 = Npc.transform;
                //Quaternion rotate = Quaternion.AngleAxis(90, Vector3.up);
                Quaternion arrowrotate2 = Quaternion.AngleAxis(90, Vector3.up);

                if (Information.Job == CharacterInformation.PlayerJob.ARCHER)
                {
                    if (IsLeftMouseUp2)
                    {
                        Instantiate(ThrownObj, FirePos.position, playerTr2.rotation);
                        // ThrownObj.transform.rotation = arrowrotate;
                    }
                }
                else if (Information.Job == CharacterInformation.PlayerJob.WIZARD)
                {
                    if (IsLeftMouseUp2)
                    {

                        StartCoroutine(EffectManager.Instance.CreatEffect("Fireball", FirePos.position, playerTr2.rotation));

                    }
                }
                break;
        }
        
        
    }

}
