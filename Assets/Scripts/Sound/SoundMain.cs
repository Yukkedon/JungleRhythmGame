using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMain : BaseSound
{

    // Start is called before the first frame update
    void Start()
    {
        LoadBGM(MainManager.instance.songName);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && !MainManager.instance.isStart)
        {
            MainManager.instance.isStart = true;
            MainManager.instance.startTime = Time.time;
            PlayBGM();
        }

    }
}
