using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnableRandomObjectsInArray : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;

    private void OnEnable()
    {
        _objects[Random.Range(0, _objects.Length)].SetActive(true);
    }
}