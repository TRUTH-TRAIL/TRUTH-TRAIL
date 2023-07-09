using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AIAgentConfig : ScriptableObject
{
    public float _maxTime = 0.2f;
    public float _maxDistance = 1.0f;
    public float _maxSightDistance = 5.0f;
    public float _maxChasingTime = 3f;
    public float _catchDistance = 0.5f;

}
