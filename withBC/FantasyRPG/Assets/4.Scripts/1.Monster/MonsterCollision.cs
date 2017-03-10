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
            Quaternion rotate = new Quaternion(collider.gameObject.transform.forward.x, -collider.gameObject.transform.forward.y, 0, collider.gameObject.transform.rotation.w);
            Vector3 EffectPos = collider.gameObject.transform.position;
                //Debug.Log(collider.gameObject.transform.eulerAngles);
            GameObject effect = (GameObject)Instantiate(Resources.Load("DecalBloodSplatEffect",typeof(GameObject)),
               EffectPos, rotate);

           
            Destroy(effect, 0.5f);
            
            
        }
    }
}
