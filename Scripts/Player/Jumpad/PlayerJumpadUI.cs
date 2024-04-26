using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerJumpadUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshProUgui;
    [SerializeField] private PlayerJumpad _jumpad;

    private void OnEnable()
    {
        _jumpad.CurrentJumpadsAmountValueChanged += OnJumpsValueChanged;
    }

    private void OnDisable()
    {
        _jumpad.CurrentJumpadsAmountValueChanged -= OnJumpsValueChanged;
    }

    private void OnJumpsValueChanged(int value)
    {
        _textMeshProUgui.text = value.ToString();
    }
}