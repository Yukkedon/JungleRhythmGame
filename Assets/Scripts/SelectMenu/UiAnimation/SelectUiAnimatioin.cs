using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SelectUiAnimatioin : MonoBehaviour
{
    [SerializeField] Fade fade;
    // �A�j���[�V����������e�L�X�g
    [SerializeField] TextMeshProUGUI gorillaDialogue = default;

    // �w�i�I�u�W�F�N�g
    [SerializeField] private GameObject backGround1;
    [SerializeField] private GameObject backGround2;
    
    // �e�L�X�g�p�l��
    [SerializeField] private GameObject textPanel;
    
    // �~�j�L����
    [SerializeField] private GameObject[] miniChara;
    
    // �����͕ύX�\��
    [SerializeField] private GameObject SelectButtons;

    //[SerializeField] private GameObject fade = default;
    
    /// <summary>
    /// Awake�����i�����ł͏����l�����Ă���j
    /// </summary>
    private void Awake()
    {
        fade.cutoutRange = 1f;
        
        // �e�I�u�W�F�N�g�̏����l�ݒ�
        backGround1.transform.localScale = new Vector3(2f, 2f, 2f);
        backGround2.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
        SelectButtons.transform.localScale = Vector3.zero;
        textPanel.transform.localPosition = new Vector3(0f, 300f, 0f);
        for (int i = 0; i < miniChara.Length; i++)
        {
            miniChara[i].transform.localScale = Vector3.zero;
        }
        SelectButtons.SetActive(false);

    }

    /// <summary>
    /// �J�n����
    /// </summary>
    void Start()
    {
        
        //fade.GetComponent<Fade>().FadeOut(1f, () => SelectAnimStart());
        SelectAnimStart();
    }

    void SelectAnimStart()
    {
        StartCoroutine(AnimStart());
        TextDOTweenAnim();
    }

    /// <summary>
    /// UI�A�j���[�V��������
    /// </summary>
    private void TextDOTweenAnim()
    {
        //DOTweenTMPAnimator���쐬
        DOTweenTMPAnimator animator = new DOTweenTMPAnimator(gorillaDialogue);

        //1�������A�j���[�V������ݒ�(i�����Ԗڂ̕������̃C���f�b�N�X)
        //Sequence�őS�����̃A�j���[�V�������܂Ƃ߂�
        var sequence = DOTween.Sequence();

        sequence.SetLoops(-1);//�������[�v�ݒ�


        //�ꕶ�����ɃA�j���[�V�����ݒ�
        var duration = 0.2f;//1��ӂ��Tween����
        for (int i = 0; i < animator.textInfo.characterCount; ++i)
        {
            sequence.Join(DOTween.Sequence()
              //��Ɉړ����Ė߂�
              .Append(animator.DOOffsetChar(i, animator.GetCharOffset(i) + new Vector3(0, 10, 0), duration).SetEase(Ease.OutFlash, 2))
              //������1.2�{�Ɋg�債�Ė߂�
              .Join(animator.DOScaleChar(i, 1.2f, duration).SetEase(Ease.OutFlash, 2))
              //�����ɐF�����F�ɂ��Ė߂�
              .Join(animator.DOColorChar(i, Color.gray, duration * 0.5f).SetLoops(2, LoopType.Yoyo))
              //�A�j���[�V������A1�b�̃C���^�[�o���ݒ�
              .AppendInterval(2f)
              //�J�n��0.15�b�����炷
              .SetDelay(0.3f * i)
            );
        }
    }

    /// <summary>
    /// UI�̃A�j���[�V�����ݒ�(Dotween�ƃR���[�`��)
    /// </summary>
    /// <returns></returns>
    IEnumerator AnimStart()
    {
        fade.FadeOut(1f);
        yield return new WaitForSeconds(1f);
        // �w�i�i���񂾂�k���j
        backGround1.transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1f);
        backGround2.transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(1f);
        
        // �e�L�X�g�p�l���i�ォ�痎���Ă���j
        textPanel.transform.DOLocalMoveY(-151f, 2.5f).SetEase(Ease.OutBounce);
        
        // �~�j�L�����i���񂾂�g��j
        for (int i = 0; i < miniChara.Length; i++)
        {
            miniChara[i].transform.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutBounce);
        }
        yield return new WaitForSeconds(1f);
        SelectButtons.SetActive(true);
        SelectButtons.transform.DOScale(new Vector3(1, 1, 1), 1f);

    }
}
