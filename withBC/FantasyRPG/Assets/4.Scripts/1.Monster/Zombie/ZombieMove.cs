using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour {

    public ZombieAnimation ZombieAni;
    public Rigidbody Z_body;

    public float walkSpeed = 1.0f;
	// Use this for initialization
	void Start () {
        ZombieAni = GetComponent<ZombieAnimation>();
        Z_body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ZombieAni.NowState == ZombieAnimation.Z_STATE.Z_WALK)
        {
            Move();
        }
	}

    void Move()
    {
        switch(ZombieAni.NowState)
        {
            case ZombieAnimation.Z_STATE.Z_WALK:
                Vector3 VecZombie = (Vector3.forward * walkSpeed * Time.deltaTime) + Z_body.transform.position;
                Z_body.MovePosition(VecZombie);
                break;
        }
    }
}
