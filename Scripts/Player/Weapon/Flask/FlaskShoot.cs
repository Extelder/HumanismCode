using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class FlaskShoot : WeaponShoot
{
    [SerializeField] private float _bloodCollectRate;
    [SerializeField] private int _bloodPerRate;
    [SerializeField] private FlaskAmmo _flaskAmmo;
    [SerializeField] private ParticleSystem _particleSystem;

    private bool _collecting;

    private CompositeDisposable _disposable = new CompositeDisposable();

    public override void Shoot()
    {
        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            if (GetHitCollider(out Collider collider))
            {
                if (collider.TryGetComponent<BloodContainer>(out BloodContainer bloodContainer))
                {
                    if (_flaskAmmo.CurrentAmmo < _flaskAmmo.MaxAmmo && _collecting == false &&
                        bloodContainer.CurrentBloodAmount.Value > 0)
                    {
                        if (!_particleSystem.isPlaying)
                            _particleSystem.Play();
                        _collecting = true;

                        StartCoroutine(CollectingBlood(bloodContainer));
                    }
                }
                else
                {
                    StopCollecting();
                }
            }
            else
            {
                StopCollecting();
            }
        }).AddTo(_disposable);
    }

    public void StopShooting()
    {
        _disposable.Clear();
        StopCollecting();
    }

    private void StopCollecting()
    {
        _collecting = false;
        _particleSystem.Stop();
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        StopShooting();
    }

    private IEnumerator CollectingBlood(BloodContainer bloodContainer)
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_bloodCollectRate);

            if (bloodContainer.CurrentBloodAmount.Value == 0)
            {
                StopCollecting();

                yield break;
            }

            _flaskAmmo.AddOxygen(bloodContainer.OxygenInProcents);

            if (_flaskAmmo.CurrentAmmo + _bloodPerRate >= _flaskAmmo.MaxAmmo)
            {
                bloodContainer.CurrentBloodAmount.Value -= _bloodPerRate;
                _flaskAmmo.AddAmmo(_bloodPerRate);

                continue;
            }

            if (_bloodPerRate > bloodContainer.CurrentBloodAmount.Value)
            {
                _flaskAmmo.AddAmmo(bloodContainer.CurrentBloodAmount.Value);
                bloodContainer.CurrentBloodAmount.Value = 0;

                continue;
            }

            _flaskAmmo.AddAmmo(_bloodPerRate);
            bloodContainer.CurrentBloodAmount.Value -= _bloodPerRate;
        }
    }
}