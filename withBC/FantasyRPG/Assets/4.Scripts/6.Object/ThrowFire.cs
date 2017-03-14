using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFire : MonoBehaviour {

    private CharacterInformation.PlayerJob Job;
    public GameObject ThrownObj;
    public Transform FirePos;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Job = GetComponent<CharacterInformation>().Job;
        PlayerCtrl player = GetComponent<PlayerCtrl>();
        bool IsLeftMouseUp = player.IsLeftMouseUp;
        Transform playerTr = player.transform;

        //Quaternion rotate = Quaternion.AngleAxis(90, Vector3.up);
        Quaternion arrowrotate = Quaternion.AngleAxis(90,Vector3.up);

        if (Job == CharacterInformation.PlayerJob.ARCHER)
        {
            if (IsLeftMouseUp)
            {
                Instantiate(ThrownObj, FirePos.position, playerTr.rotation);
                // ThrownObj.transform.rotation = arrowrotate;
            }
        }
        else if (Job == CharacterInformation.PlayerJob.WIZARD)
        {
            if (IsLeftMouseUp)
            {
               
               StartCoroutine( EffectManager.Instance.CreatEffect("Fireball",FirePos.position, playerTr.rotation));
                
            }
        }



    }
}
