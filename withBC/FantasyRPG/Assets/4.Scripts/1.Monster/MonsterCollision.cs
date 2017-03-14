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

        // 피 이펙트
          
            Vector3 EffectPos = new Vector3(collider.transform.position.x, collider.transform.position.y+1, collider.transform.position.z);
            GameObject Bloodeffect = EffectManager.Instance.CreatAndGetEffect("DecalBloodSplatEffect", 
                EffectPos, Quaternion.identity);

            Bloodeffect.transform.LookAt(this.gameObject.transform);
            Bloodeffect.transform.eulerAngles = new Vector3(Bloodeffect.transform.eulerAngles.x, Bloodeffect.transform.eulerAngles.y,
                collider.gameObject.transform.eulerAngles.z);

            Destroy(Bloodeffect, 0.5f);

        // 피 데칼
            Vector3 decalPos = gameObject.transform.position + (Vector3.up * 0.05f);
            Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0, 360));
          
            GameObject BloodDecal = EffectManager.Instance.CreatAndGetEffect("Blood04",
                decalPos, decalRot);
            float scale = Random.Range(2.0f, 4.0f);
            BloodDecal.transform.localScale = BloodDecal.transform.localScale * scale;

            Destroy(BloodDecal, 2.5f);

        }
       
    }
}
