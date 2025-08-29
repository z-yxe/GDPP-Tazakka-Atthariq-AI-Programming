using System.Collections.Generic;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    private List<Pickable> pickableList = new List<Pickable>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        Debug.Log("Pickable List " + pickableList.Count);
    }

    public void OnPickablePick(Pickable pickable)
    {
        pickableList.Remove(pickable);
        Debug.Log("Pickable List " + pickableList.Count);

        if (pickableList.Count <= 0)
        {
            Debug.Log("Win");
        }
    }
}
