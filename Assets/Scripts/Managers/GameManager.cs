using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    private static GameManager _instance;
    public static GameManager Instance { get { Init(); return _instance; } }
    private HashSet<GameObject> Enemies;
    public float _playerTimeScale = 1f;
    public float PlayerTimeScale { get { return _playerTimeScale; } set { if (value >= 1) value = 1; else if (value < 0) value = 0;  _playerTimeScale = value; } }
    float _enemyTimeScale = 1f;
    public float EnemyTimeScale { get { return _enemyTimeScale * Time.timeScale; } set { if (value >= 1) value = 1; else if (value < 0) value = 0; _enemyTimeScale = value; } }


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
