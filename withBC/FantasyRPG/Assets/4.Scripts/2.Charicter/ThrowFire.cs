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
        bool IsLeftMouseUp = GetComponent<PlayerCtrl>().IsLeftMouseUp;
       
        if (IsLeftMouseUp)
            Instantiate(ThrownObj, FirePos.position,Quaternion.AngleAxis(90,Vector3.left));
    }
}
