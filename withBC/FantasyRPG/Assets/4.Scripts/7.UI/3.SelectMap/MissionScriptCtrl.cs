using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionScriptCtrl : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}
	


    void OnOffMissionScript()
    {
        if(gameObject.active)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
