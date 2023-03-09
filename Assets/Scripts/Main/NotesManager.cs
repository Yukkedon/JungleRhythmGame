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
    public List<GameObject> LongNotesObj = new List<GameObject>();

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
            float time = (beatSec * (float)inputJson.notes[i].num / (float)inputJson.notes[i].LPB) + inputJson.offset * 0.01f;
            NotesTime.Add(time);
            LaneNum.Add(inputJson.notes[i].block);
            NoteType.Add(inputJson.notes[i].type);

            float z = NotesTime[i] * NotesSpeed;
            NotesObj.Add(Instantiate(noteObj, new Vector3(inputJson.notes[i].block - 1.5f, 0.55f, z), Quaternion.identity));
            Debug.Log(i);
            // ロングノーツ判定
            if ((int)jsonData["notes"][i]["type"] == 2)
            {
                // ロングノーツ作成処理
                for (int j = 0; j < jsonData["notes"][i]["notes"].Count; j++){
                    Debug.Log((i+j)) ;
                    // JsonDataクラスで(flaot)(int)キャストするとエラーになるため
                    // 一時的に保存する変数を作成
                    string LPB      = jsonData["notes"][i]["notes"][j]["LPB"].ToString();
                    string NUM      = jsonData["notes"][i]["notes"][j]["num"].ToString();
                    string BLOCK    = jsonData["notes"][i]["notes"][j]["block"].ToString();
                    string TYPE     = jsonData["notes"][i]["notes"][j]["type"].ToString();

                    kankaku = 60 / (inputJson.BPM * float.Parse(LPB));
                    beatSec = kankaku * float.Parse(LPB);
                    time = (beatSec * float.Parse(NUM) / float.Parse(LPB) + inputJson.offset * 0.01f);

                    NotesTime.Add(time);
                    LaneNum.Add(int.Parse(BLOCK));
                    NoteType.Add(int.Parse(TYPE));

                    z = NotesTime[i+j] * NotesSpeed;
                    
                    noteNum++;
                    
                    NotesObj.Add(Instantiate(noteObj, new Vector3(int.Parse(BLOCK) - 1.5f, 0.55f, z), Quaternion.identity));


                    Debug.Log(NoteType[NoteType.Count - 1]+":"+ NoteType[NoteType.Count - 2]);
                    Debug.Log((NoteType.Count - 1) + ":" + (NoteType.Count - 2));

                    LongNotesCreate(NotesObj[NotesObj.Count - 2].transform,NotesObj[NotesObj.Count - 1].transform);
                }
                
            }
        }
        MainManager.instance.maxScore = noteNum * MainManager.instance.MAX_RAITO_POINT;
    }

    private const int LANE_WIDTH = 1;
    private void LongNotesCreate(Transform start, Transform end)
    {


        GameObject longNotesLine = new GameObject();
        longNotesLine.AddComponent<MeshFilter>();
        longNotesLine.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        longNotesLine.GetComponent<MeshFilter>().mesh = mesh;
        Vector3[] vertices = new Vector3[4];
        int[] triangles = { 0, 2, 1, 3, 1, 2 };

        vertices[0] = start.position + new Vector3(-LANE_WIDTH / 2, 0, 0);
        vertices[1] = start.position + new Vector3( LANE_WIDTH / 2, 0, 0);
        vertices[2] = end.position   + new Vector3( LANE_WIDTH / 2, 0, 0);
        vertices[3] = end.position   + new Vector3(-LANE_WIDTH / 2, 0, 0);

        Debug.Log(start.position +":"+ end.position);

        mesh.vertices  = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}