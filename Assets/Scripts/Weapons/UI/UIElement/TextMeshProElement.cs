using System;
using TMPro;
using UnityEngine;

public class TextMeshProElement : MonoBehaviour
{
    public TextMeshProUGUI Element { get; private set; }

    private void Awake() { OnAwake(); }

    protected virtual void OnAwake() { Element = GetComponent<TextMeshProUGUI>(); }

    public void AppendCharacterNoReturn(char c) => Element.text += c;

    public string AppendCharacter(char c)
    {
        return Element.text += c;
    }

    public void RemoveLastCharacterNoReturn() => Element.text = Element.text.Remove(Element.text.Length);
    public string RemoveLastCharacter()
    {
        string newText = Element.text.Remove(Element.text.Length);
        Element.text = newText;
        return newText;
    }

    /// <summary>
    /// Format of type: "'hh' : 'mm' : 'ss' : 'ff'"<br/>
    /// Can be any individual part of the format
    /// </summary>
    public void PrintTimeInSeconds(float seconds, string format = "mm' : 'ss' : 'ff'")
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        Element.text = timeSpan.ToString(format);
    }

    /// <summary>
    /// Format of type: "'hh' : 'mm' : 'ss' : 'ff'"<br/>
    /// Can be any individual part of the format
    /// </summary>
    public static void PrintTimeInSeconds(float seconds, TextMeshProElement tmpElement, string format)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        tmpElement.Element.text = timeSpan.ToString(format);
    }
}
