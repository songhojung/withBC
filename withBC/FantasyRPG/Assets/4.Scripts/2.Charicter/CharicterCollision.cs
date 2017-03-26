using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharicterCollision : MonoBehaviour
{

    private CharacterInformation charaterInfo;

    private void Start()
    {
        charaterInfo = GetComponent<CharacterInformation>();
    }
    private void Update()
    {
        //GetComponent<WizardAnimationCtrl>().wizardState = WizardAnimationCtrl.WizardState.HIT_1;
    }


    private void OnTriggerEnter(Collider collider)
    {
        //<<: 몬스터 충돌 <<<<<<<<<<<< /////////////

        if(collider.gameObject.CompareTag("MonsterAtt"))
        {
            MonsterInformation CollierMosterInfo = collider.gameObject.transform.root.gameObject.GetComponent<MonsterInformation>();
            if (CollierMosterInfo.isOnceAttack)
            {
                if(charaterInfo.Job == CharacterInformation.PlayerJob.WIZARD)
                {
                    GetComponent<WizardAnimationCtrl>().wizardState = WizardAnimationCtrl.WizardState.HIT_1;
                }
                else if (charaterInfo.Job == CharacterInformation.PlayerJob.WARRIOR)
                {
                    GetComponent<WarriorAnimationCtrl>().warriorState = WarriorAnimationCtrl.WarriorState.HIT;
                }
                else if (charaterInfo.Job == CharacterInformation.PlayerJob.ARCHER)
                {
                    GetComponent<ArcherAnimationCtrl>().archerState = ArcherAnimationCtrl.ArcherState.HITFRONT;
                }

                CollierMosterInfo.isOnceAttack = false;
                
                Vector3 EffectPos = new Vector3(collider.transform.position.x, collider.transform.position.y + 1, collider.transform.position.z);
                GameObject Bloodeffect = EffectManager.Instance.CreatAndGetEffect("DecalBloodSplatEffect",
                    EffectPos, Quaternion.identity, 0.0f);

                Bloodeffect.transform.LookAt(this.gameObject.transform);
                Bloodeffect.transform.eulerAngles = new Vector3(Bloodeffect.transform.eulerAngles.x, Bloodeffect.transform.eulerAngles.y,
                    collider.gameObject.transform.eulerAngles.z);
                GetComponent<CharacterInformation>().isHit = true;
                Destroy(Bloodeffect, 0.5f);
                charaterInfo.hp -= 10;
                Debug.Log(charaterInfo.hp+gameObject.name);
            }
        }
    }

}
