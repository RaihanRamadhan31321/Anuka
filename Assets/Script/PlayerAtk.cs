using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : MonoBehaviour
{
    private Animator anim;
    private bool check;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            gameObject.SetActive(true);
            anim.SetBool("isChild", true);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            anim.SetBool("isChild", false);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && anim.GetBool("isAttacking") == true)
        {
            Debug.Log("Here");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.tag == "Enemy")
        {
            Debug.Log("ENTER");
        }*/   
    }
}
