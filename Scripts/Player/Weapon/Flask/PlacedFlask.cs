using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedFlask : MonoBehaviour, IInteractable
{
    [SerializeField] private float _rate;
    [SerializeField] private int _bloodperRate;

    public FlaskEquipement FlaskEquipement;
    public int Amount;
    public int Oxygen;

    public void StartSpendingBlood()
    {
        StartCoroutine(SpendingBlood());
    }

    public void Interact()
    {
        FlaskEquipement.OnFlaskTaked();
        Destroy(gameObject);
    }

    private IEnumerator SpendingBlood()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_rate);
            HumanHealth.Instance.Heal((HumanHealth.Instance.MaxOxygenPerRate / 1000) * Oxygen);

            Amount -= _bloodperRate;

            if (Amount <= 0)
            {
                Amount = 0;
                yield break;
            }
        }
    }
}