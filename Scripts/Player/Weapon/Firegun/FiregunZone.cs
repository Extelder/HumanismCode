using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;


public class FiregunZone : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private ParticleSystem _fireParticle;
    [SerializeField] private AudioSource _fireAudioSource;
    [SerializeField] private float _fireRange;
    [SerializeField] private float _fireRate;

    [field: SerializeField] public float Damage;

    private void OnEnable()
    {
        _fireParticle.Play();
        _fireAudioSource.Play();
        StartCoroutine(Firing());
    }

    private IEnumerator Firing()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_fireRate);
            Collider[] colliders = Physics.OverlapSphere(_firePoint.position, _fireRange);

            foreach (var other in colliders)
            {
                if (other.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor enemyHitBoxPart))
                {
                    Debug.Log("FIRE");
                    Accept(enemyHitBoxPart);
                }
            }
        }
    }

    private void OnDisable()
    {
        _fireAudioSource.Stop();
        _fireParticle.Stop();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_firePoint.position, _fireRange);
    }

    public void Accept(IWeaponVisitor visitor)
    {
        visitor.Visit(this);
    }
}