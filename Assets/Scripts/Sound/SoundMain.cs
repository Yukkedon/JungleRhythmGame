using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMain : BaseSound
{
    [SerializeField] string songName = "";
    bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        LoadBGM(songName);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && !isStart)
        {
            MainManager.instance.isStart = true;
            MainManager.instance.startTime = Time.time;
            PlayBGM();
        }

    }
}
