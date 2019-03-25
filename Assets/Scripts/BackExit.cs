using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackExit : MonoBehaviour
{
    public GameObject destroyObject;

    public void OnMouseDown()
    {
        Destroy(destroyObject);
    }
}
