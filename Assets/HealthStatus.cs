using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthStatus : MonoBehaviour
{
    public DarahPlayer playerHealth;
    public Slider slider;
    public Image fillImage;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    private void Update()
    {
        UpdateHealthBar();

    }

    private void UpdateHealthBar()
    {
        float healthPercentage = (float)playerHealth.currentHealth / playerHealth.maxHealth * 100;

        slider.value = healthPercentage;

        if (healthPercentage <= 0.3f * 100) // Jika di bawah 30%
        {
            fillImage.color = Color.red; // Ubah warna fill image menjadi merah
        }
        else
        {
            fillImage.color = Color.green; // Kembalikan warna fill image menjadi hijau
        }

        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }
    }
}
