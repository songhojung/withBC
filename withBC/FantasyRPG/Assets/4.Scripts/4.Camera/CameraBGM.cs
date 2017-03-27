using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBGM : MonoBehaviour {

    public AudioSource MyAudio;

    [HideInInspector]
    public AudioClip Title;

    [HideInInspector]
    public AudioClip Field;

    [HideInInspector]
    public AudioClip Boss;

    [HideInInspector]
    public AudioClip Select;

    public float NowVolume = 1.0f;
    public float Fade = 1.0f;
    public float MaxFade = 1.0f;
	// Use this for initialization
	void Start () {
        Title = SoundManager.Instance.TitleSceen;
        Field = SoundManager.Instance.FieldSceen;
        Boss = SoundManager.Instance.BossSceen;
        Select = SoundManager.Instance.WaitRoom;

        switch (GameManager.Instance.NowScene)
        {
            case GameManager.SCENE.TitleScene:
                MyAudio.Stop();
                MyAudio.clip = Title;
                MyAudio.Play();
                break;
            case GameManager.SCENE.InGameScene:
                MyAudio.Stop();
                MyAudio.clip = Select;
                MyAudio.Play();
                break;
            case GameManager.SCENE.WaitScene:
                MyAudio.Stop();
                MyAudio.clip = Select;
                MyAudio.Play();
                break;
            case GameManager.SCENE.BossScene:
                MyAudio.Stop();
                MyAudio.clip = Field;
                MyAudio.Play();
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		if(MyAudio.volume != NowVolume)
        {
            MyAudio.volume = NowVolume;
        }
        switch (GameManager.Instance.NowScene)
        {
            case GameManager.SCENE.TitleScene:
                if (MyAudio.clip != Title)
                {
                    MyAudio.Stop();
                    MyAudio.clip = Field;
                    MyAudio.Play();
                }
                else
                {
                    if (Fade < MaxFade)
                    {
                        Fade += Time.deltaTime / 3.0f ;
                        NowVolume = Fade;
                    }
                }
                break;
            case GameManager.SCENE.InGameScene:
                if (MyAudio.clip != Field)
                {
                    Fade -= Time.deltaTime / 3.0f ;
                    NowVolume = Fade;
                    if (Fade <= 0.0f)
                    {
                        MyAudio.Stop();
                        MyAudio.clip = Field;
                        MyAudio.Play();
                    }
                }
                else
                {
                    if (Fade < MaxFade)
                    {
                        Fade += Time.deltaTime / 3.0f ;
                        NowVolume = Fade;
                    }
                }
                break;
            case GameManager.SCENE.WaitScene:

                if (Fade <= 0.0f)
                {
                    MyAudio.Stop();
                    MyAudio.clip = Select;
                    MyAudio.Play();
                }
                else
                {
                    if (Fade < MaxFade)
                    {
                        Fade += Time.deltaTime / 3.0f;
                        NowVolume = Fade;
                    }
                }
                break;
            case GameManager.SCENE.BossScene:
                if (MyAudio.clip != Boss)
                {
                    Fade -= Time.deltaTime / 3.0f ;
                    NowVolume = Fade;
                    if (Fade <= 0.0f)
                    {
                        MyAudio.Stop();
                        MyAudio.clip = Boss;
                        MyAudio.Play();
                    }
                }
                else
                {
                    if (Fade < MaxFade)
                    {
                        Fade += Time.deltaTime / 3.0f ;
                        NowVolume = Fade;
                    }
                }
                break;
        }
    }
}
