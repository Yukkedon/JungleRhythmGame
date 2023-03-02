using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// �~�j�L�����̔��]�ƃA�j���[�V��������
/// TODO : Addressable Assets System�ɕύX����\��
/// </summary>
public class MiniCharaMove : MonoBehaviour
{
    // �\������X�v���C�g�i�[�p
    [SerializeField] private Sprite[] spriteImage;

    // Sprite����p
    private bool isFront = true;

    // ���Ԃ��X�s�[�h
    [Tooltip("���Ԃ��X�s�[�h")]
    [SerializeField] private float speed = 1.0f;

    // ����Image��RectTransform
    [SerializeField] RectTransform rectTransform;

    // ���̃I�u�W�F�N�g��Image
    [SerializeField] private Image miniChara;

    // �J�n���̃X�P�[��
    // ���]�������J�n����Ƃ��̑傫��
    private Vector3 startScale = new Vector3(1.0f, 1.0f, 1.0f);

    // ���Ԓn�_�̃X�P�[�� (x = 0)
    // x��0�Ȃ̂Ō����Ȃ��i���]���̑傫���j
    private Vector3 endScale = new Vector3(0f, 1.0f, 1.0f);

    // RectTransform�i�[�p�̑傫��
    private Vector3 localScale = new Vector3();

    // �⊮�p�ϐ�(0f�`1f�̊�)
    private float tick;

    /// <summary>
    /// �J�n����
    /// </summary>
    private void Start()
    {
        miniChara.sprite = spriteImage[0];
        StartCoroutine(Turn());
    }

    /// <summary>
    /// Sprite�𔽓]������֐�
    /// �X�P�[����x��b���ŕς��Ă���
    /// </summary>
    /// <returns></returns>
    IEnumerator Turn()
    {
        // �g�D�[���A�j���[�V�����J�n
        DoTweenAnimMiniChara(-30f, 2f);

        // �������[�v
        while (true)
        {
            yield return new WaitForSeconds(2f);
            tick = 0f;
            // (1/speed)�b�Œ��Ԓn�_�܂łЂ�����Ԃ�
            while (tick < 1.0f)
            {
                tick += Time.deltaTime * speed;
                // ���`���
                localScale = Vector3.Lerp(startScale, endScale, tick);

                rectTransform.localScale = localScale;

                yield return null;
            }

            // ���\��ς���
            isFront = !isFront;

            // sprite��ς���
            ChangeImage();

            // �⊮�l�̏�����
            tick = 0f;

            // (1/speed)�b�Œ��Ԃ���Ō�܂łЂ�����Ԃ�
            while (tick < 1.0f)
            {
                tick += Time.deltaTime * speed;

                localScale = Vector3.Lerp(endScale, startScale, tick);

                rectTransform.localScale = localScale;

                yield return null;
            }
        }
    }

    /// <summary>
    /// sprite��؂�ւ���֐�
    /// </summary>
    private void ChangeImage()
    {
        if (isFront == true)
        {
            miniChara.sprite = spriteImage[0];
        }
        else
        {
            miniChara.sprite = spriteImage[1];
        }
    }

    /// <summary>
    /// doTween�A�j���[�V����
    /// DOTO:������ύX����\������
    /// </summary>
    private void DoTweenAnimMiniChara(float minY, float maxY)
    {
        // y���݂̂̃A�j���[�V����
        rectTransform.DOLocalMoveY(minY, maxY).
            SetEase(Ease.OutBounce).SetLoops(-1, LoopType.Yoyo);
    }
}
