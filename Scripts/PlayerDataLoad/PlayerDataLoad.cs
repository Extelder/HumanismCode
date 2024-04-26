using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataLoad : MonoBehaviour
{
    [SerializeField] private DataSettings _dataSettings;

    private void Awake()
    {
        _dataSettings.Load();
    }
}