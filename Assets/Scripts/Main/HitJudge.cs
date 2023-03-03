using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class HitJudge : MonoBehaviour
{
    [SerializeField] GameObject[] JudgeMsgObj;
    [SerializeField] NotesManager notesManager;

    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI scoreText;


    [SerializeField] float PerfectSecond= 0.10f;
    [SerializeField] float GreatSecond  = 0.15f;
    [SerializeField] float BadSecond    = 0.20f;
    [SerializeField] float MissSecond   = 0.20f;

    // Update is called once per frame
    void Update()
    {
        if (MainManager.instance.isStart)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (notesManager.LaneNum[0] == 0)
                {
                    CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NotesTime[0] + MainManager.instance.startTime)),0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 0)
                    {
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NotesTime[0] + MainManager.instance.startTime)),1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (notesManager.LaneNum[0] == 1)
                {
                    CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NotesTime[0] + MainManager.instance.startTime)),0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 1)
                    {
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NotesTime[0] + MainManager.instance.startTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (notesManager.LaneNum[0] == 2)
                {
                    CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NotesTime[0] + MainManager.instance.startTime)), 0);
                }
                else
                {
                    if (notesManager.LaneNum[1] == 2)
                    {
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NotesTime[0] + MainManager.instance.startTime)), 1);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (notesManager.LaneNum[0] == 3)
                {
                    CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NotesTime[0] + MainManager.instance.startTime)),0);
                    MainManager.instance.ResetCombo();
                }
                else
                {
                    if (notesManager.LaneNum[1] == 3)
                    {
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NotesTime[0] + MainManager.instance.startTime)), 1);
                    }
                }
            }

            // ƒ~ƒX”»’è
            if (Time.time > notesManager.NotesTime[0] + MissSecond + MainManager.instance.startTime)
            {
                PopupJudgeMsg(3);
                DeleteData(0);
                MainManager.instance.ResetCombo();
                MainManager.instance.AddJudgeCount(3);
            }
        }
        
    }

    void CheckHitTiming(float timeLag,int offset)
    {
        if (timeLag <= PerfectSecond)
        {
            PopupJudgeMsg(0);
            DeleteData(offset);
            MainManager.instance.AddCombo();
            MainManager.instance.AddJudgeCount(0);
        }
        else if (timeLag <= GreatSecond)
        {
            PopupJudgeMsg(1);
            DeleteData(offset);
            MainManager.instance.AddCombo();
            MainManager.instance.AddJudgeCount(1);
        }
        else if (timeLag <= BadSecond)
        {
            PopupJudgeMsg(2);
            DeleteData(offset);
            MainManager.instance.ResetCombo();
            MainManager.instance.AddJudgeCount(2);
        }

    }

    void DeleteData(int offset)
    {
        notesManager.NotesTime.RemoveAt(0);
        notesManager.LaneNum.RemoveAt(0);
        notesManager.NoteType.RemoveAt(0);

        MainManager.instance.point = PointConvert();
    }

    void PopupJudgeMsg(int judge)
    {
        Instantiate(JudgeMsgObj[judge], new Vector3(notesManager.LaneNum[0] - 1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }

    int PointConvert()
    {
        return (int)Math.Round(1000000 * Math.Floor(MainManager.instance.playerScore / MainManager.instance.maxScore * 1000000) / 1000000);
    }
}
