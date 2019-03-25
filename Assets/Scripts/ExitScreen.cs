using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScreen : MonoBehaviour
{
    public string ScreenNameTo;

    public void OnMouseDown()
    {
        Debug.Log("clicked");
        SceneManager.LoadScene("SplashScreen");
    }
}
