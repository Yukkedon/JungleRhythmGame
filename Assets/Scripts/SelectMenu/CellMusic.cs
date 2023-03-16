using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using LitJson;

public class CellMusic : SelectViewer
{
    [SerializeField] TextMeshProUGUI buttonText;

    GameManager gameManager;
    private void Start()
    {
        // gameManager.GetComponent<GameManager>();

       
    }

    private void Load()
    {
      
    }

    public enum SE
    {
        SElect,
    }

 

    public void PlaySe()
    {
        PlaySE((int)SE.SElect);
    }
 
  

}
