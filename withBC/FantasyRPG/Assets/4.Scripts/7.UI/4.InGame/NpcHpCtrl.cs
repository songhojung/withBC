using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcHpCtrl : MonoBehaviour
{
    public enum HpTargetNpc { Wizard, Archor, Warrior};
    public HpTargetNpc hpTargetNpc = HpTargetNpc.Warrior;

    private UISlider UIHp;
    private CharacterInformation NpcCharacterInfo;


    void Start()
    {
        UIHp = GetComponent<UISlider>();

        //if(hpTargetNpc == HpTargetNpc.Warrior)
        //    NpcCharacterInfo = GameManager.Instance.Npc1.gameObject.GetComponent<CharacterInformation>();
        //else if(hpTargetNpc == HpTargetNpc.Archor)
        //    NpcCharacterInfo = GameManager.Instance.Npc2.gameObject.GetComponent<CharacterInformation>();
    }


    void Update()
    {
        if(NpcCharacterInfo ==null && GameManager.Instance.Npc1 != null && GameManager.Instance.Npc2 != null)
        {
            if (hpTargetNpc == HpTargetNpc.Warrior)
                NpcCharacterInfo = GameManager.Instance.Npc1.gameObject.GetComponent<CharacterInformation>();
            else if (hpTargetNpc == HpTargetNpc.Archor)
                NpcCharacterInfo = GameManager.Instance.Npc2.gameObject.GetComponent<CharacterInformation>();
        }

        if(NpcCharacterInfo != null)
        UIHp.sliderValue = NpcCharacterInfo.hp * 0.01f;

    }
}
