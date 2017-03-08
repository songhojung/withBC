using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMove : MonoBehaviour {

    public DragonAnimation DragonAni;
    public Rigidbody Dragon_body;

    private DragonDetectTarget DetectTarget;

    public float walkSpeed = 1.0f;
    // Use this for initialization
    void Start()
    {
        DragonAni = GetComponent<DragonAnimation>();
        Dragon_body = GetComponent<Rigidbody>();
        DetectTarget = GetComponent<DragonDetectTarget>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(DragonAni.NowFlyRand)
        {
            case DragonAnimation.D_FLYRAND.NOWRAND:
                if(Dragon_body.isKinematic)
                    Dragon_body.isKinematic = false;
                if(!Dragon_body.useGravity)
                    Dragon_body.useGravity = true;
                RandMove();
                break;
            case DragonAnimation.D_FLYRAND.NOWFLY:
                if (!Dragon_body.isKinematic)
                    Dragon_body.isKinematic = true;
                if (Dragon_body.useGravity)
                    Dragon_body.useGravity = false;
                FlyMove();
                break;
        }
    }

    void RandMove()
    {
        switch (DragonAni.NowState)
        {
            case DragonAnimation.D_STATE.D_WALK:
                Vector3 VecGoblin = (Dragon_body.transform.forward * walkSpeed * Time.deltaTime) + Dragon_body.transform.position;
                Dragon_body.MovePosition(VecGoblin);

                //Dragon_body.transform.LookAt(VecGoblin);
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
        //if(DragonAni.NowFlyRand != DragonAnimation.D_FLYRAND.NOWFLY)
        //{
        //    DragonAni.NowFlyRand = DragonAnimation.D_FLYRAND.NOWFLY;
        //}
        switch (DragonAni.NowState)
        {
            case DragonAnimation.D_STATE.D_FLY:
                Vector3 VecGoblin = (Dragon_body.transform.forward * walkSpeed * Time.deltaTime) + Dragon_body.transform.position;
                Dragon_body.MovePosition(VecGoblin);

                //Dragon_body.transform.LookAt(VecGoblin);
                break;
            case DragonAnimation.D_STATE.D_FLY_FAST:
                Vector3 VecGoblin2 = (Dragon_body.transform.forward * walkSpeed * 1.5f * Time.deltaTime) + Dragon_body.transform.position;
                Dragon_body.MovePosition(VecGoblin2);
                //Vector3 VecLerp = Vector3.Lerp(Dragon_body.transform.position,DetectTarget.)
                //DetectTarget.target.transform.position;
                //Dragon_body.transform.LookAt(VecGoblin2);
                break;
        }
    }
}
