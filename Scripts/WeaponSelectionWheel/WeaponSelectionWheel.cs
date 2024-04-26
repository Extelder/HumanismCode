using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionWheel : MonoBehaviour
{
    [SerializeField] private GameObject[] _weapons;

    private Vector2 _normalizedMousePosition;
    private float _currentAngle;
    private int _selection;
    private int _previousSelection;


    private void Update()
    {
        _normalizedMousePosition = new Vector2(Input.mousePosition.x - Screen.width / 2,
            Input.mousePosition.y - Screen.height / 2);
        _currentAngle = Mathf.Atan2(_normalizedMousePosition.y, _normalizedMousePosition.x) * Mathf.Rad2Deg;

        _currentAngle = (_currentAngle + 360) % 360;

        _selection = (int)_currentAngle / 45;
    }
}