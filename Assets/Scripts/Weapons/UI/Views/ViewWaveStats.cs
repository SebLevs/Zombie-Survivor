using UnityEngine;

public class ViewWaveStats : ViewElement
{
    [field:Header("Filling bar")]
    [field: SerializeField] public ViewFillingBarWithTextElement TimerViewElement { get; private set; }

    [field:Header("Wave name")]
    [field: SerializeField] public TextMeshProElement WaveElement { get; private set; }

    [field:Header("Timer")]
    [field: SerializeField] public TextMeshProElement Minutes { get; private set; }
    [field: SerializeField] public TextMeshProElement Seconds { get; private set; }
    [field: SerializeField] public TextMeshProElement Milliseconds { get; private set; }

    public void PrintTimer(float time)
    {
        Minutes.PrintTimeInSeconds(time, "mm");
        Seconds.PrintTimeInSeconds(time, "ss");
        Milliseconds.PrintTimeInSeconds(time, "ff");
    }
}
