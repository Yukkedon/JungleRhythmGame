using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMain : BaseSound
{



    // Start is called before the first frame update
    void Start()
    {
        base.LoadMusic("GurennoYumiya");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            base.audioSourceBGM.Play();
        }
        
    }
}
