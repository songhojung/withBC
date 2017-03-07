﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCam : MonoBehaviour {

    private Ray ray;
    private RaycastHit rayHit;
    private Transform tr;
    private Transform CameraTr;
    private float distance = 20.0f;
    private float origDistance = 0.0f;
    private float origHeight = 0.0f;
    private float camera_dist = 0.0f;
    private Vector3 dir;
    private bool isHit = false;
    private Vector3 prevCamPos = Vector3.zero;
    public FollowCam followCam;
    Vector3 ray_target;
    // Use this for initialization
    void Start ()
    {
        ray = new Ray();
        tr = GetComponent<Transform>();

        origDistance = followCam.distance;
        origHeight = followCam.height;
        CameraTr = GameObject.FindGameObjectWithTag("MainCamera").transform;

        camera_dist = Mathf.Sqrt(origDistance * origDistance + origHeight * origHeight);
        dir = new Vector3(0, origHeight, origDistance).normalized;
    }
	
	// Update is called once per frame
	void Update ()
    {

        
        ray.origin = tr.position + new Vector3(0, 5.0f, 0);
        ray.direction = CameraTr.position - ray.origin;
        //ray.direction = (tr.up * origHeight) + (tr.forward * origDistance);
         ray_target = CameraTr.position - ray.origin;





        Raycast();
        SaveRayDistance();
    }
    
    void Raycast()
    {


        Physics.Raycast(ray, out rayHit, distance);
        //Physics.Raycast(tr.position + new Vector3(0, 5.0f, 0), ray_target, out rayHit, distance);

        if (this.rayHit.collider != null)
        {

            if (rayHit.collider.gameObject.tag == "Object")
            {
                if(distance > origDistance)
                {
                    MoveCamToOriginPos();
                }
                moveCam();
            }
        }
        else
        {
            MoveCamToOriginPos();
            

        }
    }
    void moveCam()
    {
        //float t = Time.deltaTime / 0.000001f;
        ////float valueDistance = Mathf.LerpUnclamped(0, 0.1f, 50);
        ////float valueHeight = Mathf.LerpUnclamped(0, 0.025f, 50);
        //followCam.distance -= 10.0f * Time.deltaTime;
        //followCam.height -= 2.5f * Time.deltaTime;
        CameraTr.position = rayHit.point + ray.direction.normalized;
       // CameraTr.Translate(-dir);
        //Debug.Log("충돌");

    }

    void MoveCamToOriginPos()
    {
        //float t = Time.deltaTime / 0.000001f;
        //if (origDistance > followCam.distance)
        //{
        //    //float valueDistance = Mathf.LerpUnclamped(0, 0.1f, 50);
        //    followCam.distance += 10.0f * Time.deltaTime;
        //}
        //if (origHeight > followCam.height)
        //{
        //    //float valueHeight = Mathf.LerpUnclamped(0, 0.025f, 50);
        //    followCam.height += 2.5f * Time.deltaTime;
        //}
        CameraTr.localPosition = Vector3.zero;
        //CameraTr.Translate(dir * camera_dist);
        CameraTr.Translate(-dir);

        //Debug.Log("충돌아니다");
    }

    void SaveRayDistance()
    {
        distance = Vector3.Distance(ray.origin, CameraTr.position);
    }


    private void OnDrawGizmos()
    {
        if (this.rayHit.collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.rayHit.point, 1.0f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.ray.origin,
                this.CameraTr.position);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.rayHit.point,
                        this.rayHit.point + this.rayHit.normal);

            //Gizmos.color = Color.cyan;
            //Gizmos.DrawLine(ray.origin,
            //    ray.origin + (-ray.direction * distance));

        }
        else
        {
            CameraTr = GameObject.FindGameObjectWithTag("MainCamera").transform;
            Gizmos.DrawLine(this.ray.origin,
                this.CameraTr.position );

            

        }
    }
}
