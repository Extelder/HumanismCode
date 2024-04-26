using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private int _gameBuildSceneId;
    [SerializeField] private float _imageFillLerpTime = 1;
    [SerializeField] private Image _loadingStatusImage;

    private AsyncOperation _asyncOperation;
    private CompositeDisposable _disposable = new CompositeDisposable();

    private void Start()
    {
        StartCoroutine(LoadindScene());
    }

    private void OnDisable()
    {
        _disposable.Clear();
    }

    private IEnumerator LoadindScene()
    {
        yield return new WaitForSecondsRealtime(1);
        _asyncOperation = SceneManager.LoadSceneAsync(_gameBuildSceneId);
        _asyncOperation.allowSceneActivation = false;

        Observable.EveryUpdate().Subscribe(_ =>
        {
            if (_loadingStatusImage.fillAmount < 0.95f)
            {
                float progress = _asyncOperation.progress / 0.9f;
                _loadingStatusImage.fillAmount = Mathf.Lerp(_loadingStatusImage.fillAmount, progress,
                    _imageFillLerpTime * Time.deltaTime);
                return;
            }

            StartCoroutine(Load());
            _disposable.Clear();
        }).AddTo(_disposable);
    }

    private IEnumerator Load()
    {
        yield return new WaitForSecondsRealtime(2f);
        Debug.Log("Load");
        _asyncOperation.allowSceneActivation = true;
    }
}