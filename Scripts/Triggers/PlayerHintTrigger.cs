using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHintTrigger : MonoBehaviour
{
    [SerializeField] private string _rusHint;
    [SerializeField] private string _engHint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            PlayerHint.Instance.ChangeHint(_rusHint, _engHint);
            Destroy(this);
        }
    }
}