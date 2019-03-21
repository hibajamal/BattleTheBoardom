using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsAnim : MonoBehaviour
{
    public GameObject logo;
    public GameObject chains;

    // Start is called before the first frame update
    void Start()
    {
        logo.transform.position  =  new Vector3(transform.position.x, -6, 0);
        chains.transform.position = new Vector3(transform.position.x, -11.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (logo.transform.position.y < 3.78)
        {
            float y = logo.transform.position.y + .1f;
            logo.transform.position = new Vector3(logo.transform.position.x, y, 0);
        }

        if (logo.transform.position.y >= 1 && chains.transform.position.y < -2.35)
        {
            float y = chains.transform.position.y + .1f;
            chains.transform.position = new Vector3(logo.transform.position.x, y, 0);
            Debug.Log(y);
        }
    }
}
