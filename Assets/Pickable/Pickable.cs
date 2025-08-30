using System;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public PickableType pickableObject;
    public Action<Pickable> onPicked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPicked(this);
            Destroy(gameObject);
        }
    }
}