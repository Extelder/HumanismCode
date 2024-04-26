using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Pool _projectilesPool;
    [SerializeField] private FollowedObject _followedObject;

    public void Attack()
    {
        Vector3 direction = _followedObject.transform.position;
        Projectile projectile = _projectilesPool
            .GetFreeElement(_attackPoint.position, Quaternion.FromToRotation(_attackPoint.position, direction))
            .GetComponent<Projectile>();
        projectile.Initiate(direction);
    }
}