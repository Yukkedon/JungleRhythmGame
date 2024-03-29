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
    [SerializeField] Button button;
    public int id;
    //public Button _image;

    static class AnimatorHash
    {
        public static readonly int Scroll = Animator.StringToHash("scroll");
    }
    float currentPosition = 0;

    public TextMeshProUGUI _txtName;
    public override void UpdateContent(MusicItemData itemData)
    {
        cellMusic.PlaySe();
        id = itemData.Id;
        // ここで曲を切り替える
        // cellMusic.PlayBgm();
        _txtName.text = itemData.musicName;
        button.GetComponent<Image>().sprite = itemData.image;
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
