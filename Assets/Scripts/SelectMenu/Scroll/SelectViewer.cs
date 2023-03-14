using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ScrollViewに実際に表示させるデータを渡したりする。
/// ゲーム側との折衷役
/// </summary>
public class SelectViewer : MonoBehaviour
{
    // スクロールパネル
    [SerializeField] private ScrollView scrollview;

    /// <summary>
    /// 開始処理
    /// </summary>
    private void Start()
    {
        var items = Enumerable.Range(0, 10).
            Select(i => new MusicItemData(i, $"{i:D2}曲目")).ToArray();

        scrollview.UpdateData(items);
    }
}
