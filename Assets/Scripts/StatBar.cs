using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatBar : MonoBehaviour
{
    private GameObject p1;
    private GameObject p2;
    public GameObject gameover;

    // Start is called before the first frame update
    void Start()
    {
        p1 = transform.GetChild(0).gameObject;
        p2 = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (p1.transform.Find("Health").Find("Number").GetComponent<Text>().text == "0" 
            || p2.transform.Find("Health").Find("Number").GetComponent<Text>().text == "0")
        {
            gameover.SetActive(true);
            StartCoroutine(waitShow());
        }

    }

    IEnumerator waitShow()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("SplashScreen");
    }
}
