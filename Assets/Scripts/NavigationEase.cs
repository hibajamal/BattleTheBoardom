using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationEase : MonoBehaviour
{
    public AudioSource clicksound;
    public bool topClicked = false;
    public bool leftClicked = false;
    public bool rightClicked = false;
    public bool downClicked = false;

    public bool topleftClicked = false;
    public bool toprightClicked = false;
    public bool bottomleftClicked = false;
    public bool bottomrightClicked = false;

    public bool shootClicked = false;
    public bool knifeClicked = false;

    public void SendInfo()
    {
        clicksound.Play();

        if (gameObject.tag == "top")
            topClicked = true;
        else if (gameObject.tag == "left")
            leftClicked = true;
        else if (gameObject.tag == "right")
            rightClicked = true;
        else if (gameObject.tag == "down")
            downClicked = true;
        else if (gameObject.tag == "topright")
            toprightClicked = true;
        else if (gameObject.tag == "topleft")
            topleftClicked = true;
        else if (gameObject.tag == "bottomright")
            bottomrightClicked = true;
        else if (gameObject.tag == "bottomleft")
            bottomleftClicked = true;
        else if (gameObject.tag == "SHOOT")
            shootClicked = knifeClicked = true;
        Debug.Log(gameObject.tag+" "+shootClicked);
    }
    
}
