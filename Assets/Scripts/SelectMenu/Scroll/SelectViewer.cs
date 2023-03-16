using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using LitJson;

/// <summary>
/// ScrollViewに実際に表示させるデータを渡したりする。
/// ゲーム側との折衷役
/// </summary>
public class SelectViewer : BaseSound
{
    public List<string> jsonKey = new List<string>();

    // スクロールパネル
    [SerializeField] private ScrollView scrollview;

    [SerializeField] SoundSelect soundSelect;

    private void Awake()
    {
        Load();
    }
    /// <summary>
    /// jsonロード
    /// </summary>
    private void Load()
    {
        AsyncOperationHandle json;

        // jsonをaddressableで取得
        json = Addressables.LoadAssetAsync<TextAsset>($"Assets/Resource/CutMusic/NameList.json");

        // 同期処理化
        var scoreLoad = json.WaitForCompletion();

        TextAsset score = json.Result as TextAsset;
        JsonData jsonData = JsonMapper.ToObject(score.ToString());

        Addressables.Release(json);

        for (int i = 0; i < jsonData.Count; i++)
        {
            jsonKey.Add(jsonData[i].ToString());
            Debug.Log(jsonKey[i]);
        }
    }

    /// <summary>
    /// 開始処理
    /// </summary>
    private void Start()
    {
        StartCoroutine(StartScrollObj());
        LoadBGM("",true);
    }

    private void Update()
    {
        //Debug.Log("bgmClip.Count" + BgmClipCount());
    }

    IEnumerator StartScrollObj()
    {
        yield return new WaitForSeconds(2f);
        var items = Enumerable.Range(0, jsonKey.Count).
          Select(i => new MusicItemData(jsonKey.Count, jsonKey[i])).ToArray();
        scrollview.UpdateData(items);
    }

   public void PlayBgm()
    {
        soundSelect.PlayBGM(0);
    }
}
