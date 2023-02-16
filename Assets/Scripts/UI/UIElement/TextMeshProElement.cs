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
}
