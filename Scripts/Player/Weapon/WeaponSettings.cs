using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(order = 1, menuName = "Weapon", fileName = "Settings")]
public class WeaponSettings : ScriptableObject
{
    public bool SpawnTrail;
    public bool ReturnDecal;
}