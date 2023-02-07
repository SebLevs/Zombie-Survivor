using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Header("Enemy spawning")]
    [Range(1, 10)][SerializeField] private float _tickRange;
    [SerializeField] private SequentialStopwatch _spawnerStopWatch;
}
