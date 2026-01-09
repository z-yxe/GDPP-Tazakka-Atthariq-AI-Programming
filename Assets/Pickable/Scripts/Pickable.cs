using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    public PickableType pickableType;
    public Action<Pickable> onPicked;
    [SerializeField] private float respawnDelay = 10f;

    private void Awake()
    {
        if (pickableType != PickableType.Coin)
        {
            RandomizeType();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onPicked?.Invoke(this);

            if (pickableType != PickableType.Coin)
            {
                StartCoroutine(RespawnRoutine());
            } 
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator RespawnRoutine()
    {
        ToggleObject(false);

        yield return new WaitForSeconds(respawnDelay);

        RandomizeType();
        ToggleObject(true);
    }

    private void ToggleObject(bool state)
    {
        if (TryGetComponent<Renderer>(out var renderer)) renderer.enabled = state;
        if (TryGetComponent<Collider>(out var collider)) collider.enabled = state;
    }

    private void RandomizeType()
    {
        Array allTypes = Enum.GetValues(typeof(PickableType));

        List<PickableType> validTypes = new();

        foreach (PickableType type in allTypes)
        {
            if (type != PickableType.Coin)
            {
                validTypes.Add(type);
            }
        }

        int randomWeapon = UnityEngine.Random.Range(0, validTypes.Count);
        pickableType = validTypes[randomWeapon];
    }
}