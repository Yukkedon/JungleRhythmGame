using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMain : BaseSound
{
    [SerializeField] MainManager mainManager;

    public enum SE
    {
        Touch,
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadBGM(mainManager.songName);
    }

    // Update is called once per frame
    void Update()
    {

        if (mainManager.isStart && IsCheckEndBGM())
        {
            mainManager.SetStartTime(Time.time);
            PlayBGM();
        }

    }
}
