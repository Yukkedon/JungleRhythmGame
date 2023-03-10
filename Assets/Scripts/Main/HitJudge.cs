using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;

public class HitJudge : MonoBehaviour
{

    [SerializeField] MainManager mainManager;
    [SerializeField] GameObject[] JudgeMsgObj;
    [SerializeField] NotesManager notesManager;
    [SerializeField] SoundMain    soundMain;

    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI scoreText;


    [SerializeField] float PerfectSecond= 0.10f;
    [SerializeField] float GreatSecond  = 0.15f;
    [SerializeField] float BadSecond    = 0.20f;
    [SerializeField] float MissSecond   = 0.20f;

    // Update is called once per frame
    void Update()
    {
        if (mainManager.isStart && !mainManager.isEnd)
        {
            // 各レーンに対応したボタン入力の処理
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (notesManager.NoteDataAll[0].laneNum == 0)
                {
                    CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[0].time + mainManager.startTime)),0);
                }
                else
                {
                    if (notesManager.NoteDataAll[1].laneNum == 0)
                    {
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)),1);
                    }
                }
                soundMain.PlaySE((int)SoundMain.SE.Touch);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (notesManager.NoteDataAll[0].laneNum == 1)
                {
                    CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[0].time + mainManager.startTime)),0);
                }
                else
                {
                    if (notesManager.NoteDataAll[1].laneNum == 1)
                    {
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)), 1);
                    }
                }
                soundMain.PlaySE((int)SoundMain.SE.Touch);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                if (notesManager.NoteDataAll[0].laneNum == 2)
                {
                    CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[0].time + mainManager.startTime)), 0);
                }
                else
                {
                    if (notesManager.NoteDataAll[1].laneNum == 2)
                    {
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)), 1);
                    }
                }
                soundMain.PlaySE((int)SoundMain.SE.Touch);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (notesManager.NoteDataAll[0].laneNum == 3)
                {
                    CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[0].time + mainManager.startTime)),0);
                    mainManager.ResetCombo();
                }
                else
                {
                    if (notesManager.NoteDataAll[1].laneNum == 3)
                    {
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)), 1);
                    }
                }
                soundMain.PlaySE((int)SoundMain.SE.Touch);
            }

            // ミス判定
            if (Time.time > notesManager.NoteDataAll[0].time + MissSecond + mainManager.startTime)
            {
                PopupJudgeMsg(3);
                DeleteData(0);
                mainManager.ResetCombo();
                mainManager.AddJudgeCount(3);
            }
        }

    }

    private void FixedUpdate()
    {
        UpdateText();
    }

    void CheckHitTiming(float timeLag,int offset)
    {
        if (timeLag <= PerfectSecond)
        {
            PopupJudgeMsg(0,offset);
            DeleteData(offset);
            mainManager.AddCombo();
            mainManager.AddJudgeCount(0);
        }
        else if (timeLag <= GreatSecond)
        {
            PopupJudgeMsg(1,offset);
            DeleteData(offset);
            mainManager.AddCombo();
            mainManager.AddJudgeCount(1);
        }
        else if (timeLag <= BadSecond)
        {
            PopupJudgeMsg(2, offset);
            DeleteData(offset);
            mainManager.ResetCombo();
            mainManager.AddJudgeCount(2);
        }
    }

    void DeleteData(int offset)
    {
        notesManager.NoteDataAll.RemoveAt(offset);
        Destroy(notesManager.NotesObj[offset]);
        notesManager.NotesObj.RemoveAt(offset);

        if (notesManager.NoteDataAll.Count <= 0)
        {
            mainManager.isEnd = true;
        }
    }

    void UpdateText()
    {
        comboText.text = "Combo\n"+mainManager.GetCombo().ToString();
        scoreText.text = "Score:" + mainManager.GetPoint().ToString();
    }
    void PopupJudgeMsg(int judge,int offset = 0)
    {
        // Instanceの削除処理はオブジェクトに記述
        Instantiate(JudgeMsgObj[judge], new Vector3(notesManager.NoteDataAll[offset].laneNum - 1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }

    void AddPoint()
    {
        mainManager.point = (int)Math.Round(1000000 * Math.Floor(mainManager.playerScore / mainManager.maxScore * 1000000) / 1000000);
    }
}
