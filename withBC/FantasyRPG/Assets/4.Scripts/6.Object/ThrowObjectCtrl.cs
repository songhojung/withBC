using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectCtrl : MonoBehaviour {

    public enum ThrowObjectType { ARROW, FIREBALL, FIRE,LIGHTNING, DRAGONFIRE}
    public ThrowObjectType throwObjType; 

    private Transform tr;
    public float moveSpeed = 20.0f;


    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
       

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVec = (tr.forward* moveSpeed * Time.deltaTime);
        tr.position = tr.position+moveVec;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Monster"))
        {

            if(throwObjType == ThrowObjectType.ARROW) // 아쳐 활
            {

            }
            else if (throwObjType == ThrowObjectType.FIREBALL) // 위자드 파이어볼
            {

                GameObject ExplosionFireEffect = EffectManager.Instance.CreatAndGetEffect("BigExplosionEffect"
                    , collider.gameObject.transform.position, Quaternion.identity);
                StartCoroutine(EffectManager.Instance.DestroyEffect(ExplosionFireEffect, 2.5f));
                StartCoroutine(EffectManager.Instance.DestroyEffect(this.gameObject, 2.5f));
            }
            else if (throwObjType == ThrowObjectType.FIRE) // 위자드 그냥 파이어
            {

            }
            else if (throwObjType == ThrowObjectType.LIGHTNING) // 위자드 라이트닝
            {

            }
            else if (throwObjType == ThrowObjectType.DRAGONFIRE) // 용이쓰는 불꽃
            {

            }

            
        }
    }
}
