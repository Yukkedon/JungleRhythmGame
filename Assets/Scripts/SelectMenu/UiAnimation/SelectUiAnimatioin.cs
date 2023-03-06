using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SelectUiAnimatioin : MonoBehaviour
{
    // アニメーションさせるテキスト
    [SerializeField] TextMeshProUGUI gorillaDialogue = default;

    /// <summary>
    /// 開始処理
    /// </summary>
    void Start()
    {
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
}
