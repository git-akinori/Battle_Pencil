using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance;

    string nextBgmName;

    public AudioSource BgmSource;
    public AudioSource SeSource;

    //音源保存用
    Dictionary<string, AudioClip> bgms;
    Dictionary<string, AudioClip> ses;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        bgms = new Dictionary<string, AudioClip>();
        ses = new Dictionary<string, AudioClip>();

        object[] bgmList = Resources.LoadAll("Audio/BGM");
        object[] seList = Resources.LoadAll("Audio/SE");

        foreach (AudioClip bgm in bgmList)
        {
            bgms[bgm.name] = bgm;
            Debug.Log(bgm.name);
        }
        foreach (AudioClip se in seList)
        {
            ses[se.name] = se;
        }

    }


    public void PlayeSE(string seName, AudioSource audioSource)
    {
        if (!ses.ContainsKey(seName))
        {
            Debug.Log("そのSEはありません。");
            return;
        }
        audioSource.PlayOneShot(ses[seName]);
    }

    public void PlayeBgm(string bgmName)
    {
        if (!bgms.ContainsKey(bgmName))
        {
            Debug.Log("そのBGMはありません。");

            return;
        }
        nextBgmName = bgmName;
        //違う曲名が再生されたらフェードアウト
        if (BgmSource.clip.name != nextBgmName)
        {
            StartCoroutine(FeedOut());
        }
        else
        {
            BgmSource.clip = bgms[nextBgmName] as AudioClip;
            BgmSource.Play();
        }
    }

    IEnumerator FeedOut()
    {
        while (BgmSource.volume >= 0)
        {
            BgmSource.volume -= Time.deltaTime;

            if (BgmSource.volume <= 0)
            {
                BgmSource.Stop();
                BgmSource.volume = 1;
                StartCoroutine(FeedIN());
                yield break;
            }
            yield return null;
        }
    }

    IEnumerator FeedIN()
    {
        BgmSource.clip = bgms[nextBgmName] as AudioClip;
        BgmSource.Play();
        yield return null;

    }
}
