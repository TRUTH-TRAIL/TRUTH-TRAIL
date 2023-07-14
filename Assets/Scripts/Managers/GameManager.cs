using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { Init(); return _instance; } }
    private GameObject players;
    private HashSet<GameObject> Enemies;


    private float _aggruGauge = 0;
    public float aggroGauge { get { return _aggruGauge; } set { _aggruGauge = value; } }

    private static void Init()
    {
        if(_instance == null)
        {
            _instance = new GameManager();
        }
    }
    public void Clear()
    {
        _instance = new GameManager();
    }

}
