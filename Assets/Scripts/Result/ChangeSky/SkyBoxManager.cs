using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkyBoxManager : MonoBehaviour
{
    // カメラの回転スピード
    [Tooltip("カメラの回転スピード")]
    [Range(0.01f, 1f)]
    [SerializeField] float rotateSpeed = 0.01f;

    // スカイボックスマテリアル
    [SerializeField] Material sky;

    // skyboxのブレンド値
    float alphaValue = 0f;

    // カメラ
    private Camera cam;

    // カメラの回転
    private Vector3 newAngle = new Vector3(0f, 0f, 0f);


    private void Start()
    {
        // メインカメラの取得
        cam = Camera.main;
        
        // ブレンドの割合の初期値設定
        alphaValue = 0f;
        sky.SetFloat("_value", alphaValue);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        // フェードを考え1秒skyboxの更新処理を待つ
        Invoke("ChangeSkyBox", 1);
        
        // カメラの回転処理
        CamRotate();
    }

    /// <summary>
    /// カメラの回転処理
    /// </summary>
    void CamRotate()
    {
        // マウスの移動量分カメラを回転させる.
        newAngle.y += rotateSpeed;

        cam.gameObject.transform.localEulerAngles = newAngle;
    }

    /// <summary>
    /// skyboxの変更処理
    /// </summary>
    private void ChangeSkyBox()
    {
        if (SceneManager.GetActiveScene().name == "ResultScene")
        {
            sky.SetFloat("_value", alphaValue);

            if (alphaValue <= 1)
            {
                alphaValue += 0.005f;
            }
        }
    }
}
