using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//왜 안들리냐..?
public class ButtonSound : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            audioSource.Play();
        }
    }
}
