using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class LinePrinterSimple : TextMeshProElement
{
    protected TextMeshProUGUI m_textMeshProUGUI;

    [Header("Texts to print")]
    [TextArea(3, 10)]
    [SerializeField] protected string[] m_texts;
    protected WaitForSeconds _waitForSecondsCache_charPrintSpeed;
    protected WaitForSeconds _waitForSecondsCache_linePrintPause;

    private void Awake()
    {
        m_textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        _waitForSecondsCache_charPrintSpeed = new WaitForSeconds(UIManager.Instance.CharacterPrintSpeed);
        _waitForSecondsCache_linePrintPause = new WaitForSeconds(UIManager.Instance.LinePrintPause * 0.25f);
    }

    public void Print(Action callback)
    {
        StopAllCoroutines();
        StartCoroutine(Execute(callback));
    }

    public void Print()
    {
        StopAllCoroutines();
        StartCoroutine(Execute());
    }

    protected IEnumerator Execute(Action callback = null)
    {
        yield return null;
        foreach (string line in m_texts)
        {
            m_textMeshProUGUI.text = "";
            for (int i = 0; i < line.Length; i++)
            {
                m_textMeshProUGUI.text += line[i];
                //m_audioHandler.PlaySpecificSound("Print");
                yield return _waitForSecondsCache_charPrintSpeed;
            }

            for (int i = 0; i < 4; i++)
            {
                m_textMeshProUGUI.text = (i % 2 == 0) ? line + "_" : line;
                yield return _waitForSecondsCache_linePrintPause;
            }
        }

        callback?.Invoke();
    }
}
