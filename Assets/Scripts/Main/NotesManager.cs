using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using LitJson;


[Serializable]
public class Data
{
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public NoteData[] notes;
}

[Serializable]
public class NoteData
{
    public int type;
    public int num;
    public int block;
    public int LPB;
    public LongNoteData[] longNotes;
}

[Serializable]
public class LongNoteData
{
    public int type;
    public int num;
    public int block;
    public int LPB;
}

enum NotesType
{
    None,
    NormalNotes,
    LongNotes
}

public class NotesManager : MonoBehaviour
{
    public int noteNum;

    public List<int> LaneNum = new List<int>();
    public List<int> NoteType = new List<int>();
    public List<float> NotesTime = new List<float>();
    public List<GameObject> NotesObj = new List<GameObject>();

    [SerializeField] float NotesSpeed;
    [SerializeField] GameObject noteObj;

    void Start()
    {
        noteNum = 0;
        Load(MainManager.instance.songName);

    }

    private void Load(string SongName)
    {
        AsyncOperationHandle json;
        json = Addressables.LoadAssetAsync<TextAsset>($"Assets/Resource/Scores/{MainManager.instance.songName}.json");
        var scoreLoad = json.WaitForCompletion();

        string score = json.Result.ToString();
        Data inputJson = JsonUtility.FromJson<Data>(score);

        Debug.Log(inputJson.notes[inputJson.notes.Length - 1].longNotes);

        Addressables.Release(json);

        noteNum = inputJson.notes.Length;
        MainManager.instance.maxScore = noteNum * MainManager.instance.MAX_RAITO_POINT;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            //Debug.Log(inputJson.notes[i].longNotes);

            float z = NotesTime[i] * NotesSpeed;
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z), Quaternion.identity));
        }
    }
}