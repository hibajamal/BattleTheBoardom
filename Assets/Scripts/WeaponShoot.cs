using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required when using Event data.
using UnityEngine.UI;

public class WeaponShoot : MonoBehaviour, IPointerDownHandler
{
    Player p1, p2;
    private void Start()
    {
        p1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<Player>();
        p2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<Player>();
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
        /*
        foreach (Block b in FindObjectOfType<MapGen>().blocks)
        {
            if (b.obj != null && b.obj.GetComponent<RectTransform>().transform.position == this.GetComponent<RectTransform>().transform.position)
            {
                if (p1.enabled)
                    p1.clicked = b;
                else
                    p2.clicked = b;
                Debug.Log(b.pos.x+", "+b.pos.y);
                break;
            }
        }
        */
    }
}
