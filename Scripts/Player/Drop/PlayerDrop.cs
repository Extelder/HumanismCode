using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrop : MonoBehaviour
{
    [SerializeField] private Transform _dropPlace;

    public static PlayerDrop Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    public void Drop(Item item, int amount)
    {
        Instantiate(item.Prefab, _dropPlace.position, Quaternion.identity).GetComponent<PickupItem>().Amount = amount;
    }
}