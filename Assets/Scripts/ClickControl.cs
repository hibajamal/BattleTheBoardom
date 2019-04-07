using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickControl : MonoBehaviour
{
    public GameObject settingsScreen = null;
    public GameObject pauseScreen;
    public GameObject Roll;
    public GameObject Market;
    public GameObject Back;
    
    public GameObject GoBackOriginal1;
    public GameObject GoBackOriginal2;
    public GameObject GoBackOriginal3;
    public GameObject GoBackOriginal4;




    public RectTransform large;

    void Start()
    {
    	if (pauseScreen!= null)
    		pauseScreen.SetActive(false);
    }
    
    public void OnMouseDown()
    {
        if (gameObject.tag != "Settings" && gameObject.tag != "Exit")
        {
            print(gameObject.tag);
            GameObject.Find("Background").GetComponent<ElementsAnim>().clicked = true;
            // invoke function for time delay before scene change
            StartCoroutine(Awaiting());
        }
        else if (gameObject.tag == "Settings")
        {
            Instantiate(settingsScreen);
            //peeche ka sab disable karo
        }
        


    }
    public void openPause()
    {
    	pauseScreen.SetActive(true);
    }
    public void ZoomQuad()
    {

        Roll.SetActive(false);
        Market.SetActive(false);
        Back.SetActive(true);
        this.transform.SetAsLastSibling();
        this.transform.position = large.transform.position;
        this.transform.rotation= large.transform.rotation;
        this.transform.localScale = large.transform.localScale*2;
        Debug.Log("ZoomQuad Called");
        
        this.GetComponent<Button>().interactable = false;

    }

    public void UnzoomQuad()
    {
        GameObject a = null;
        GameObject GoBackOriginal = null;
        IList<string> s = new List<string>(){"To1st", "To2nd", "To3rd", "To4th"};
        foreach(string i in s)
        {
            if (GameObject.Find(i).GetComponent<Button>().interactable == false )
            {
                a = GameObject.Find(i);
                if (i == "To1st")
                {
                    GoBackOriginal = GoBackOriginal1;
                }
                if (i == "To2nd")
                {
                    GoBackOriginal = GoBackOriginal2;
                }
                if (i == "To3rd")
                {
                    GoBackOriginal = GoBackOriginal3;
                }
                if (i == "To4th")
                {
                    GoBackOriginal = GoBackOriginal4;
                }
                break;
            }
        }
        
        a.GetComponent<Button>().interactable = true;
        Back.SetActive(false);
        Roll.SetActive(true);
        Market.SetActive(true);

        a.transform.position = GoBackOriginal.transform.position;
        a.transform.rotation = GoBackOriginal.transform.rotation;
        a.transform.localScale = GoBackOriginal.transform.localScale;


    }
    
    public void closePause()
    {
    	pauseScreen.SetActive(false);
    }


    IEnumerator Awaiting()
    {
        print(Time.time);
        yield return new WaitForSeconds(2);
        if (gameObject.tag == "Play")
        {
            Application.LoadLevel("AfterClickOnPlay");
            Debug.Log(gameObject.tag);
        }
        else if (gameObject.tag == "Continue")
            Debug.Log(gameObject.tag);
        else if (gameObject.tag == "Instructions")
            SceneManager.LoadScene("InstructionsScreen");
        else if (gameObject.tag == "Exit")
            Application.LoadLevel("");
        print(Time.time);
        
        
    }

}
