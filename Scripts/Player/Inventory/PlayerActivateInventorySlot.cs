using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivateInventorySlot : MonoBehaviour
{
    [SerializeField] private ActiveInventorySlot[] _activateSlots;

    private PlayerControls _controls;
    private ActiveInventorySlot _currentActivatedInventorySlot;

    [field: SerializeField] public ActiveItem[] ActiveItems;
    public static PlayerActivateInventorySlot Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    private void OnEnable()
    {
        _controls = new PlayerControls();

        _controls.Enable();

        _controls.Main.ActivateFirstItem.performed += context => ActivateItem(0);
        _controls.Main.ActivateSecondItem.performed += context => ActivateItem(1);
        _controls.Main.ActivateThirdItem.performed += context => ActivateItem(2);
        _controls.Main.ActivateFourthItem.performed += context => ActivateItem(3);
        _controls.Main.ActivateFifthItem.performed += context => ActivateItem(4);
        _controls.Main.ActivateSixthItem.performed += context => ActivateItem(5);
        _controls.Main.ActivateSeventhItem.performed += context => ActivateItem(6);
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

    public void ActivateItem(int id)
    {
        if (_currentActivatedInventorySlot != null)
        {
            _currentActivatedInventorySlot.TryDisActivateCurrentItem();
        }

        _activateSlots[id].TryActivateCurrentItem();
        _currentActivatedInventorySlot = _activateSlots[id];
    }
}