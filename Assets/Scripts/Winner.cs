using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Winner : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;

    // Start is called before the first frame update
    void Start()
    {
        if (int.Parse(p1.transform.Find("Coin").Find("Number").GetComponent<Text>().text) < int.Parse(p2.transform.Find("Coin").Find("Number").GetComponent<Text>().text))
        {
            GetComponent<Text>().text = "Player 1 wins!!!";
        }
        else if (int.Parse(p1.transform.Find("Coin").Find("Number").GetComponent<Text>().text) > int.Parse(p2.transform.Find("Coin").Find("Number").GetComponent<Text>().text))
        {
            GetComponent<Text>().text = "Player 2 wins!!";
        }
        else
        {
            GetComponent<Text>().text = "Match tied";
        }

    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
