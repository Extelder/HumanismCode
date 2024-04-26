using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class WeaponTrail : MonoBehaviour
{
    private CompositeDisposable _disposable = new CompositeDisposable();

    public void OnSpawn(Vector3 endPosition, float speed)
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            transform.position = Vector3.Lerp(transform.position, endPosition, speed * Time.deltaTime);
        }).AddTo(_disposable);
        Destroy(gameObject, 2f);
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }
}