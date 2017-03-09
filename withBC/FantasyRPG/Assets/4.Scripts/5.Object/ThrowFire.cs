using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFire : MonoBehaviour {

    public GameObject ThrownObj;
    public Transform FirePos;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayerCtrl player = GetComponent<PlayerCtrl>();
        bool IsLeftMouseUp = player.IsLeftMouseUp;
        Transform playerTr = player.transform;

        //Quaternion rotate = Quaternion.AngleAxis(90, Vector3.up);
        Quaternion arrowrotate = Quaternion.AngleAxis(90,Vector3.up);

        if (IsLeftMouseUp)
        {
            Instantiate(ThrownObj, FirePos.position, playerTr.rotation);
           // ThrownObj.transform.rotation = arrowrotate;
        }
    }
}
