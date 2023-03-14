using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ScrollViewに実際に表示させるデータを渡したりする。
/// ゲーム側との折衷役
/// </summary>
public class SelectViewer : BaseSound
{
    // スクロールパネル
    [SerializeField] private ScrollView scrollview;

    private void Awake()
    {
        LoadBGM("", true);
    }

    /// <summary>
    /// 開始処理
    /// </summary>
    private void Start()
    {
        StartCoroutine(StartScrollObj());
    }

    private void Update()
    {
        //Debug.Log("bgmClip.Count" + BgmClipCount());
    }

    IEnumerator StartScrollObj()
    {
        yield return new WaitForSeconds(2f);
        var items = Enumerable.Range(0, GetbgmClip().Count).
          Select(i => new MusicItemData(i, $""+ GetBgmClipName(i))).ToArray();
        scrollview.UpdateData(items);
    }
}
