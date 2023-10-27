using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Image fillImage; // Tambahkan ini dan hubungkan melalui Inspector
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHealth(float value)
    {
        sliderHealth.value = value;

        // Tambahan kode untuk mengatur warna fill image berdasarkan persentase kesehatan
        float healthPercentage = sliderHealth.value;

        if (healthPercentage <= 30f) // Jika di bawah 30%
        {
            fillImage.color = Color.red; // Ubah warna fill image menjadi merah
        }
        else
        {
            fillImage.color = Color.green; // Kembalikan warna fill image menjadi hijau
        }

        if (sliderHealth.value <= sliderHealth.minValue)
        {
            fillImage.enabled = false;
        }
    }
}
