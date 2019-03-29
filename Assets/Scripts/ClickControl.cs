using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickControl : MonoBehaviour
{
    public GameObject settingsScreen = null;
    public GameObject pauseScreen =null;
    
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
        else if (gameObject.tag == "PauseButton")
        {
        	Debug.Log(123);
        	Instantiate(pauseScreen);
        }
        if (gameObject.tag == "No")
        {
        	Destroy(pauseScreen);
        }
    }
    public void openPause()
    {
    	Instantiate(pauseScreen);
    }

    IEnumerator Awaiting()
    {
        print(Time.time);
        yield return new WaitForSeconds(3);
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
