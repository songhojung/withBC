using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAI : MonoBehaviour
{

    private WarriorAnimationCtrl warriorAniCtrl;

  

    // Use this for initialization
    void Start()
    {
        warriorAniCtrl = GetComponent<WarriorAnimationCtrl>();

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
