using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager sInstance;

    public static EffectManager Instance
    {
        get
        {
            if(sInstance == null)
            {
                GameObject newEffectManager = new GameObject("EffectManager");
                sInstance = newEffectManager.GetComponent<EffectManager>();
            }
            return
                sInstance;
        }
    }

    private void Awake()
    {
        sInstance = this;
    }

    // >>:==================================================

	public IEnumerator hit()
    {
        Debug.Log("aaaaaaaaaa");
        yield return null;

    }
    public IEnumerator CreatEffect(string path ,Vector3 Position, Quaternion rotate)
    {
        yield return new WaitForSeconds(0.7f);
        Instantiate(Resources.Load(path), Position, rotate);

        
    }

    public IEnumerator CreatEffect(string path, Vector3 Position, Quaternion rotate, float waitTime,float deadTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject Effect = (GameObject)Instantiate(Resources.Load(path, typeof(GameObject)),
            Position, rotate);

            Destroy(Effect, deadTime);
    }

    public GameObject CreatAndGetEffect(string path, Vector3 Position, Quaternion rotate, float waitTime)
    {
        WaitSeconds(waitTime);
        GameObject EffectObj = (GameObject)Instantiate(Resources.Load(path,typeof(GameObject)), Position, rotate);
        return EffectObj;
    }

    public IEnumerator DestroyEffect(GameObject Object, float time)
    {
        Destroy(Object, time);
        yield return null;
    }

    private void WaitSeconds(float time)
    {
        float ntime = 0.0f;
        while(true)
        {
            ntime += Time.deltaTime;
            if (ntime > time)
                break;
        }
    }
}
