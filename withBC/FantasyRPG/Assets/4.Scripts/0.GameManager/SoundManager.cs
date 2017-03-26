using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private static SoundManager sInstance;
    public static SoundManager Instance
    {
        get
        {
            if (sInstance == null)
            {

                GameObject newSoundManager = new GameObject("SoundManager");
                sInstance = newSoundManager.GetComponent<SoundManager>();
            }
            return
                sInstance;
        }
    }

    public AudioClip HitBySpider;
    public AudioClip SwordManHit;
    public AudioClip HitByAxe;
    public AudioClip GoblinHit;
    public AudioClip ArchorHit;
    public AudioClip WolfRoar;
    public AudioClip ElectricMagic;
    public AudioClip HitByElectric;
    public AudioClip MagicianHit;
    public AudioClip Punch;
    public AudioClip HitByStick;
    public AudioClip Fireball;

    public AudioClip DragonFly;
    public AudioClip DragonWalk;
    public AudioClip DragonHit;
    public AudioClip DragonBite;
    public AudioClip DragonFire;
    public AudioClip DragonSpawn;
    public AudioClip DragonRoar;

    public AudioClip TitleSceen;
    public AudioClip BossSceen;
    public AudioClip FieldSceen;
    public AudioClip WaitRoom;
    public AudioClip EffectFire;

    private void Awake()
    {
        sInstance = this;
        //DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
