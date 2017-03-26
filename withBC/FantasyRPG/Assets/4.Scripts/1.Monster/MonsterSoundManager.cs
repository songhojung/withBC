using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundManager : MonoBehaviour {

    [HideInInspector]
    public AudioSource MyAudio;

    [HideInInspector]
    public AudioClip Hit;

    [HideInInspector]
    public AudioClip Death;

    [HideInInspector]
    public AudioClip Stay;

    [HideInInspector]
    public AudioClip Move;

    [HideInInspector]
    public AudioClip Attack;

    [HideInInspector]
    public AudioClip Roar;


    public float NowVolum = 1.0f;
    // Use this for initialization
    void Start () {
        MyAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(MyAudio.volume != NowVolum)
        {
            MyAudio.volume = NowVolum;
        }
	}
}
