using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionCam : MonoBehaviour
{
    private Transform tr;
    private Transform Player;
    private Ray ray;
    private RaycastHit rayHit;
    private float distance ;
    private float OrigRayDistance;
    private float passedDistance = 0.0f;
    private float diffDistance = 0.0f;
    private bool IsHiting = false;
    private bool readyforMove = false;
	// Use this for initialization
	void Start ()
    {
        ray = new Ray();
        tr = GetComponent<Transform>();

        StartCoroutine(SaveOrigRayDistance());
        


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
        if (readyforMove)
        {
            float movement = Mathf.Lerp(0, rayHit.distance, 5 * Time.deltaTime);
            tr.position = tr.position + (tr.forward * movement);

            IsHiting = true;
            passedDistance = 0.0f;
            diffDistance = 0.0f;
        }
    }

    // 레이에 오브젝트 충돌없으면 월래 카메라 높이,거리값으로 돌림
    void MoveCamToOriginPos()
    {
        //camParent = tr.parent.gameObject.GetComponent<FollowCam>();
        //if(OrigDistance != camParent.distance)
        //camParent.distance += 0.5f * Time.deltaTime;
        //if(OrigHeight != camParent.height)
        //camParent.height += 0.1f * Time.deltaTime;


           // 충돌됬고 ,초기레이거리값이 존재하고 ,초기 레이거리값이 현재거리값보다 크냐
           if (IsHiting && OrigRayDistance > 0  && distance <= OrigRayDistance) 
           {
               if (diffDistance <= 0) // 거리값차이가 초기화 안됫냐
               {
                   diffDistance = OrigRayDistance - distance; // diffdistance초기화
               }
               diffDistance -= passedDistance;
               float movement = Mathf.Lerp(0, diffDistance, 5 * Time.deltaTime);
               passedDistance = movement;
               tr.position = tr.position + (-tr.forward * movement);
               if (passedDistance < 0.000001f) // 
               {
                   IsHiting = false;
                   passedDistance = 0.0f;
                   diffDistance = 0.0f;
               }

           }


    }
    
    //레이의 거리값을 캐릭터와 카메라의 사이 거리으로 조정
    void SetRayDistance()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        distance = Vector3.Distance(tr.position, Player.position);
    }

    IEnumerator SaveOrigRayDistance()
    {
        yield return new WaitForSeconds(1.4f);
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        distance = Vector3.Distance(tr.position, Player.position);
        OrigRayDistance = distance;
        readyforMove = true;

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
