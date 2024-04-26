using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAmmoToWeaponsAfterEnemyDeath : MonoBehaviour
{
    [SerializeField] private WeaponAmmo[] _weaponAmmo;
    [SerializeField] private int _ammoToRocketLauncher;

    public static AddAmmoToWeaponsAfterEnemyDeath Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }

        Destroy(this);
    }

    public void OnEnemyKilled()
    {
        _weaponAmmo[0].AddAmmo(_ammoToRocketLauncher);
    }
}