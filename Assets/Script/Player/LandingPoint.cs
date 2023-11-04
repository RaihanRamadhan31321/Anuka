using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class LandingPoint : MonoBehaviour
{
    private float xvalue;
    private float yvalue;
    private float playerSpeed;
    private GameObject player;
    private GameObject childOfPlayer;
    private PlayerMovement movement;
    public bool playerMoving;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        movement = player.GetComponent<PlayerMovement>();
        childOfPlayer = player.transform.Find("MoveLimit").gameObject;
        
        playerSpeed = movement.speed;
    }

    // Update is called once per frame
    void Update()
    {
        playerMoving = movement.moving;
        if (playerMoving == true)
        {
            Gerak();
        }
        
    }
    public void Gerak()
    {
        xvalue = Input.GetAxis("Horizontal");
        yvalue = Input.GetAxis("Vertical");

        Vector2 arah = new Vector2 (xvalue, yvalue).normalized;
        transform.Translate(arah * Time.deltaTime * playerSpeed);
    }
}
