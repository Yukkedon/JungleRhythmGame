using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    enum PusingKey
    {
        D,F,J,K,
    }

    bool[] pushingKeyState = new bool[4] { false, false, false, false };

    // Update is called once per frame
    void Update()
    {
        // ロングノーツがあるかどうか
        bool isLong0 = notesManager.NoteDataAll[0].type == 2;
        bool isLong1 = notesManager.NoteDataAll[1].type == 2;

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
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)), 1, isLong1);
                    }
                }
                soundMain.PlaySE((int)SoundMain.SE.Touch);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                if (notesManager.NoteDataAll[0].laneNum == 3)
                {
                    CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[0].time + mainManager.startTime)),0, isLong0);
                    mainManager.ResetCombo();
                }
                else
                {
                    if (notesManager.NoteDataAll[1].laneNum == 3)
                    {
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)), 1, isLong1);
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
        UpdatePushingKeyState();

        if (isLong0)
        {
            UpdateLongNotes(notesManager.NoteDataAll[0]);
        }
        else if (isLong1)
        {
            UpdateLongNotes(notesManager.NoteDataAll[1]);
        }

    }

    private void FixedUpdate()
    {
        UpdateText();
    }

    void CheckHitTiming(float timeLag,int offset,bool isLong = false)
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

    void UpdateLongNotes(NoteData notesData)
    {

    }

    
    void UpdatePushingKeyState()
    {
        pushingKeyState[(int)PusingKey.D] = Input.GetKey(KeyCode.D);
        pushingKeyState[(int)PusingKey.F] = Input.GetKey(KeyCode.F);
        pushingKeyState[(int)PusingKey.J] = Input.GetKey(KeyCode.J);
        pushingKeyState[(int)PusingKey.K] = Input.GetKey(KeyCode.K);
    }

    void DeleteData(int offset)
    {
        if (notesManager.NoteDataAll[offset].longNotes != null)
        {
            foreach(LongNoteData notes in notesManager.NoteDataAll[offset].longNotes)
            {
                Debug.Log(notes.ToString());
                Destroy(notes.notes);
            }
        }

        notesManager.NoteDataAll.RemoveAt(offset);
        Destroy(notesManager.NotesObj[offset]);
        notesManager.NotesObj.RemoveAt(offset);

        if (notesManager.NoteDataAll.Count <= 0)
        {
            mainManager.isEnd = true;
        }
    }

    // ロングノーツの情報を持っているオブジェクト・リストの削除
    void DeleteDataLongNotes(int offset)
    {
        
        Destroy(notesManager.NotesObj[offset]);
        foreach(LongNoteData longnotes in notesManager.NoteDataAll[offset].longNotes)
        {
            Destroy(notesManager.NotesObj[offset]);
        }

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

    bool PushingKey()
    {
        if (Input.anyKey)
        {
            return true;
        }
        return false;
    }
    void AddPoint()
    {
        mainManager.point = (int)Math.Round(1000000 * Math.Floor(mainManager.playerScore / mainManager.maxScore * 1000000) / 1000000);
    }
}
