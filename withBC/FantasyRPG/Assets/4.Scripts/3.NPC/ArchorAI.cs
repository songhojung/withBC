using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchorAI : MonoBehaviour
{

    private bool isDie = false;
    private bool isRun = false;
    private bool isCombo1 = false;
    private bool isCombo2 = false;
    private Vector3 Direction = Vector3.zero;
    private bool IsLeftMouseDown = false;
    private bool IsRightMouseDown = false;
    private bool IsJump = false;

    

    // Use this for initialization
    void Start ()
    {
        

    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Monster"))
        {
            
        }
        else
        {
            
        }
    }
}
