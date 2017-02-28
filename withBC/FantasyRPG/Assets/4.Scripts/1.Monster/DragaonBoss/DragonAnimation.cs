using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAnimation : MonoBehaviour {

    public enum D_STATE
    {
        D_STAY, D_IDLE, D_ATT1,D_ATT2,D_FIRE,
        D_TAIL, D_HIT1, D_HIT2, D_DEATH, D_WALK, D_RUN, D_FLY,
        D_FLY_ATT, D_FLY_FIRE, D_FLY_FAST, D_FLY_HIT, D_FLY_DEATH,
        D_WALK_ROOT,D_WALK_ROTATE,D_EAT

    };

    public enum D_FLYRAND
    {
        NOWRAND,NOWFLY
    };
    public D_STATE NowState;

    public D_FLYRAND NowFlyRand;

    public int AttackNum;
    public int _health = 20;
    public int _hitNum;

    private Animation Dragon;

    // Use this for initialization
    void Start () {
        Dragon = GetComponent<Animation>();
        NowState = D_STATE.D_STAY;
        NowFlyRand = D_FLYRAND.NOWRAND;

        Dragon.wrapMode = WrapMode.Loop;
        Dragon.CrossFade("stand", 0.3f);

        AttackNum = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
