using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Timer : MonoBehaviour
{
    float t;
    public GameObject gameover;
    public bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        t = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            t -= Time.deltaTime;
            t = (float)Math.Round(t, 2);
            GetComponent<Text>().text = t.ToString();
            if (t <= 0)
            {
                gameover.SetActive(true);
                paused = true;
                StartCoroutine(waitShow());
            }
        }
    }

    IEnumerator waitShow()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("SplashScreen");
    }
}
