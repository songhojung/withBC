using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundManager : MonoBehaviour {

    public AudioSource MyAudio;

    [HideInInspector]
    public AudioClip SwordManHit;
    [HideInInspector]
    public AudioClip HitByAxe;
    [HideInInspector]
    public AudioClip ArchorHit;
    [HideInInspector]
    public AudioClip WizardHit;
    [HideInInspector]
    public AudioClip HitByPunch;
    [HideInInspector]
    public AudioClip SwingSword;
    [HideInInspector]
    public AudioClip SwingStaff;
    [HideInInspector]
    public AudioClip Shoot;
    [HideInInspector]
    public AudioClip ShootFIre;

    public float NowVolum = 1.0f;
	// Use this for initialization
	void Start () {
        SwordManHit = SoundManager.Instance.SwordManHit;
        HitByAxe = SoundManager.Instance.HitByAxe;
        ArchorHit = SoundManager.Instance.ArchorHit;
        WizardHit = SoundManager.Instance.MagicianHit;
        HitByPunch = SoundManager.Instance.Punch;
        SwingSword = SoundManager.Instance.SwingSword;
        Shoot = SoundManager.Instance.Shoot;
        ShootFIre = SoundManager.Instance.Fireball;
        SwingStaff = SoundManager.Instance.SwingStaff;

    }
	
	// Update is called once per frame
	void Update () {
        if (MyAudio.volume != NowVolum)
            MyAudio.volume = NowVolum;
	}
}
