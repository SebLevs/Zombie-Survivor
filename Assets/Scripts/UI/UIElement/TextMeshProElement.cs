using TMPro;
using UnityEngine;

public class TextMeshProElement : MonoBehaviour
{
    public TextMeshProUGUI Element { get; private set; }

    private void Awake() { OnAwake(); }

    protected virtual void OnAwake() { Element = GetComponent<TextMeshProUGUI>(); }

    public string AppendCharacter(char c)
    {
        return Element.text += c;
    }

    public string RemoveLastCharacter()
    {
        string newText = Element.text.Remove(Element.text.Length);
        Element.text = newText;
        return newText;
    }
}
