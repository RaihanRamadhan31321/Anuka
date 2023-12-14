using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneTransition : MonoBehaviour
{
    public bool loadNext;
    public bool loadPrevious;
    public bool loadMain;
    public bool reload;
    public Animator animator;
    public float count = 1f;

    // Update is called once per frame
    void Update()
    {
        if (loadNext)
        {
            StartCoroutine(Loading(SceneManager.GetActiveScene().buildIndex + 1));

        }
        else if (loadPrevious)
        {
            StartCoroutine(Loading(SceneManager.GetActiveScene().buildIndex - 1));

        }
        else if (loadMain)
        {
            StartCoroutine(Loading(0));

        }
        else if (reload)
        {
            StartCoroutine(Loading(SceneManager.GetActiveScene().buildIndex));

        }
    }
    IEnumerator Loading(int level)
    {
        animator.SetTrigger("Transition");
        yield return new WaitForSeconds(count);
        SceneManager.LoadScene(level);
        loadMain = false;
        loadNext = false;
        loadPrevious = false;
        reload = false;

    }
}
