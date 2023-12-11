using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownCD : MonoBehaviour
{

    public static CooldownCD Instance;
    public Image specialCD;
    public float cooldown = 3;
    bool isCooldown = false;


    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        specialCD.fillAmount = 0;
    }

    public void Update()
    {
        HugeAtt();
    }

    public void HugeAtt()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && !isCooldown)
        {
            isCooldown = true;
            specialCD.fillAmount = 1;
        }

        if (isCooldown)
        {
            specialCD.fillAmount -= 1 / cooldown * Time.deltaTime;

            if (specialCD.fillAmount <= 0)
            {
                specialCD.fillAmount = 0;
                isCooldown = false;
            }
        }
    }


}
