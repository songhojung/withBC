using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControll : MonoBehaviour {

    private AudioSource MyAudio;
	// Use this for initialization
	void Start () {

        MyAudio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(MyAudio.isPlaying)
        {

        }
	}
}
