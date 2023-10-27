using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBarrier : MonoBehaviour
{
    private MovementAlt player;
    public bool isFighting = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<MovementAlt>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isFighting = true;
            Debug.Log("Player dalam trigger");
            player.atas = (transform.position.x) - player.batasBarrier;
            player.bawah = transform.position.x + player.batasBarrier;
            transform.gameObject.SetActive(false);
        }
    }
}
