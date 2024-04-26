using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInventorySlot : MonoBehaviour
{
    [SerializeField] private ContainerSlot _containerSlot;

    private ActiveItem _activeItem;

    private void OnEnable()
    {
        _containerSlot.ItemValuesChanged += OnItemValuesChanged;
    }

    private void OnDisable()
    {
        _containerSlot.ItemValuesChanged -= OnItemValuesChanged;
    }

    private void OnItemValuesChanged()
    {
        if (_activeItem != null)
            if (_containerSlot.CurrentItem != _activeItem?.CurrentItem && _activeItem.gameObject.activeSelf)
                TryDisActivateCurrentItem();
        if (_containerSlot.CurrentItem.CanActivated)
        {
            for (int i = 0; i < PlayerActivateInventorySlot.Instance.ActiveItems.Length; i++)
            {
                if (PlayerActivateInventorySlot.Instance.ActiveItems[i].CurrentItem == _containerSlot.CurrentItem)
                {
                    _activeItem = PlayerActivateInventorySlot.Instance.ActiveItems[i];
                    Debug.Log("Dene");
                    return;
                }
            }
        }

        _activeItem = null;
    }

    public void TryDisActivateCurrentItem()
    {
        if (_activeItem != null)
            _activeItem.gameObject.SetActive(false);
    }

    public void TryActivateCurrentItem()
    {
        if (_activeItem != null)
            _activeItem.gameObject.SetActive(true);
    }
}