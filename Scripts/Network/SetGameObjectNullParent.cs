using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Mirror;
using UnityEngine;

public class SetGameObjectNullParent : MonoBehaviour
{
    private void Awake()
    {
        transform.parent = null;
    }
}