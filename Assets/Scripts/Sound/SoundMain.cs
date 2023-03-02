using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMain : BaseSound
{
    [SerializeField] string songName = "";
    // Start is called before the first frame update
    void Start()
    {
        
        LoadBGM(songName,true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayBGM();
        }

    }
}
