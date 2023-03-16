using System;
using UnityEngine;
using UnityEngine.UI;

public class UITabulation: MonoBehaviour
{
    [Header("Controller")]
    [SerializeField] private ViewController m_controller;

    [field:Header("Tabulation elements")]
    [field: SerializeField] public Button TabButton { get; private set; }
    [field:SerializeField] public ViewElement Content { get; private set; }

    public void OnDeselectTabulation()
    {
        Content.OnHideQuick();
    }
}
