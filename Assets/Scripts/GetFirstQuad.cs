using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetFirstQuad : MonoBehaviour
{
    private MapGen map;
    bool onOff;

    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.FindObjectOfType<MapGen>();
        onOff = false;
    }

    // Update is called once per frame
    void Update()
    {
        //
    }

    public void getQuad()
    {
        float tileSize; 
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(5, Screen.height));

        int x, y;
        x = y = 0;

        if (map.mapSet)
        {
            foreach (Block b in map.firstQuad)
            {
                tileSize = map.blocks[0].obj.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
                GameObject b1 = new GameObject();
                if (b.obj != null)
                {
                    b1 = Instantiate(b.obj);
                    tileSize *= (0.1f + .15f);
                    b1.transform.localScale += new Vector3(0.1f, 0.1f, 0);
                    b1.transform.position = new Vector3(worldStart.x + tileSize / 2 + (tileSize * x),
                                                        worldStart.y - tileSize / 2 - (tileSize * y),
                                                        -2);

                    Debug.Log("*****"+x + "," + y);
                }
                else
                    Debug.Log(x + "," + y);
                x++;
                if (x == 10)
                {
                    x = 0;
                    y++;
                }
            }
        }
    }
}
