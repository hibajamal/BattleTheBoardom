using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    float t;
    public GameObject gameover;

    // Start is called before the first frame update
    void Start()
    {
        t = 120f;
    }

    // Update is called once per frame
    void Update()
    {
        t -= Time.deltaTime;
        GetComponent<Text>().text = t.ToString();
        if (t <= 0){
            gameover.SetActive(true);
            SceneManager.LoadScene("SplashScreen");
        }
    }
}
