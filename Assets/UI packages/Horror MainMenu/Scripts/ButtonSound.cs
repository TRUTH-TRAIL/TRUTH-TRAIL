using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//�� �ȵ鸮��..?
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
