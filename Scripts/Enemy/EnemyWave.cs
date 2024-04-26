using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWave : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemiesInWave;

    public UnityEvent OnWaveEnd;

    public void StartChecking()
    {
        StartCoroutine(WaitingForNonEnemiesInWave());
    }

    private IEnumerator WaitingForNonEnemiesInWave()
    {
        yield return new WaitUntil(NonEnemiesInWave);
        OnWaveEnd?.Invoke();
    }

    private bool NonEnemiesInWave()
    {
        for (int i = 0; i < _enemiesInWave.Length; i++)
        {
            if (_enemiesInWave[i].activeSelf == true)
            {
                return false;
            }
        }

        return true;
    }
}