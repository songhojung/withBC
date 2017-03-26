using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMove : MonoBehaviour {

    private DragonAnimation DragonAni;
    private Rigidbody Dragon_body;

    private DragonDetectTarget DetectTarget;
    private MonsterDetectCollider DetectColl;
    public float DragonwalkSpeed = 10.0f;
    // Use this for initialization
    void Start()
    {
        DragonAni = GetComponent<DragonAnimation>();
        Dragon_body = GetComponent<Rigidbody>();
        DetectTarget = GetComponent<DragonDetectTarget>();
        DetectColl = GetComponent<MonsterDetectCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.name);
        switch (DragonAni.NowFlyRand)
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

                if (!(DetectColl.FindPlayer &&
                    Vector3.Distance(DetectTarget.target.transform.position, this.transform.position) <= 5.0f))
                {
                    Vector3 VecGoblin2 = (Dragon_body.transform.forward * DragonwalkSpeed * 1.5f * Time.deltaTime) + Dragon_body.transform.position;
                    Dragon_body.MovePosition(VecGoblin2);

                }
                //Dragon_body.transform.LookAt(VecGoblin2);
                break;
            case DragonAnimation.D_STATE.D_RUN:

                if (!(DetectColl.FindPlayer &&
                    Vector3.Distance(DetectTarget.target.transform.position, this.transform.position) <= 5.0f))
                {
                    Vector3 VecGoblin2 = (Dragon_body.transform.forward * DragonwalkSpeed * 1.5f * Time.deltaTime) + Dragon_body.transform.position;
                    Dragon_body.MovePosition(VecGoblin2);
                
                }
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
                Vector3 VecGoblin = (Dragon_body.transform.forward * DragonwalkSpeed * Time.deltaTime) + Dragon_body.transform.position;
                Dragon_body.MovePosition(VecGoblin);

                //Dragon_body.transform.LookAt(VecGoblin);
                break;
            case DragonAnimation.D_STATE.D_FLY_FAST:
                Vector3 VecGoblin2 = (Dragon_body.transform.forward * DragonwalkSpeed * 1.5f * Time.deltaTime) + Dragon_body.transform.position;
                Dragon_body.MovePosition(VecGoblin2);
                //Vector3 VecLerp = Vector3.Lerp(Dragon_body.transform.position,DetectTarget.)
                //DetectTarget.target.transform.position;
                //Dragon_body.transform.LookAt(VecGoblin2);
                break;
        }
    }
}
