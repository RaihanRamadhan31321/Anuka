using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

<<<<<<<< HEAD:Assets/GameManager.cs
public class GameManager : MonoBehaviour
========
public class level : MonoBehaviour
>>>>>>>> fdb306607bfd97c51150eaabafc099b1912634fc:Assets/level.cs
{
    // Start is called before the first frame update
    public List<Button> buttones = new List<Button>();
    public Button[] buttons;
    
    void Start()
    {
<<<<<<<< HEAD:Assets/GameManager.cs
        DontDestroyOnLoad(gameObject);
========
        buttons = new Button[buttons.Length];
>>>>>>>> fdb306607bfd97c51150eaabafc099b1912634fc:Assets/level.cs
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
