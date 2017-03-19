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
                EffectPos, Quaternion.identity,0.0f);

            Bloodeffect.transform.LookAt(this.gameObject.transform);
            Bloodeffect.transform.eulerAngles = new Vector3(Bloodeffect.transform.eulerAngles.x, Bloodeffect.transform.eulerAngles.y,
                collider.gameObject.transform.eulerAngles.z);

            Destroy(Bloodeffect, 0.5f);

        // 피 바닥데칼
            Vector3 decalPos = gameObject.transform.position + (Vector3.up * 0.05f);
            Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0, 360));
          
            GameObject BloodDecal = EffectManager.Instance.CreatAndGetEffect("Blood04",
                decalPos, decalRot, 0.0f);
            float scale = Random.Range(2.0f, 4.0f);
            BloodDecal.transform.localScale = BloodDecal.transform.localScale * scale;

            Destroy(BloodDecal, 2.5f);
            


        }
        else if(collider.gameObject.CompareTag("ThrowObject"))
        {
            ThrowObjectCtrl.ThrowObjectType objectType = collider.gameObject.GetComponent<ThrowObjectCtrl>().throwObjType;

            if (objectType == ThrowObjectCtrl.ThrowObjectType.ARROW) // 아쳐 활 타격시 이펙트
            {
                StartCoroutine(EffectManager.Instance.CreatEffect("BloodSplatEffect",
                        this.gameObject.transform.position, Quaternion.identity, 0.0f, 0.5f));

                collider.gameObject.GetComponent<ThrowObjectCtrl>().moveSpeed = 0.0f;
                collider.gameObject.transform.parent = this.gameObject.transform;
                Destroy(collider.gameObject, 15.0f);
            }
            else if (objectType == ThrowObjectCtrl.ThrowObjectType.FIREBALL) // 위자드 파이어볼 타격시 이펙트
            {

                StartCoroutine(EffectManager.Instance.CreatEffect("BigExplosionEffect",
                         collider.gameObject.transform.position, Quaternion.identity, 0.0f, 2.5f));

                StartCoroutine(EffectManager.Instance.DestroyEffect(collider.gameObject, 0.0f));
            }
            else if (objectType == ThrowObjectCtrl.ThrowObjectType.FIRE) // 위자드 그냥 파이어 타격시 이펙트
            {

                StartCoroutine(EffectManager.Instance.CreatEffect("Hit_Fire",
                          collider.gameObject.transform.position, Quaternion.identity, 0.0f, 1.8f));

                StartCoroutine(EffectManager.Instance.DestroyEffect(collider.gameObject.transform.parent.gameObject, 0.0f));
                
            }
            else if (objectType == ThrowObjectCtrl.ThrowObjectType.LIGHTNING) // 위자드 라이트닝 타격시 이펙트
            {
                StartCoroutine(EffectManager.Instance.CreatEffect("Hit_Lightning",
                         this.gameObject.transform.position, Quaternion.identity, 0.3f, 1.8f));
            }

        }
    }
}
