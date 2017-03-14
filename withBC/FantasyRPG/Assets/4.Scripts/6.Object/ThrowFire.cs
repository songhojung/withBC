using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFire : MonoBehaviour {

    private CharacterInformation Information;
    public GameObject ThrownObj;
    public Transform FirePos;

    private PlayerCtrl player;
    private MoveNPC Npc;
    // Use this for initialization
    void Start ()
    {
        Information = GetComponent<CharacterInformation>();
        switch(Information._mode)
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
                Transform playerTr = player.transform;
                //Quaternion rotate = Quaternion.AngleAxis(90, Vector3.up);
                Quaternion arrowrotate = Quaternion.AngleAxis(90, Vector3.up);

                if (Information.Job == CharacterInformation.PlayerJob.ARCHER)
                {
                    if (IsLeftMouseUp)
                    {
                        Instantiate(ThrownObj, FirePos.position, playerTr.rotation);
                        // ThrownObj.transform.rotation = arrowrotate;
                    }
                }
                else if (Information.Job == CharacterInformation.PlayerJob.WIZARD)
                {
                    if (IsLeftMouseUp)
                    {

                        StartCoroutine(EffectManager.Instance.CreatEffect("Fireball", FirePos.position, playerTr.rotation));

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
