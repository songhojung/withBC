using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundManager : MonoBehaviour {

    [HideInInspector]
    public AudioSource MyAudio;

    [HideInInspector]
    public AudioClip GoblinHit;
    [HideInInspector]
    public AudioClip WolfHit;


    [HideInInspector]
    public AudioClip WolfDeath;
    [HideInInspector]
    public AudioClip GoblinDeath;

    [HideInInspector]
    public AudioClip SpiderStay;

    [HideInInspector]
    public AudioClip GoblinAttack;
    [HideInInspector]
    public AudioClip WolfAttack;

    [HideInInspector]
    public AudioClip WolfSpawn;


    public float NowVolum = 1.0f;
    // Use this for initialization
    void Start () {
        MyAudio = GetComponent<AudioSource>();
        GoblinHit = SoundManager.Instance.GoblinHit;
        GoblinAttack = SoundManager.Instance.GoblinAttack;
        GoblinDeath = SoundManager.Instance.GoblinDie;

        WolfAttack = SoundManager.Instance.WolfAttack;
        GoblinAttack = SoundManager.Instance.GoblinAttack;

        SpiderStay = SoundManager.Instance.SpiderBasic;

        GoblinDeath = SoundManager.Instance.GoblinDie;
        WolfDeath = SoundManager.Instance.WolfDie;

        WolfSpawn = SoundManager.Instance.WolfRoar;

	}
	
	// Update is called once per frame
	void Update () {
		if(MyAudio.volume != NowVolum)
        {
            MyAudio.volume = NowVolum;
        }
	}
}
