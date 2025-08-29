using System;
using UnityEngine;

public enum PickableType
{
    Coin,
    PowerUp
}

public class Pickable : MonoBehaviour
{
    [SerializeField]
    private PickableType pickableObject;
    public Action<Pickable> onPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (pickableObject == PickableType.Coin)
            {
                Debug.Log("Pick Up Coin");
            }
            else if (pickableObject == PickableType.PowerUp)
            {
                Debug.Log("Pick Up Power Up");
            }

            onPicked(this);
            Destroy(gameObject);
        }
    }
}