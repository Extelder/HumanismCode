using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropInventorySlot : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _image;

    private ContainerSlot _currentSlot;
    private RectTransform _rectTransform;

    private void Awake()
    {
        _currentSlot = transform.GetComponentInParent<ContainerSlot>();
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_currentSlot.IsEmpty())
            return;

        _rectTransform.position = Input.mousePosition;
        // _rectTransform.position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_currentSlot.IsEmpty())
            return;
        Debug.Log(_currentSlot.CurrentItem);
        _image.color = new Color(1, 1, 1, 0.75f);
        _image.raycastTarget = false;
        transform.SetParent(transform.parent.parent);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_currentSlot.IsEmpty())
            return;

        _image.color = new Color(1, 1, 1, 1f);

        _rectTransform.SetParent(_currentSlot.transform);

        _rectTransform.localPosition = new Vector3(0, 0, 0);
        Debug.Log(eventData.pointerCurrentRaycast.gameObject);

        if (eventData.pointerCurrentRaycast.gameObject == null)
        {
            _image.raycastTarget = true;
            return;
        }

        if (eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.TryGetComponent<ContainerSlot>(
            out ContainerSlot slot))
        {
            Debug.Log("Detect " + eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);
            slot.ExchangeData(_currentSlot);
        }

        _image.raycastTarget = true;
    }
}