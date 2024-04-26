using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class WeaponDash : MonoBehaviour
{
    [Space(10)] [SerializeField] private CharacterController _characterController;
    [SerializeField] private Transform _camera;
    [SerializeField] private WeaponShoot _weaponShoot;
    [SerializeField] private PlayerPhysics _physics;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashSpeed;


    private void OnEnable()
    {
        _weaponShoot.Shooted += Dash;
    }

    private void OnDisable()
    {
        _weaponShoot.Shooted -= Dash;
        _physics.enabled = true;
    }

    private void Dash()
    {
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        float startTime = Time.time;

        Vector3 inputDirection = -_camera.forward;

        Vector3 direction = inputDirection;

        while (Time.time < startTime + _dashTime)
        {
            _physics.Velocity = new Vector3(0, 0, 0);
            _physics.enabled = false;

            _characterController.Move(direction * _dashSpeed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        _physics.enabled = true;
    }
}