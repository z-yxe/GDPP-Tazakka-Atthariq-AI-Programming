using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Canvas canvas;

    private void LateUpdate()
    {
        if (Camera.main != null)
        {
            canvas.transform.rotation = Camera.main.transform.rotation;
        }
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthSlider.value = currentHealth / maxHealth;
    }
}
