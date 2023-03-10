using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{

    [SerializeField] private GameObject[] buttonPanel;
 

    [SerializeField] GameObject panel;

    private void Awake()
    {
        // ƒpƒlƒ‹‚Ì‰Šú’l‘å‚«‚³İ’è
        for (int i = 0; i < buttonPanel.Length; i++)
        {
            buttonPanel[i].transform.localScale = Vector3.zero;
        }
        panel.SetActive(false);
       
    }

    public void OnClickCloseButton()
    {
        panel.SetActive(false);

        for (int i = 0; i < buttonPanel.Length; i++)
        {
            if (buttonPanel[i])
            {
                buttonPanel[i].transform.DOScale(Vector3.zero, 0.2f);


                if (buttonPanel[i].transform.localScale == Vector3.zero)
                {
                    buttonPanel[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void OnClickRetryMusic()
    {
        panel.SetActive(true);

        buttonPanel[0].gameObject.SetActive(true);
        buttonPanel[0].transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }

    public void OnClickSelectMusic()
    {
        panel.SetActive(true);

        buttonPanel[1].gameObject.SetActive(true);
        buttonPanel[1].transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }

    public void OnClickYesButton()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void OnClickSerectYesButton()
    {
        SceneManager.LoadScene("SelectScene");
    }

}
