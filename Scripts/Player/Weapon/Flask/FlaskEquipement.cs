using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FlaskEquipement : MonoBehaviour
{
    [SerializeField] private FlaskPlacer _flaskPlacer;

    [FormerlySerializedAs("_flask")] [SerializeField]
    public GameObject Flask;

    [SerializeField] private FlaskAmmo _flaskAmmo;

    public bool CanShow = true;
    private PlacedFlask _placedFlask;

    private PlayerControls _controls;

    public static FlaskEquipement Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Debug.Log("AlreadyHaveFlaskEquipement");
        Destroy(Instance);
    }

    private void OnEnable()
    {
        _controls = new PlayerControls();
        _controls.Enable();
        _controls.Main.FlaskTakeHide.started += context => Show();
        _controls.Main.FlaskTakeHide.canceled += context => Hide();
        _flaskPlacer.FlaskPlaced += OnFlaskPlaced;
    }

    private void OnDisable()
    {
        _controls.Main.FlaskTakeHide.started -= context => Show();
        _controls.Main.FlaskTakeHide.canceled -= context => Hide();
        _controls.Disable();
        _flaskPlacer.FlaskPlaced -= OnFlaskPlaced;
    }

    private void FlaskTakeHide()
    {
        if (Flask.activeSelf)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    public void Show()
    {
        if (CanShow && !Flask.activeSelf)
        {
            Flask.SetActive(true);
            RadialMenu.Instance.DeSelectCurrentItem();
        }
    }

    public void Hide()
    {
        if (Flask.activeSelf)
        {
            Flask.SetActive(false);
            RadialMenu.Instance.SelectCurrentItem();
        }
    }

    public void OnFlaskPlaced(PlacedFlask placedFlask)
    {
        _placedFlask = placedFlask;
        _placedFlask.FlaskEquipement = this;
        CanShow = false;
        Hide();
    }

    public void OnFlaskTaked()
    {
        CanShow = true;
        Show();
        _flaskAmmo.AddAmmo(_placedFlask.Amount);
        _flaskAmmo.CurrentOxygen = _placedFlask.Oxygen;
        _flaskAmmo.TargetOxygen = _flaskAmmo.CurrentOxygen;
        _flaskAmmo.CurrentOxygenValueChanged?.Invoke(_flaskAmmo.CurrentOxygen);
    }
}