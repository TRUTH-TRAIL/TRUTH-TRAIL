using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AlleySpot : MonoBehaviour
{
    private static AlleySpot instance = null;
    [SerializeField]
    private static GameObject[] spot;
    enum State
    {
        Idle,
        Walk,
        Attack,
        Find
    }
    void Awake() {
        if (instance == null){
            instance = this;
        }
    }
    public static AlleySpot Instance
    {
        get
        {
            if (instance == null){
                return null;
            }
            return instance;
        }
    }
}
