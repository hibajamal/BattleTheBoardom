using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerFade : MonoBehaviour
{
    public bool activate = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("trigger created");
    }

    // Update is called once per frame
    void Update()
    {
        //
    }
    /*
    void Triggered(Block b)
    {
        Debug.Log("TTRRRRRIIGEREREDDDD");
        created = false;
        GetComponent<RectTransform>().transform.position = new Vector3(b.obj.GetComponent<RectTransform>().transform.position.x,
                                                                            b.obj.GetComponent<RectTransform>().transform.position.y, 0);

        Debug.Log("scale: "+GetComponent<RectTransform>().transform.localScale.y);
        while (GetComponent<RectTransform>().transform.localScale.y <= 3)
        {
            GetComponent<RectTransform>().transform.localScale += new Vector3(0.1f, 0.1f, 0);
        }
        while (GetComponent<RectTransform>().transform.localScale.y >= 0)
        {
            GetComponent<RectTransform>().transform.localScale -= new Vector3(0.1f, 0.1f, 0);
        }
        Destroy(this);
    }*/
}
