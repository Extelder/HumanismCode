using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthUpgradeLevel")]
public class UpgradeHealthValueLevel : ScriptableObject
{
    public int Index;
    public float MaxHealthValue;
    public float Cost;
}
