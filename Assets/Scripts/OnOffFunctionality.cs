using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffFunctionality : MonoBehaviour
{
    public GameObject alternateEnabled;
    public GameObject other;
    public GameObject alternateOther;

    public void OnMouseDown()
    {
        float z = transform.position.z;
        float z1 = transform.position.z == -3 ? -2 : -3;
        transform.position = new Vector3(transform.position.x, transform.position.y, z1);
        alternateEnabled.transform.position = new Vector3(alternateEnabled.transform.position.x,
                                                           alternateEnabled.transform.position.y,
                                                           z);

        other.transform.position = new Vector3(other.transform.position.x, 
                                               other.transform.position.y,
                                               z);
        alternateOther.transform.position = new Vector3(alternateOther.transform.position.x,
                                               alternateOther.transform.position.y,
                                               z1);
    }
}
