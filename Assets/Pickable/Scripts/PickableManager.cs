using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PickableManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private List<Pickable> pickableList;

    private int coinCount = 0;

    void Start()
    {
        InitPickableList();
    }

    private void InitPickableList()
    {
        foreach (var pickableObjects in pickableList)
        {
            pickableObjects.onPicked += OnPickablePick;

            if (pickableObjects.pickableType == PickableType.Coin)
            {
                coinCount++;
            }
        }

        scoreManager.SetMaxScore(coinCount);
    }

    public void OnPickablePick(Pickable pickable)
    {
        if (pickable.pickableType == PickableType.Bazoka)
        {
            player.PickedPowerUp(PickableType.Bazoka);
        }
        else if (pickable.pickableType == PickableType.Rifle)
        {
            player.PickedPowerUp(PickableType.Rifle);
        }
        else if (pickable.pickableType == PickableType.Pistol)
        {
            player.PickedPowerUp(PickableType.Pistol);
        }
        else if (pickable.pickableType == PickableType.Coin)
        {
            scoreManager.AddScore(1);
        }

        if (coinCount <= 0)
        {
            SceneManager.LoadScene("WinScreen");
        }
    }
}
