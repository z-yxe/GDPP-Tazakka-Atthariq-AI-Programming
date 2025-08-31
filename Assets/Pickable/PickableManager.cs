using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickableManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private ScoreManager scoreManager;
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

        scoreManager.SetMaxScore(pickableObjects.Length);
    }

    public void OnPickablePick(Pickable pickable)
    {
        pickableList.Remove(pickable);
        scoreManager.AddScore(1);

        if (pickable.pickableObject == PickableType.PowerUp)
        {
            player.PickedPowerUp();
        }

        if (pickableList.Count <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
