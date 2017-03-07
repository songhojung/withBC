using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionCam : MonoBehaviour
{
    private Ray ray;
    private RaycastHit rayHit;
    private float distance = 10.0f;
    private float DistanceForOrig = 0.0f;
    private float passedDistance = 0.0f;
    private float OrigDistance;
    private float OrigHeight;
    private Transform tr;
    private FollowCam camParent;
    private Transform Player;
    private Vector3 prevCamPos = Vector3.zero;
    private Vector3 LastCamPos = Vector3.zero;
    private bool IsHiting = false;
	// Use this for initialization
	void Start ()
    {
        ray = new Ray();
        tr = GetComponent<Transform>();
        camParent = tr.parent.gameObject.GetComponent<FollowCam>();
        OrigDistance = camParent.distance;
        OrigHeight = camParent.height;

    }
	
	// Update is called once per frame
	void Update ()
    {
        

        Raycast();
        
    }

    void Raycast()
    {
        ray.origin = tr.position;
        ray.direction = tr.forward;
        Physics.Raycast(ray, out rayHit, this.distance);

        if (this.rayHit.collider != null)
        {

            if (rayHit.collider.gameObject.tag == "Object")
            {
                moveCam();
            }
        }
        else
        {
            MoveCamToOriginPos();

        }
        SetRayDistance();
    }
    
    // 오브젝트랑 부딪힐떄 카메라 높이,거리값 감소시킴
    void moveCam()
    {
        //camParent = tr.parent.gameObject.GetComponent<FollowCam>();
        //camParent.distance -= 0.5f*Time.deltaTime;
        //camParent.height -= 0.1f * Time.deltaTime;

        float movement = Mathf.Lerp(0, rayHit.distance, 5 * Time.deltaTime);
        tr.position = tr.position + (tr.forward *movement);
        LastCamPos = rayHit.point;
        IsHiting = true;
    }

    // 레이에 오브젝트 충돌없으면 월래 카메라 높이,거리값으로 돌림
    void MoveCamToOriginPos()
    {
        //camParent = tr.parent.gameObject.GetComponent<FollowCam>();
        //if(OrigDistance != camParent.distance)
        //camParent.distance += 0.5f * Time.deltaTime;
        //if(OrigHeight != camParent.height)
        //camParent.height += 0.1f * Time.deltaTime;
        if (distance <= 19.23f)
        {
            float movement = Mathf.Lerp(0, 5, 5 * Time.deltaTime);
            tr.position = tr.position + (-tr.forward * movement);
            
        }
        else
        {
            IsHiting = false;
        }

    }
    
    //레이의 거리값을 캐릭터와 카메라의 사이 거리으로 조정
    void SetRayDistance()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        distance = Vector3.Distance(tr.position, Player.position);
    }

    void SaveInitCamPos()
    {
        if (prevCamPos == Vector3.zero)
        {
            
            prevCamPos = tr.position; //충돌직후 카메라위치 저장.
        }
    }


    private void OnDrawGizmos()
    {
        if (this.rayHit.collider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.rayHit.point, 1.0f);

            Gizmos.color = Color.black;
            Gizmos.DrawLine(this.transform.position,
                this.transform.position +
                this.transform.forward * this.rayHit.distance);

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.rayHit.point,
                        this.rayHit.point + this.rayHit.normal);

        }
        else
        {
            Gizmos.DrawLine(this.transform.position,
                this.transform.position +
                this.transform.forward * this.distance);

        }
    }
}
