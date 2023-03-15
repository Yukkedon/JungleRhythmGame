using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FancyScrollView;

class Cell : FancyCell<MusicItemData>
{
    [SerializeField] Animator animator = default;
    [SerializeField] CellMusic cellMusic;
    static class AnimatorHash
    {
        public static readonly int Scroll = Animator.StringToHash("scroll");
    }
    float currentPosition = 0;

    public TextMeshProUGUI _txtName;
    public override void UpdateContent(MusicItemData itemData)
    {
        cellMusic.PlaySe();
        _txtName.text = itemData.musicName;
    }

    public override void UpdatePosition(float position)
    {
        currentPosition = position;
        if (animator.isActiveAndEnabled)
        {
            animator.Play(AnimatorHash.Scroll, -1, position);
        }
        animator.speed = 0;
    }
    void OnEnable()
    {
        UpdatePosition(currentPosition);
    }
}
