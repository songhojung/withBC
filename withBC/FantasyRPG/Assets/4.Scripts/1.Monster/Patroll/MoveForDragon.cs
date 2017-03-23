using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveForDragon : MonoBehaviour {


    public float Speed  = 1.0f;
    public int AngularSpeed = 90;
    public int Acceleration = 8;
    public float StopDistance = 0.0f;
    public bool OnOff = false;
    public GameObject target = null;
    private DragonAnimation DragonAni;
	// Use this for initialization
	void Start ()
    {
        //Nav_collider = GetComponent<Collider>();
        DragonAni = GetComponent<DragonAnimation>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (OnOff)
        {
            if (target)
            {
                if (DragonAni.NowState != DragonAnimation.D_STATE.D_FLY_FIRE)
                {
                    Vector3 direction = target.transform.position - transform.position;
                    /*Vector3 forward = Vector3.Slerp(transform.forward,
                                    direction,
                                    (AngularSpeed * Time.deltaTime) / Vector3.Angle(transform.forward, direction));*/
                    //transform.LookAt(transform.position + forward);

                    Quaternion drot = Quaternion.LookRotation(direction);
                    float timeSpeed = AngularSpeed * Time.deltaTime / Quaternion.Angle(transform.rotation, drot);
                    Quaternion rot = Quaternion.Slerp(transform.rotation, drot, timeSpeed);

                    transform.rotation = rot;
                }
                //Debug.Log(this.name);
            }
        }
        else
        {
            if (target)
            {
                target = null;
                //Debug.Log(this.name);
            }

        }
	}
}