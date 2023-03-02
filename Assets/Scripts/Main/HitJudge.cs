using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class HitJudge : MonoBehaviour
{
    [SerializeField] GameObject[] JudgeMsgObj;
    [SerializeField] NotesManager notesManager;

    [SerializeField] float PerfectSecond= 0.10f;
    [SerializeField] float GreatSecond  = 0.15f;
    [SerializeField] float BadSecond    = 0.20f;
    [SerializeField] float MissSecond   = 0.20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (notesManager.LaneNum[0] == 0)
            {
                CheckHitTiming(Mathf.Abs(Time.time - notesManager.NotesTime[0]));
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (notesManager.LaneNum[0] == 1)
            {
                CheckHitTiming(Mathf.Abs(Time.time - notesManager.NotesTime[0]));
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (notesManager.LaneNum[0] == 2)
            {
                CheckHitTiming(Mathf.Abs(Time.time - notesManager.NotesTime[0]));
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (notesManager.LaneNum[0] == 3)
            {
                CheckHitTiming(Mathf.Abs(Time.time - notesManager.NotesTime[0]));
            }
        }

        // ƒ~ƒX”»’è
        if (Time.time  > notesManager.NotesTime[0] + MissSecond)
        {
            PopupJudgeMsg(3);
            DeleteData();
        }
        
    }

    void CheckHitTiming(float timeLag)
    {
        if (timeLag <= PerfectSecond)
        {
            PopupJudgeMsg(0);
            DeleteData();
        }
        else if (timeLag <= GreatSecond)
        {
            PopupJudgeMsg(1);
            DeleteData();
        }
        else if (timeLag <= BadSecond)
        {
            PopupJudgeMsg(2);
            DeleteData();
        }

    }

    void DeleteData()
    {
        notesManager.NotesTime.RemoveAt(0);
        notesManager.LaneNum.RemoveAt(0);
        notesManager.NoteType.RemoveAt(0);
    }

    void PopupJudgeMsg(int judge)
    {
        Instantiate(JudgeMsgObj[judge], new Vector3(notesManager.LaneNum[0] - 1.5f, 0.76f, 0.15f), Quaternion.Euler(45, 0, 0));
    }
}
