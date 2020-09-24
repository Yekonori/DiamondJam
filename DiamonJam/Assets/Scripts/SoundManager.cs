using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource knifeOut;
    public AudioSource killNPC;

    private IEnumerator fadeCoroutine;

    #region Singleton
    private static SoundManager _instance = null;

    public static SoundManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    //void Update()
    //{
        
    //}
    public void PlayMusic(AudioClip bgm)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        music.volume = 1f;
        music.clip = bgm;
        music.Play();
    }
    public void PlaySound(AudioClip sound)
    {
        knifeOut.PlayOneShot(sound);
    }


    public void FadeMusic()
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);
        fadeCoroutine = FadeMusicCoroutine();
        StartCoroutine(fadeCoroutine);
    }

    private IEnumerator FadeMusicCoroutine()
    {
        float t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime;
            music.volume = 1f - t;
            yield return null;
        }
    }

    void PlaySound(AudioSource audio)
    {
        audio.Play();
    }
}
