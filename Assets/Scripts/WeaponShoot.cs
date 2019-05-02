using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required when using Event data.
using UnityEngine.UI;

public class WeaponShoot : MonoBehaviour, IPointerDownHandler
{
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //
    }
    /// <summary>
    ///  get block no. compare and pass to player script
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (Block b in FindObjectOfType<MapGen>().blocks)
        {
            if (b.obj != null && b.obj.GetComponent<RectTransform>().transform.position == this.GetComponent<RectTransform>().transform.position)
            {
                FindObjectOfType<Player>().GetComponent<Player>().clicked = b;
                Debug.Log(b.pos.x+", "+b.pos.y);
                break;
            }
        }
    }
}
