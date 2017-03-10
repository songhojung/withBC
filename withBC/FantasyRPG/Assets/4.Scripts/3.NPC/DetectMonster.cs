using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectMonster : MonoBehaviour {

    //몬스터 찾았는지 못찾았는지
    public bool isMonster = false;

    public GameObject Monster = null;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!isMonster)
        {
            if (Monster)
                isMonster = true;
        }
        else
        {
            if (!Monster)
                isMonster = false;
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (!isMonster)
        {
            if (other.CompareTag("Monster"))
            {
                Monster = other.gameObject;
                isMonster = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == Monster)
        {
            Monster = null;
            isMonster = false;
        }
    }
}
