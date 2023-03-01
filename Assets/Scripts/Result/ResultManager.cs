using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public void OnRetryButton()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void OnSelectButton()
    {
        SceneManager.LoadScene("SelectScene");
    }
}
