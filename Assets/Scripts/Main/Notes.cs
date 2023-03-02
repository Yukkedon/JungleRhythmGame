using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{
    [SerializeField] float NoteSpeed = 7f;

    // Update is called once per frame
    void Update()
    {
        transform.position -= transform.forward * Time.deltaTime * NoteSpeed;
    }
}
