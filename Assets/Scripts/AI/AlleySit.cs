using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleySit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator anime = GetComponent<Animator>();
        anime.Play("Sit");
    }
}
