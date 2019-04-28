using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsScript : MonoBehaviour
{
    public List<GameObject> ChanceCards;
    public List<GameObject> TreasureChest;
    Timer t;
    public GameObject curr;
    public List<GameObject> PauseList;

    // Start is called before the first frame update
    void Start()
    {
        t = FindObjectOfType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenChance()
    {
        int a = Random.Range(0,9);
        ChanceCards[a].SetActive(true);
        PauseEverything();
        curr = ChanceCards[a];
        t.paused = true;
        Invoke("Resume", 8);
    }

    public void GenTreasure()
    {
        int a = Random.Range(0, 4);
        TreasureChest[a].SetActive(true);
        PauseEverything();
        curr = TreasureChest[a];
        t.paused = true;
        Invoke("Resume", 8);
    }

    void PauseEverything()
    {
        foreach(GameObject g in PauseList)
        {
            g.SetActive(false);
        }
    }

    void Resume()
    {
        curr.SetActive(false);
        t.paused = false;
        foreach (GameObject g in PauseList)
        {
            g.SetActive(true);
        }
    }
}
