using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SelectManager : MonoBehaviour
{
    // 遷移ボタン
    [SerializeField] private GameObject buttonPanel;

    // ボタン格納変数
    [SerializeField] Button yesbutton;
    [SerializeField] Button nobutton;
    [SerializeField] Button closebutton;

    [SerializeField] GameObject panel;

    /// <summary>
    /// 開始処理
    /// 1回しか呼ばないためAwake
    /// </summary>
    private void Awake()
    {
        // パネルの初期スケールの設定
        buttonPanel.transform.localScale = Vector3.zero;

        // 各ボタンにアクションを格納
        nobutton.onClick.AddListener(OnClickCloseButton);
        closebutton.onClick.AddListener(OnClickCloseButton);
        yesbutton.onClick.AddListener(OnClickYesButton);
        // パネルを非表示
        panel.SetActive(false);
    }

    /// <summary>
    /// いいえ、もしくはXボタンを押した時の処理
    /// </summary>
    private void OnClickCloseButton()
    {
        panel.SetActive(false);
        // オプションウィンドウをだんだん小さく
        buttonPanel.transform.DOScale(Vector3.zero, 0.2f);

        // スケールが0になったら非表示にする
        if(buttonPanel.transform.localScale== Vector3.zero)
        {
            buttonPanel.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 曲を選択したとき
    /// TODO:別処理にする可能性
    /// </summary>
    public void OnClickSelectMusic()
    {
        panel.SetActive(true);

        // ボタンパネルを表示
        buttonPanel.gameObject.SetActive(true);

        // オプションウィンドウをだんだん拡大
        buttonPanel.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }

    /// <summary>
    /// 「はい」のボタンを押したとき
    /// </summary>
    private void OnClickYesButton()
    {
        // フェード処理
        // ここに書く

        SceneManager.LoadScene("MainScene");

    }

}
