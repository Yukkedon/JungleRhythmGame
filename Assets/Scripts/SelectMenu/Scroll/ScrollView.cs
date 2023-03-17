using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FancyScrollView;

// スクロールビュウで利用するデータ型も宣言
class MusicItemData
{
    // id
    public int Id;
    // 曲名
    public string musicName;

    public Sprite image;

    // コンストラクタ
    public MusicItemData(int id ,string musicName,Sprite image)
    {
        this.Id = id;
        this.musicName = musicName;
        this.image = image;
    }
}

class ScrollView : FancyScrollView<MusicItemData>
{
    [SerializeField] private Scroller _scroller;
    [SerializeField] private GameObject _cellPrefab;

    protected override GameObject CellPrefab => _cellPrefab;

    protected override void Initialize()
    {
        _scroller.OnValueChanged(UpdatePosition);
    }

    public void UpdateData(IList<MusicItemData> music)
    {
        UpdateContents(music)
;       _scroller.SetTotalCount(music.Count);
    }
}
