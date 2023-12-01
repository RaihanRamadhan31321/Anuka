using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class level : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Button> buttones = new List<Button>();
    public Button[] buttons;
    
    void Start()
    {
        buttons = new Button[buttons.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
