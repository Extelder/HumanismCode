using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xamin;

public class ActivateWeaponsByInput : MonoBehaviour
{
    [SerializeField] private Button[] _itemButtons;
    private PlayerControls _controls;

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
        _controls.Main.ActivateEightItem.performed += context => ActivateItem(7);
    }

    private void OnDisable()
    {
        _controls.Main.ActivateFirstItem.performed -= context => ActivateItem(0);
        _controls.Main.ActivateSecondItem.performed -= context => ActivateItem(1);
        _controls.Main.ActivateThirdItem.performed -= context => ActivateItem(2);
        _controls.Main.ActivateFourthItem.performed -= context => ActivateItem(3);
        _controls.Main.ActivateFifthItem.performed -= context => ActivateItem(4);
        _controls.Main.ActivateSixthItem.performed -= context => ActivateItem(5);
        _controls.Main.ActivateSeventhItem.performed -= context => ActivateItem(6);
        _controls.Main.ActivateEightItem.performed -= context => ActivateItem(7);
        _controls.Disable();
    }

    public void ActivateItem(int index)
    {
        if(RadialMenu.Instance.CurrentRadialButton == _itemButtons[index])
            return;
        RadialMenu.Instance.DeSelectCurrentItem();
        RadialMenu.Instance.CurrentRadialButton = _itemButtons[index];
        RadialMenu.Instance.SelectCurrentItem();
    }
}