using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIcutscene : MonoBehaviour
{
    public Animator anime;
    // Start is called before the first frame update
    void Start()
    {
        anime = GetComponent<Animator>();
        anime.Play("cutscene");
    }
}
