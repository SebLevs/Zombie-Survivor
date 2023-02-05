using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextBreathing : MonoBehaviour
{
    private TextMeshProUGUI _text;

    [SerializeField] private float _toggleWaitTime;

    [Space(10)]
    [SerializeField] private float _fontSizeModifier;
    [Min(0)][SerializeField] private float _minTextSize = 34;
    [Min(0)][SerializeField] private float _maxTextSize = 36;

    private WaitForSeconds _breatheUpDownToggleCache;
    private WaitForFixedUpdate _waitForFixedUpdateCache;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _breatheUpDownToggleCache = new WaitForSeconds(_toggleWaitTime);
        _waitForFixedUpdateCache = new WaitForFixedUpdate();
    }

    public void Breathe()
    {
        StartCoroutine(TitleCueUp());
    }

    private IEnumerator TitleCueUp()
    {
        while (!(_text.fontSize >= _maxTextSize))
        {
            yield return _waitForFixedUpdateCache;
            _text.fontSize += _fontSizeModifier;
        }
        yield return _breatheUpDownToggleCache;
        StartCoroutine(TitleCueDown());
    }

    private IEnumerator TitleCueDown()
    {
        while (!(_text.fontSize <= _minTextSize))
        {
            yield return _waitForFixedUpdateCache;
            _text.fontSize -= _fontSizeModifier;
        }
        yield return _breatheUpDownToggleCache;

        StartCoroutine(TitleCueUp());
    }
}
