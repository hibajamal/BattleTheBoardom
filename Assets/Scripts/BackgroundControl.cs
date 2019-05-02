using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackgroundControl : MonoBehaviour
{
    public Sprite transparent;
    public Sprite normal;
    public bool normalModeActive = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (normalModeActive)
        {
            GetComponent<Image>().sprite = normal;
        }
        else
        {
            GetComponent<Image>().sprite = transparent;
        }
    }
}
