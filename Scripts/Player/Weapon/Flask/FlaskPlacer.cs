using System;
using System.Collections;
using System.Collections.Generic;
using LincolnCpp.HUDIndicator;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

public class FlaskPlacer : RaycastBehaviour
{
    [SerializeField] private FlaskAmmo _flaskAmmo;

    [Space(10)] [SerializeField] private GameObject _flaskBlueprint;
    [SerializeField] private PlacedFlask _flask;

    public event Action<PlacedFlask> FlaskPlaced;

    private bool _blueprintSpawned;
    private GameObject _spawnedFlaskBlueprint;
    private CompositeDisposable _disposable = new CompositeDisposable();
    private Collider currentCollider;

    private bool _inputBinded;
    private PlayerControls _controls;

    private void OnEnable()
    {
        _controls = new PlayerControls();
        _controls.Enable();
    }

    private void OnDisable()
    {
        UnSubscribeEventPlaceFlaskOnInputBind();
        _controls.Disable();
        BlueprintFlaskDestroy();
    }

    private void Update()
    {
        if (GetHitCollider(out Collider collider))
        {
            RaycastHit hit = GetRaycastHit();

            if (collider.TryGetComponent<FlaskHandlerObject>(out FlaskHandlerObject flaskHandlerObject))
            {
                if (_blueprintSpawned == false)
                {
                    currentCollider = hit.collider;
                    _blueprintSpawned = true;
                    _spawnedFlaskBlueprint = Instantiate(_flaskBlueprint, hit.point, Quaternion.identity);
                    SubscribeEventPlaceFlaskOnInputBind();
                    _spawnedFlaskBlueprint.transform.rotation =
                        Quaternion.FromToRotation(_spawnedFlaskBlueprint.transform.forward, hit.normal);
                }
                else
                {
                    _spawnedFlaskBlueprint.transform.position = hit.point;
                    _spawnedFlaskBlueprint.transform.rotation =
                        Quaternion.FromToRotation(Vector3.forward, hit.normal);
                }
            }
            else
            {
                BlueprintFlaskDestroy();
            }
        }
        else
        {
            BlueprintFlaskDestroy();
        }
    }

    private void BlueprintFlaskDestroy()
    {
        UnSubscribeEventPlaceFlaskOnInputBind();

        if (_spawnedFlaskBlueprint != null)
            Destroy(_spawnedFlaskBlueprint.gameObject);
        _blueprintSpawned = false;
        _disposable.Clear();
    }

    private void PlaceFlask()
    {
        PlacedFlask placedFlask = Instantiate(_flask, _spawnedFlaskBlueprint.transform.position,
            _spawnedFlaskBlueprint.transform.rotation);

        placedFlask.Amount = _flaskAmmo.CurrentAmmo;
        placedFlask.Oxygen = _flaskAmmo.CurrentOxygen;
        placedFlask.StartSpendingBlood();

        _flaskAmmo.TrySpendAmmo(_flaskAmmo.MaxAmmo);
        _flaskAmmo.CurrentOxygen = 0;
        _flaskAmmo.TargetOxygen = _flaskAmmo.CurrentOxygen;
        _flaskAmmo.CurrentOxygenValueChanged?.Invoke(_flaskAmmo.CurrentOxygen);


        placedFlask.GetComponent<IndicatorOffScreen>().enabled = true;
        placedFlask.GetComponent<IndicatorOnScreen>().enabled = true;

        BlueprintFlaskDestroy();
        FlaskPlaced?.Invoke(placedFlask);
    }

    private void SubscribeEventPlaceFlaskOnInputBind()
    {
        if (!_inputBinded)
        {
            _inputBinded = true;
            _controls.Enable();
            _controls.Main.PlaceFlask.performed += context => PlaceFlask();
        }
    }

    private void UnSubscribeEventPlaceFlaskOnInputBind()
    {
        if (_inputBinded)
        {
            _inputBinded = false;
            _controls.Main.PlaceFlask.performed -= context => PlaceFlask();
            _controls.Disable();
        }
    }
}