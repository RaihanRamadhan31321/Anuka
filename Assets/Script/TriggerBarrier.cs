using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBarrier : MonoBehaviour
{
    private PlayerMovement player;
    public bool isFighting = false;
    public bool waveStart = false;
    private bool isTriggered = false;
    [SerializeField]private List<GameObject> characters;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveStart)
        {
            CharacterSorting();
        }
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isTriggered == false)
        {
            isFighting = true;
            waveStart = true;
            player.atas = (transform.position.x) - player.batasBarrier;
            player.bawah = transform.position.x + player.batasBarrier;
            isTriggered = true;
            Debug.Log("atas = " + player.atas);
            Debug.Log("bawah = " + player.bawah);
        }
    }
    void CharacterSorting()
    {
        characters = new List<GameObject> (GameObject.FindGameObjectsWithTag("Enemy"));
        characters.Add(player.gameObject);
        characters.Sort(SortPos);
        foreach (var character in characters)
        {
            character.GetComponent<SpriteRenderer>().sortingOrder = characters.IndexOf(character);
            //SpriteRenderer sr = character.gameObject.GetComponent<SpriteRenderer>();
            //sr.sortingOrder = characters.IndexOf(character);
        }
    }
    private int SortPos(GameObject a, GameObject b)
    {
        if(a.gameObject.transform.position.y > b.gameObject.transform.position.y)
        {
            return -1;
        }else if(a.gameObject.transform.position.y < b.gameObject.transform.position.y)
        {
            return 1;
        }
        return 0;
    }
}
