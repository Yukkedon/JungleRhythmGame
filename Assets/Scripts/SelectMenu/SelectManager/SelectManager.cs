using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.EventSystems;//追加

public class SelectManager : MonoBehaviour
{
    // �J�ڃ{�^��
    [SerializeField] private GameObject buttonPanel;

    // �{�^���i�[�ϐ�
    [SerializeField] Button yesbutton;
    [SerializeField] Button nobutton;
    [SerializeField] Button closebutton;

    [SerializeField] GameObject panel;
    //[SerializeField] GameObject fade;
    [SerializeField] SoundSelect soundSelect;
    private GameObject button;
    public List<string> names = new List<string>();
    [SerializeField] private EventSystem eventSystem;
    int nowid=-1;
    int id;
    /// <summary>
    /// �J�n����
    /// 1�񂵂��Ă΂Ȃ�����Awake
    /// </summary>
    private void Awake()
    {
        // �p�l���̏����X�P�[���̐ݒ�
        buttonPanel.transform.localScale = Vector3.zero;

        // �e�{�^���ɃA�N�V�������i�[
        nobutton.onClick.AddListener(OnClickCloseButton);
        closebutton.onClick.AddListener(OnClickCloseButton);
        yesbutton.onClick.AddListener(OnClickYesButton);
        // �p�l�����\��
        panel.SetActive(false);
        //fade = GameObject.Find("FadeCanvas");

    }

    /// <summary>
    /// �������A��������X�{�^�������������̏���
    /// </summary>
    private void OnClickCloseButton()
    {
        nowid = button.GetComponent<Cell>().id;
        // タイトルBGMの再生
        soundSelect.PlaySE((int)SoundSelect.SE.Cansel);

        panel.SetActive(false);
        // �I�v�V�����E�B���h�E�����񂾂񏬂���
        buttonPanel.transform.DOScale(Vector3.zero, 0.2f);

        // �X�P�[����0�ɂȂ������\���ɂ���
        if(buttonPanel.transform.localScale== Vector3.zero)
        {
            buttonPanel.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// �Ȃ�I�������Ƃ�
    /// TODO:�ʏ����ɂ���\��
    /// </summary>
    public void OnClickSelectMusic()
    {
        button = eventSystem.currentSelectedGameObject;
        id = button.GetComponent<Cell>().id;
        // ここでゲームマネージャに値を渡す
        GameManager.Instance.GetComponent<GameManager>().songName = names[id];
        if (id != nowid)
        {
            soundSelect.PlayBGM(id);
        }
        // タイトルBGMの再生
        soundSelect.PlaySE((int)SoundSelect.SE.Select);

        panel.SetActive(true);

        // �{�^���p�l����\��
        buttonPanel.gameObject.SetActive(true);

        // �I�v�V�����E�B���h�E�����񂾂�g��
        buttonPanel.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }

    /// <summary>
    /// �u�͂��v�̃{�^�����������Ƃ�
    /// </summary>
    private void OnClickYesButton()
    {
        // タイトルBGMの再生
        soundSelect.PlaySE((int)SoundSelect.SE.Select);
        // �t�F�[�h����
        // �����ɏ���
        //fade.GetComponent<Fade>().FadeIn(1f, () =>SceneManager.LoadScene("MainScene"));
        SceneManager.LoadScene("MainScene");
    }

}
