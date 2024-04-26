using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiregunShoot : MonoBehaviour
{
    [SerializeField] private GameObject _fireZone;

    public void Shoot()
    {
        _fireZone.SetActive(true);
    }

    public void StopShoot()
    {
        _fireZone.SetActive(false);
    }
}