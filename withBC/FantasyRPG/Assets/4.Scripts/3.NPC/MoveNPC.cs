using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveNPC : MonoBehaviour {


    public enum PlayerJob { NONE, WARRIOR, ARCHER, WIZARD};
    public PlayerJob Job = PlayerJob.NONE;

    public enum PlayerState { Follow, Detect, Attack, Search};
    public PlayerState NowState = PlayerState.Follow;

    private Rigidbody rigibody;

    //private float h = 0.0f;
    //private float v = 0.0f;

    [System.NonSerialized]
    public Vector3 direction = Vector3.zero;
    [System.NonSerialized]
    public bool IsLeftMouseDown = false;
    [System.NonSerialized]
    public bool IsLeftMouseUp = false;
    [System.NonSerialized]
    public bool IsLeftMouseStay = false;
    [System.NonSerialized]
    public bool IsRightMouseDown = false;
    [System.NonSerialized]
    public bool IsJump = false;
    [System.NonSerialized]
    public bool IsNumKey_1 = false;
    [System.NonSerialized]
    public bool IsNumKey_2 = false;
    [System.NonSerialized]
    public bool IsKey_E = false;
    [System.NonSerialized]
    public bool IsKey_Q = false;
    [System.NonSerialized]
    public bool IsKey_Shift = false;
    [System.NonSerialized]
    public bool AttackState = false;

    [System.NonSerialized]
    public bool isMove = false;


    public float moveSpeed = 5.0f;
    public float rotateSpeed = 5.0f;

    public bool isMonster = false;

    private DetectMonster DetectMon;

    public GameObject TargetPoint;

    public NavMeshAgent TargetNav;

    public List<GameObject> TargetMonster = new List<GameObject>();
    public GameObject NearestMonster;
    //public GameObject[] TargetMonster;

    public float AngularSpeed = 5.0f;

    // Use this for initialization
    void Start () {
        DetectMon = GetComponent<DetectMonster>();
        rigibody = GetComponent<Rigidbody>();
        //TargetNav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        isMonster = DetectMon.FollowMonster;

        if(!isMove)
            direction = Vector3.zero;
        MoveAi();
        
	}

    private void MoveAi()
    {
        switch(NowState)
        {
            case PlayerState.Search:
                break;
            case PlayerState.Follow:
                FollowMove();
                break;
            case PlayerState.Detect:
                DetectMove();
                break;
        }
    }

    private void FollowMove()
    {
        if (!isMonster)
        {
            if (TargetPoint)
            {
                //TargetNav.destination = TargetPoint.transform.position;

                if(TargetNav.enabled)
                {
                    if(Vector3.Distance(TargetNav.destination,TargetPoint.transform.position) >= 1.0f)
                        TargetNav.destination = TargetPoint.transform.position;
                    Vector3 direct = rigibody.transform.forward;
                    //rigibody.MovePosition(rigibody.position + direct * moveSpeed * Time.deltaTime);
                    direction = direct;
                }
                
                
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<NpcPatroll>())
        {
            if (other.GetComponent<NpcPatroll>().PointJob == Job)
            {
                if (TargetNav)
                {
                    if (TargetNav.enabled)
                        TargetNav.enabled = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<NpcPatroll>())
        {
            if (other.GetComponent<NpcPatroll>().PointJob == Job)
            {
                if (TargetNav)
                {
                    if (!TargetNav.enabled)
                    {
                        TargetNav.enabled = true;
                    }
                }
            }
        }
    }

    private void SearchMove()
    {

    }

    private void DetectMove()
    {
        if (isMonster)
        {
            if (TargetNav)
            {
                if (TargetNav.enabled)
                    TargetNav.enabled = true;
            }
            if (TargetMonster.Count>0)
            {
                if (DetectMon.FollowMonster)
                {
                    if (NearestMonster)
                        TargetNav.destination = NearestMonster.transform.position;
                } 
            }
        }
    }

    private void AttackMove()
    {

    }
}
