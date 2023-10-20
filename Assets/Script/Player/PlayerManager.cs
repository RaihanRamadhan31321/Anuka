using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float health = 100;
    [SerializeField] private UIManager _uiManager;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = UIManager.instance;
        _uiManager.updateHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
