using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SelectUiAnimatioin : MonoBehaviour
{
    // アニメーションさせるテキスト
    [SerializeField] TextMeshProUGUI gorillaDialogue = default;

    // 背景オブジェクト
    [SerializeField] private GameObject backGround1;
    [SerializeField] private GameObject backGround2;
    
    // テキストパネル
    [SerializeField] private GameObject textPanel;
    
    // ミニキャラ
    [SerializeField] private GameObject[] miniChara;
    
    // ここは変更予定
    [SerializeField] private GameObject SelectButton;
    
    /// <summary>
    /// Awake処理（ここでは初期値を入れている）
    /// </summary>
    private void Awake()
    {
        // 各オブジェクトの初期値設定
        backGround1.transform.localScale = new Vector3(2f, 2f, 2f);
        backGround2.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        textPanel.transform.localPosition = new Vector3(0f, 300f, 0f);
        for (int i = 0; i < miniChara.Length; i++)
        {
            miniChara[i].transform.localScale = Vector3.zero;
        }
        // セレクトボタンは非表示
        SelectButton.SetActive(false);
    }

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
        StartCoroutine(AnimStart());
        TextDOTweenAnim();
    }

    /// <summary>
    /// UIアニメーション処理
    /// </summary>
    private void TextDOTweenAnim()
    {
        //DOTweenTMPAnimatorを作成
        DOTweenTMPAnimator animator = new DOTweenTMPAnimator(gorillaDialogue);

        //1文字ずつアニメーションを設定(iが何番目の文字かのインデックス)
        //Sequenceで全文字のアニメーションをまとめる
        var sequence = DOTween.Sequence();

        sequence.SetLoops(-1);//無限ループ設定


        //一文字ずつにアニメーション設定
        var duration = 0.2f;//1回辺りのTween時間
        for (int i = 0; i < animator.textInfo.characterCount; ++i)
        {
            sequence.Join(DOTween.Sequence()
              //上に移動して戻る
              .Append(animator.DOOffsetChar(i, animator.GetCharOffset(i) + new Vector3(0, 10, 0), duration).SetEase(Ease.OutFlash, 2))
              //同時に1.2倍に拡大して戻る
              .Join(animator.DOScaleChar(i, 1.2f, duration).SetEase(Ease.OutFlash, 2))
              //同時に色を黄色にして戻す
              .Join(animator.DOColorChar(i, Color.gray, duration * 0.5f).SetLoops(2, LoopType.Yoyo))
              //アニメーション後、1秒のインターバル設定
              .AppendInterval(2f)
              //開始は0.15秒ずつずらす
              .SetDelay(0.3f * i)
            );
        }
    }

    /// <summary>
    /// UIのアニメーション設定(Dotweenとコルーチン)
    /// </summary>
    /// <returns></returns>
    IEnumerator AnimStart()
    {
        // 背景（だんだん縮小）
        backGround1.transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1f);
        backGround2.transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1f);
        
        // テキストパネル（上から落ちてくる）
        textPanel.transform.DOLocalMoveY(-151f, 2.5f).SetEase(Ease.OutBounce);
        
        // ミニキャラ（だんだん拡大）
        for (int i = 0; i < miniChara.Length; i++)
        {
            miniChara[i].transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutBounce);
        }
        yield return new WaitForSeconds(2f);
        SelectButton.SetActive(true);

    }
}
