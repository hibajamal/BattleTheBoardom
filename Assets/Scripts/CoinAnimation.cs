using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    bool changeState;

    // Start is called before the first frame update
    void Start()
    {
        changeState = false;
        transform.localScale = new Vector3(0.55f, 0.55f, 0);
        if (transform.localScale.x > 0.4)
        {
            transform.localScale -= new Vector3(0.005f, 0.005f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(1,100) >= 50)
        {
            if (changeState)
            {
                transform.localScale -= new Vector3(0.005f, 0.005f, 0);
                if (transform.localScale.x <= 0.4)
                    changeState = !changeState;
            }
            else
            {
                transform.localScale += new Vector3(0.005f, 0.005f, 0);
                if (transform.localScale.x >= 0.5)
                    changeState = !changeState;
            }
        }
    }
}
