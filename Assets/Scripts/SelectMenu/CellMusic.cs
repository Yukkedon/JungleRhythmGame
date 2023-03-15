using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMusic : BaseSound
{
    public enum SE
    {
        Select,
    }

    public void PlaySe()
    {
        PlaySE((int)SoundSelect.SE.Select);
    }
}
