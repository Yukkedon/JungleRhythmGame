using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSound : MonoBehaviour
{
    //BGM
    [SerializeField] AudioSource audioSourceBGM;
    [SerializeField] AudioClip[] audioClipsBGM;

    //SE
    [SerializeField] AudioSource audioSourceSE;
    [SerializeField] AudioClip[] audioClipsSE;

    /// <summary>
    /// BGMの再生
    /// </summary>
    public void PlayBGM(int num)
    {
        // 列挙型から流したいBGMを選ぶ（intでキャスト）
        audioSourceBGM.clip = audioClipsBGM[(int)num];
        audioSourceBGM.Play();

    }

    public void StopBgm()
    {
        audioSourceBGM.Stop();
    }

    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="se"></param>
    public void PlaySE(int num)
    {
        audioSourceSE.PlayOneShot(audioClipsSE[num]);
    }



    public void LoadMusic(string path)
    {

    }
}
