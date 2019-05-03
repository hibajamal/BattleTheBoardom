using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElementsAnim : MonoBehaviour
{
    public GameObject logo;
    public GameObject chains;
    public GameObject blur;
    public AudioSource Pop;


    public List<GameObject> buttons;
    private float pos;
    
    private bool buttonUp;
    public bool clicked;

    // Start is called before the first frame update
    void Start()
    {
        logo.transform.position  =  new Vector3(transform.position.x, -6, 0);
        chains.transform.position = new Vector3(transform.position.x, -11.5f, 0);
        
        
        pos = -7.1f;

        for (int i = 0; i < 4; i++)
        {
            buttons[i].transform.position = new Vector3(0.1f, pos, -1);
            pos -= 1.5f;
        }


        
        clicked = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
            
        if (logo.transform.position.y < 4.12)
        {
            float y = logo.transform.position.y + .1f;
            logo.transform.position = new Vector3(logo.transform.position.x, y, -1);
            buttonUp = false;
            
                
        }
        
        if (logo.transform.position.y >= 1 && chains.transform.position.y < -.5)
        {
            buttonUp = true;
            float y = chains.transform.position.y + .1f;
            chains.transform.position = new Vector3(logo.transform.position.x, y, 0);
            if (blur.activeSelf)
            {
                blur.SetActive(false);
            }

            


        }
        if (logo.transform.position.y > -4.5f && logo.transform.position.y < -4.2f)
        {
            
                Pop.Play();

        }


        if (buttons[0].transform.position.y <= 2.68 && buttonUp)
        {
            for (int i = 3; i >= 0; i--)
            {
                pos = buttons[i].transform.position.y + 0.1f;
                buttons[i].transform.position = new Vector3(0.1f, pos, -1);
                
            }
            
        }



        if (clicked)
        {
            for (int i = 0; i < 4; i++)
            {
                Rigidbody2D sc = buttons[i].AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
            }
            clicked = false;
            
        }
       
    }
}
