using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class BaseSound : MonoBehaviour
{

    protected AsyncOperationHandle snd;
    [SerializeField] AudioSource bgmSource;
    [SerializeField] List<AudioClip> bgmClip;

    [SerializeField] AudioSource seSuorce;
    [SerializeField] List<AudioClip> seClip;

    protected void LoadBGM(string songName, bool isAll = false)
    {
        // Select画面の音楽データをロードするかしないか
        if (isAll)
        {
            Addressables.LoadAssetsAsync<AudioClip>("SelectMusic", null).Completed += snd =>
            {
                 foreach (var clip in snd.Result)
                 {
                    bgmClip.Add((AudioClip)clip);
                    Debug.Log(clip.name);
                 }
                Addressables.Release(snd);
             };
        }
        if (!isAll)
        {
            snd = Addressables.LoadAssetAsync<AudioClip>($"Assets/Resource/Musics/{songName}.wav");
            var soundLoad = snd.WaitForCompletion();
            bgmClip.Add((AudioClip)snd.Result);
            Addressables.Release(snd);
        }
    }

    public void PlayBGM(int num = 0)
    {
        bgmSource.clip = bgmClip[num];
        bgmSource.Play();
    }

    public void PlaySE(int num)
    {
        seSuorce.PlayOneShot(seClip[num]);
    }

    public bool IsCheckEndBGM()
    {
        if (!bgmSource.isPlaying)
        {
            return true;
        }
        return false;
    }

    public void OnDestroy()
    {
        if (snd.IsValid())
            Addressables.Release(snd);
/*        if (this != null)
            Destroy(this);*/
    }

}
