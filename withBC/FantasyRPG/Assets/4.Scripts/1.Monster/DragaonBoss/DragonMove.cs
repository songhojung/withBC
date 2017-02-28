using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMove : MonoBehaviour {

    public DragonAnimation DragonAni;
    public Rigidbody Dragon_body;

    public float walkSpeed = 1.0f;
    // Use this for initialization
    void Start()
    {
        DragonAni = GetComponent<DragonAnimation>();
        Dragon_body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (DragonAni.NowState == DragonAnimation.D_STATE.D_WALK ||
            DragonAni.NowState == DragonAnimation.D_STATE.D_RUN)
        {
            RandMove();
        }

        if (DragonAni.NowState == DragonAnimation.D_STATE.D_FLY ||
            DragonAni.NowState == DragonAnimation.D_STATE.D_FLY_FAST)
        {
            FlyMove();
        }
    }

    void RandMove()
    {
        switch (DragonAni.NowState)
        {
            case DragonAnimation.D_STATE.D_WALK:
                Vector3 VecGoblin = (Dragon_body.transform.forward * walkSpeed * Time.deltaTime) + Dragon_body.transform.position;
                Dragon_body.MovePosition(VecGoblin);

                Dragon_body.transform.LookAt(VecGoblin);
                break;
            case DragonAnimation.D_STATE.D_RUN:
                Vector3 VecGoblin2 = (Dragon_body.transform.forward * walkSpeed * 1.5f * Time.deltaTime) + Dragon_body.transform.position;
                Dragon_body.MovePosition(VecGoblin2);

                //Dragon_body.transform.LookAt(VecGoblin2);
                break;
        }
    }

    void FlyMove()
    {
        switch (DragonAni.NowState)
        {
            case DragonAnimation.D_STATE.D_FLY:
                Vector3 VecGoblin = (Dragon_body.transform.forward * walkSpeed * Time.deltaTime) + Dragon_body.transform.position;
                Dragon_body.MovePosition(VecGoblin);

                Dragon_body.transform.LookAt(VecGoblin);
                break;
            case DragonAnimation.D_STATE.D_FLY_FAST:
                Vector3 VecGoblin2 = (Dragon_body.transform.forward * walkSpeed * 1.5f * Time.deltaTime) + Dragon_body.transform.position;
                Dragon_body.MovePosition(VecGoblin2);

                //Dragon_body.transform.LookAt(VecGoblin2);
                break;
        }
    }
}
