using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFire : MonoBehaviour {

    public GameObject DragonFlame;
    private DragonAnimation D_Animation;
    private GameObject fireEffect;


    // Use this for initialization
    void Start () {
        D_Animation = GetComponentInParent<DragonAnimation>();
	}
	
	// Update is called once per frame
	void Update () {
		
        switch(D_Animation.NowState)
        {
            case DragonAnimation.D_STATE.D_FIRE:
                if(D_Animation.OnceAttackCheck)
                {
                    //if (D_Animation.Dragon["breath fire"].normalizedTime >= 0.3f)
                    {
                         fireEffect = (GameObject)EffectManager.Instance.CreatAndGetEffect("DragonFire",
                            this.gameObject.transform.position, this.gameObject.transform.rotation, 0.7f);
                        Destroy(fireEffect, 2.0f);

                        //Instantiate(DragonFlame);
                        D_Animation.OnceAttackCheck = false;
                    }
                }
                if (fireEffect != null)
                    fireEffect.transform.position = this.gameObject.transform.position;
                break;
            case DragonAnimation.D_STATE.D_FLY_FIRE:
                if (D_Animation.OnceAttackCheck)
                {
                    StartCoroutine(EffectManager.Instance.CreatEffect("DragonFire",
                          this.gameObject.transform.position, this.gameObject.transform.rotation, 0.0f, 3.5f));

                    //Instantiate(DragonFlame);
                    D_Animation.OnceAttackCheck = false;
                }
                break;

                
        }

        
    }
}
