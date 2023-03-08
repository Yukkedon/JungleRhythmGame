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
    public List<NoteData> notes;
}

[Serializable]
public class NoteData
{
    public int type;
    public int num;
    public int block;
    public int LPB;
    public List<LongNoteData> longNotes;
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

        TextAsset score = json.Result as TextAsset;
        Data inputJson = JsonMapper.ToObject<Data>(score.text);  // LitJson

        JsonData jsonData = JsonMapper.ToObject(score.ToString());

        Addressables.Release(json);

        noteNum = inputJson.notes.Count;

        for (int i = 0; i < inputJson.notes.Count; i++)
        {
            float kankaku = 60 / (inputJson.BPM * (float)inputJson.notes[i].LPB);
            float beatSec = kankaku * (float)inputJson.notes[i].LPB;
            float time = (beatSec * inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            float z = NotesTime[i] * NotesSpeed;
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z), Quaternion.identity));

            // ロングノーツ作成
            if (jsonData["notes"][i]["type"].Equals("2"))
            {
                for(int j = 0; j < jsonData["notes"][i]["notes"].Count; j++){
                    // JsonDataクラスで(flaot)(int)キャストするとエラーになるため
                    // 一時的に保存する変数を作成
                    var LPB      = jsonData["notes"][i]["notes"][j]["LPB"];
                    var NUM      = jsonData["notes"][i]["notes"][j]["num"];
                    var BLOCK    = jsonData["notes"][i]["notes"][j]["block"];
                    var TYPE     = jsonData["notes"][i]["notes"][j]["type"];
                    Debug.Log(NUM);
                    Debug.Log(BLOCK);
                    Debug.Log(TYPE);

                    kankaku = (int)LPB;
                    kankaku = 60 / (inputJson.BPM * kankaku);
                    
                    beatSec = (int)LPB;
                    beatSec = kankaku * beatSec;

                    time = (int)NUM / (int)LPB;
                    time = (beatSec * time + inputJson.offset * 0.01f);
                    NotesTime.Add(time);
                    LaneNum.Add((int)BLOCK);
                    NoteType.Add((int)TYPE);

                    z = NotesTime[i+j] * NotesSpeed;

                    noteNum++;
                    
                    NotesObj.Add(Instantiate(noteObj, new Vector3((int)BLOCK - 1.5f, 0.55f, z), Quaternion.identity));
                }
                
            }
        }

        MainManager.instance.maxScore = noteNum * MainManager.instance.MAX_RAITO_POINT;
    }
}