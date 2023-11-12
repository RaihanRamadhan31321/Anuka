using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobil : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    private PlayerMovement player;
    [SerializeField]private float kecepatan;
    [SerializeField] private float pental;
    [Min(0)]
    [SerializeField] private float counterPental;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            MobilMaju();
        }
    }
    void MobilMaju()
    {
        rb.velocity = new Vector2(kecepatan, 0);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.landing.SetActive(true);
            player.child.SetActive(false);
            player.animator.SetBool("getHit", true);
            player.landing.transform.position = player.child.transform.position;
            player.rb.gravityScale = 3;
            Vector2 counter;
            if (transform.position.x < player.transform.position.x)
            {   
                counter = new Vector2(-counterPental, pental);
            }
            else
            {
                counter = new Vector2(Mathf.Abs(counterPental), pental);
            }
            player.rb.velocity = counter;
        }
    }
}
