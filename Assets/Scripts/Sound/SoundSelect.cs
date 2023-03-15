using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSelect : BaseSound
{
    public static SoundSelect instance;
    public enum SE
    {
        Select,
        Cansel,
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySe()
    {
        PlaySE((int)SoundSelect.SE.Select);
    }
}
