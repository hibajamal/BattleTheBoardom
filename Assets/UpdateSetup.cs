using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UpdateSetup : MonoBehaviour
{
    
    public GameObject OccurenceChance;
    public GameObject OccurenceChest;
    public GameObject OccurenceCoin;
    public GameObject OccurenceQsand;
    public GameObject OccurenceHelicopter;
    public GameObject OccurenceEmpty;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OccurenceEmpty.GetComponent<Text>().text =(100- (int.Parse(OccurenceChance.GetComponent<Text>().text) + int.Parse(OccurenceChest.GetComponent<Text>().text)
            + int.Parse(OccurenceCoin.GetComponent<Text>().text) + int.Parse(OccurenceQsand.GetComponent<Text>().text)
            + int.Parse(OccurenceHelicopter.GetComponent<Text>().text))).ToString();
            


    }
}
