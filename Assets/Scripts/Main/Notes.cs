using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] float NoteSpeed = 7f;

    bool isStart = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isStart)
        {
            isStart = true;
        }
        else
        {
            transform.position -= transform.forward * Time.deltaTime * NoteSpeed;
        }
        
        
    }
}