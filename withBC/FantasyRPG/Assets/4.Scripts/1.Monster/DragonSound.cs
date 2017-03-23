using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSound : MonoBehaviour {

    public AudioSource MyAudio;

    public AudioClip Hit;
    public AudioClip Death;
    public AudioClip Stay;
    public AudioClip Move;
    public AudioClip Fly;
    public AudioClip Attack;
    public AudioClip Fire;
    public AudioClip Roar;


    public float NowVolum = 1.0f;

    // Use this for initialization
    void Start () {
        MyAudio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (MyAudio.volume != NowVolum)
        {
            MyAudio.volume = NowVolum;
        }
    }
}
