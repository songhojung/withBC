using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCollision : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
		
	}

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Weapon"))
        {
            Debug.Log("고블린 히트됨");
        }
    }
}
