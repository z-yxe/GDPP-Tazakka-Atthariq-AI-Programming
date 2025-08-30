using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    private List<Pickable> pickableList = new List<Pickable>();

    void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        Pickable[] pickableObjects = GameObject.FindObjectsByType<Pickable>(FindObjectsSortMode.None);

        for (int i = 0; i < pickableObjects.Length; i++)
        {
            pickableList.Add(pickableObjects[i]);
            pickableObjects[i].onPicked += OnPickablePick;
        }
    }

    public void OnPickablePick(Pickable pickable)
    {
        pickableList.Remove(pickable);

        if (pickable.pickableObject == PickableType.PowerUp)
        {
            player.PickedPowerUp();
        }

        if (pickableList.Count <= 0)
        {
            Debug.Log("Win");
        }
    }
}
