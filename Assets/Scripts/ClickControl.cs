using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ClickControl : MonoBehaviour
{
    public GameObject settingsScreen = null;
    
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

    IEnumerator Awaiting()
    {
        print(Time.time);
        yield return new WaitForSeconds(3);
        if (gameObject.tag == "Play")
            Debug.Log(gameObject.tag);
        else if (gameObject.tag == "Continue")
            Debug.Log(gameObject.tag);
        else if (gameObject.tag == "Instructions")
            SceneManager.LoadScene("InstructionsScreen");
        else if (gameObject.tag == "Exit")
            Application.Quit();

        print(Time.time);
    }
}
