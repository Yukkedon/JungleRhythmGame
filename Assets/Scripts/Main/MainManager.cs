using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance = null;

    public int MAX_RAITO_POINT = 5;
    public int MAX_DIGIT_POINT = 1000000;
    public int PERFECT_POINT   = 5;



    public bool isStart      = false;
    public float startTime   = 0;   // スタートボタンを押すまでの秒数を保存
    public float playerScore = 0;
    public float maxScore    = 0;
    public int point = 0;
    
    int combo = 0;

    int perfect  = 0;
    int great    = 0;
    int bad      = 0;
    int miss     = 0;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void Start()
    {
        Debug.Log(instance.isStart);
    }


    public void ResetCombo()
    {
        combo = 0;
    }
    public void AddCombo()
    {
        combo++;
    }

    
    // 0:perfect 1:great 2:bad 3:miss
    public void AddJudgeCount(int num)
    {
        switch (num)
        {
            case 0:
                perfect++;
                break;
            case 1:
                great++;
                break;
            case 2:
                bad++;
                break;
            case 3:
                miss++;
                break;
        }
    }
}
