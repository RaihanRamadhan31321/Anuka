using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider sliderHealth;
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void updateHealth(float value)
    {
        sliderHealth.value = value;
    }
 }
