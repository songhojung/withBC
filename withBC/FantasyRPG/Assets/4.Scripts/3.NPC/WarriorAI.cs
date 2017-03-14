using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAI : MonoBehaviour
{

    private WarriorAnimationCtrl warriorAniCtrl;
    //private DetectMonster _detectMonster;
    //private MoveNPC _move;

    // Use this for initialization
    void Start()
    {
        warriorAniCtrl = GetComponent<WarriorAnimationCtrl>();
        //_detectMonster = GetComponent<DetectMonster>();
        //_move = GetComponent<MoveNPC>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.CompareTag("Monster"))
        {
           
            warriorAniCtrl.IsLeftMouseDown = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.CompareTag("Monster"))
        {

            warriorAniCtrl.IsLeftMouseDown = false;
        }
       
    }
}
