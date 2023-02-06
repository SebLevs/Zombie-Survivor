using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Manager<WaveManager>
{
    //[Header("Wave cue as time")]

    [field: SerializeField] public SequentialStopwatch _stopwatch { get; private set; }

    protected override void OnAwake()
    {
        //_stopwatch = new SequentialStopwatch();
    }

    private void Update()
    {
        // if (!GameManager.Instance.IsPaused)
/*        if (_stopwatch.OnUpdateTime())
        {
            Debug.Log("Timer has reached target");
        }*/
        
    }
}
