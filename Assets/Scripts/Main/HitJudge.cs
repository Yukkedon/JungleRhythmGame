using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Experimental.GraphView;
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

    [SerializeField] GameObject hitEffect;


    [SerializeField] float PerfectSecond= 0.10f;
    [SerializeField] float GreatSecond  = 0.15f;
    [SerializeField] float BadSecond    = 0.20f;
    [SerializeField] float MissSecond   = 0.20f;

    enum PusingKey
    {
        D,F,J,K,
    }

    bool[] pushingKeyState = new bool[4] { false, false, false, false };

    List<NoteData> longNoteDataList = new List<NoteData>();

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
                    if (notesManager.NoteDataAll[0].type != 2)
                    {
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[0].time + mainManager.startTime)), 0);
                    }
                    else
                    {
                        NoteData tmpNote = new NoteData(notesManager.NoteDataAll[0]);
                        longNoteDataList.Add(tmpNote);
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[0].time + mainManager.startTime)), 0);
                    }
                }
                else if (notesManager.NoteDataAll.Count != 1)
                {
                    if (notesManager.NoteDataAll[1].laneNum == 0)
                    {
                        if (notesManager.NoteDataAll[1].type != 2)
                        {
                            CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)), 1);
                        }
                        else
                        {
                            NoteData tmpNote = new NoteData(notesManager.NoteDataAll[1]);
                            longNoteDataList.Add(tmpNote);
                            CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)), 1);
                        }
                    }
                }
                soundMain.PlaySE((int)SoundMain.SE.Touch);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                
                if (notesManager.NoteDataAll[0].laneNum == 1)
                {
                    if (notesManager.NoteDataAll[0].type != 2)
                    {
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[0].time + mainManager.startTime)), 0);
                    }
                    else
                    {
                        NoteData tmpNote = new NoteData(notesManager.NoteDataAll[0]);
                        longNoteDataList.Add(tmpNote);
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[0].time + mainManager.startTime)), 0);
                    }
                }
                else if (notesManager.NoteDataAll.Count != 1)
                {
                    if (notesManager.NoteDataAll[1].laneNum == 1)
                    {
                        if (notesManager.NoteDataAll[1].type != 2)
                        {
                            CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)), 1);
                        }
                        else
                        {
                            NoteData tmpNote = new NoteData(notesManager.NoteDataAll[1]);
                            longNoteDataList.Add(tmpNote);
                            CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)), 1);
                        }
                    }
                }
                soundMain.PlaySE((int)SoundMain.SE.Touch);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                
                if (notesManager.NoteDataAll[0].laneNum == 2)
                {
                    if (notesManager.NoteDataAll[0].type != 2)
                    {
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[0].time + mainManager.startTime)), 0);
                    }
                    else
                    {
                        NoteData tmpNote = new NoteData(notesManager.NoteDataAll[0]);
                        longNoteDataList.Add(tmpNote);
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[0].time + mainManager.startTime)), 0);
                    }
                }
                else if (notesManager.NoteDataAll.Count != 1)
                {
                    if (notesManager.NoteDataAll[1].laneNum == 2)
                    {
                        if (notesManager.NoteDataAll[1].type != 2)
                        {
                            CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)), 1);
                        }
                        else
                        {
                            NoteData tmpNote = new NoteData(notesManager.NoteDataAll[1]);
                            longNoteDataList.Add(tmpNote);
                            CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)), 1);
                        }
                    }
                }
                soundMain.PlaySE((int)SoundMain.SE.Touch);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                
                if (notesManager.NoteDataAll[0].laneNum == 3)
                {
                    if (notesManager.NoteDataAll[0].type != 2)
                    {
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[0].time + mainManager.startTime)), 0);
                    }
                    else
                    {
                        NoteData tmpNote = new NoteData(notesManager.NoteDataAll[0]);
                        longNoteDataList.Add(tmpNote);
                        CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[0].time + mainManager.startTime)), 0);
                    }
                }
                else if (notesManager.NoteDataAll.Count != 1)
                {
                    if (notesManager.NoteDataAll[1].laneNum == 3)
                    {
                        if (notesManager.NoteDataAll[1].type != 2)
                        {
                            CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)), 1);
                        }
                        else
                        {
                            NoteData tmpNote = new NoteData(notesManager.NoteDataAll[1]);
                            longNoteDataList.Add(tmpNote);
                            CheckHitTiming(Mathf.Abs(Time.time - (notesManager.NoteDataAll[1].time + mainManager.startTime)), 1);
                        }
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

        if (longNoteDataList.Count != 0)
        {
            // NoteDataを個別で見る
            foreach (NoteData note in longNoteDataList)
            {
                UpdateLongNotes(note);
                
            }

            for (int i = longNoteDataList.Count - 1; i >= 0; i--)
            {
                if (longNoteDataList[i].isEnd)
                {
                    foreach(LongNoteData longnote in longNoteDataList[i].longNotes)
                    {
                        Destroy(longnote.notes);
                    }
                    longNoteDataList.RemoveAt(i);
                }
            }
            
        }

    }

    private void FixedUpdate()
    {
        UpdateText();
    }

    void CheckHitTiming(float timeLag,int offset, bool isLong = false)
    {
        if (timeLag <= PerfectSecond)
        {
            PopupJudgeMsg(0, offset);
            mainManager.AddCombo();
            mainManager.AddJudgeCount(0);
        }
        else if (timeLag <= GreatSecond)
        {
            PopupJudgeMsg(1, offset);
            mainManager.AddCombo();
            mainManager.AddJudgeCount(1);
        }
        else if (timeLag <= BadSecond)
        {
            PopupJudgeMsg(2, offset);
            mainManager.ResetCombo();
            mainManager.AddJudgeCount(2);
        }
        // ロングノーツである場合はすべて削除しない
        if (!isLong && timeLag <= BadSecond)
        {
            
            DeleteData(offset);
        }
        else if (isLong && timeLag <= BadSecond)
        {
            notesManager.NoteDataAll.RemoveAt(offset);
            Destroy(notesManager.NotesObj[offset]);
        }
    }



    void UpdateLongNotes(NoteData notedata)
    {
        for (int i = 0; i< notedata.longNotes.Count; i++)
        {
            if (i != notedata.longNotes.Count - 1)
            {
                // パーフェクト判定の範囲に入っていてまだ判定していない状態であれば処理
                if ((Mathf.Abs(Time.time - (notedata.longNotes[i].time + mainManager.startTime))) <= PerfectSecond && !notedata.longNotes[i].passnext)
                {
                    if (pushingKeyState[notedata.longNotes[i].laneNum])
                    {
                        PopupJudgeLongMsg(0, notedata.longNotes[i].laneNum);
                        notedata.longNotes[i].passnext = true;
                        soundMain.PlaySE((int)SoundMain.SE.Touch);
                    }
                }
            }
            else
            {
                if ((Mathf.Abs(Time.time - (notedata.longNotes[i].time + mainManager.startTime))) <= MissSecond && !notedata.isEnd)
                {
                    if (!pushingKeyState[notedata.longNotes[i].laneNum])
                    {
                        if ((Mathf.Abs(Time.time - (notedata.longNotes[i].time + mainManager.startTime)) <= PerfectSecond))
                        {
                            PopupJudgeLongMsg(0,notedata.longNotes[i].laneNum);
                            mainManager.AddCombo();
                            mainManager.AddJudgeCount(0);
                        }
                        else if ((Mathf.Abs(Time.time - (notedata.longNotes[i].time + mainManager.startTime)) <= GreatSecond))
                        {
                            PopupJudgeMsg(1, notedata.longNotes[i].laneNum);
                            mainManager.AddCombo();
                            mainManager.AddJudgeCount(1);
                        }
                        else if ((Mathf.Abs(Time.time - (notedata.longNotes[i].time + mainManager.startTime)) <= BadSecond))
                        {
                            PopupJudgeMsg(2, notedata.longNotes[i].laneNum);
                            mainManager.ResetCombo();
                            mainManager.AddJudgeCount(2);
                        }
                        else if ((Mathf.Abs(Time.time - (notedata.longNotes[i].time + mainManager.startTime)) <= MissSecond))
                        {
                            PopupJudgeMsg(3, notedata.longNotes[i].laneNum);
                            mainManager.ResetCombo();
                            mainManager.AddJudgeCount(3);
                        }
                        soundMain.PlaySE((int)SoundMain.SE.Touch);
                        notedata.isEnd = true;
                        Debug.Log(notedata.isEnd);
                    }
                }
            }
        }
        
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

        if (judge != 3)
        {
            Instantiate(hitEffect, new Vector3(notesManager.NoteDataAll[offset].laneNum - 1.5f, 0.6f, 0f), Quaternion.Euler(90, 0, 0));
        }
    }
    void PopupJudgeLongMsg(int judge,int laneNum)
    {
        // Instanceの削除処理はオブジェクトに記述
        Instantiate(JudgeMsgObj[judge], new Vector3(laneNum - 1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
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
