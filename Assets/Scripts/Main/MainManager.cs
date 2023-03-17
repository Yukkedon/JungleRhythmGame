using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainManager : MonoBehaviour
{
    [SerializeField] Fade fade;
    [SerializeField] TextMeshProUGUI countText;

    [SerializeField] SoundMain soundMain;
    [SerializeField] GameObject comboText;
    public string songName;

    public int MAX_RAITO_POINT = 5;
    public int MAX_DIGIT_POINT = 1000000;
    public int PERFECT_POINT = 5;

    public bool isStart = false;
    public bool isEnd = false;
    public float startTime = 0;   // �X�^�[�g�{�^���������܂ł̕b����ۑ�
    public float playerScore = 0;
    public float maxScore = 0;
    public int point = 0;

    int combo = 0;

    int perfect = 0;
    int great = 0;
    int bad = 0;
    int miss = 0;


    public bool isAnimStart = false;
    IEnumerator animCorou = null;

    public void SetStartTime(float startTime)
    {
        this.startTime = startTime;
    }

    public void Start()
    {
        animCorou = PushSpaceAnim();
        songName = GameManager.Instance.songName;
        countText.DOFade(0.0f, 1.0f).SetLoops(-1, LoopType.Yoyo).Play();
    }

    public void Update()
    {
        

        if (!isAnimStart && !isStart && Input.GetKeyDown(KeyCode.Space))
        {
            isAnimStart = true;

            //StartCoroutine(PushSpaceAnim());
/*            countText.DOPause();
            countText.transform.DOLocalRotate(new Vector3(0, 0, 720f), 1f, RotateMode.FastBeyond360);
            
            isStart = true;*/
        }

        if (isAnimStart)
        {
            StartCoroutine(PushSpaceAnim());
        }


        if (soundMain == null && isStart && isEnd) return;  // AudioSource����������Ԃł��Q�Ƃ��Ă��܂�����Null�����ǉ�
        if (soundMain.IsCheckEndBGM() && isStart && isEnd)
        {
            SetGameManagerScore();
            fade.FadeIn(1f, () => SceneManager.LoadScene("ResultScene"));
            
        }
    }

    public void SetGameManagerScore()
    {
        GameManager.Instance.point = point;
        GameManager.Instance.combo = combo;
        GameManager.Instance.perfect = perfect;
        GameManager.Instance.great = great;
        GameManager.Instance.bad = bad;
        GameManager.Instance.miss = miss;
    }

    public void ResetCombo()
    {
        combo = 0;
    }
    public void AddCombo()
    {
        ComboAnim();
        combo++;
    }
    public int GetCombo()
    {
        return combo;
    }

    public int GetPoint()
    {
        return point;
    }


    // 0:perfect 1:great 2:bad 3:miss
    public void AddJudgeCount(int num)
    {
        switch (num)
        {
            case 0:
                perfect++;
                point += 5;
                break;
            case 1:
                great++;
                point += 3;
                break;
            case 2:
                bad++;
                point += 1;
                break;
            case 3:
                miss++;
                break;
        }
    }

    private void ComboAnim()
    {
        comboText.transform.DOPunchScale(new Vector3(1.05f, 1.05f, 1.05f), 0.1f).OnComplete(() => BaseScale());
    }

    private void BaseScale()
    {
        comboText.transform.localScale = Vector3.one;
    }

    IEnumerator PushSpaceAnim()
    {
        isAnimStart = false;
        countText.transform.DOScale(new Vector3(10,10,10) ,1);
        countText.DOPause();
        countText.transform.DOLocalRotate(new Vector3(0, 0, 720f), 1f, RotateMode.FastBeyond360).WaitForCompletion();
        countText.DOFade(0.0f, 1.0f).Play();
        
        yield return new  WaitForSeconds(1f);
        GameManager.Instance.isStart = true;
        isStart = true;
        yield break;
    }

}
