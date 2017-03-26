using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSound : MonoBehaviour {

    public AudioSource MyAudio;

    [HideInInspector]
    public AudioClip Hit;

    [HideInInspector]
    public AudioClip Death;

    [HideInInspector]
    public AudioClip Walk;

    [HideInInspector]
    public AudioClip Fly;

    [HideInInspector]
    public AudioClip Attack;

    [HideInInspector]
    public AudioClip Fire;

    [HideInInspector]
    public AudioClip Roar;


    public float NowVolum = 1.0f;

    // Use this for initialization
    void Start () {
        MyAudio = GetComponent<AudioSource>();
        Hit = SoundManager.Instance.DragonHit;
        Walk = SoundManager.Instance.DragonWalk;
        Fly = SoundManager.Instance.DragonFly;
        Attack = SoundManager.Instance.DragonBite;
        Fire = SoundManager.Instance.DragonFire;
        Roar = SoundManager.Instance.DragonRoar;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (MyAudio.volume != NowVolum)
        {
            MyAudio.volume = NowVolum;
        }
        
    }
}
