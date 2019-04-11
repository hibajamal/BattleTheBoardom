using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int PlayerID;    //IF 1 then keep p1 on top left corner then if 2 then keep on bottom rigth corner

    private Block placedOnBlock;
    private MapGen map;

    private NavigationEase top;
    private NavigationEase left;
    private NavigationEase right;
    private NavigationEase down;

    private NavigationEase topright;
    private NavigationEase topleft;
    private NavigationEase bottomright;
    private NavigationEase bottomleft;

    private bool playerSet;

    public int diceCount;

    // Start is called before the first frame update
    void Start()
    {
        map = GameObject.FindObjectOfType<MapGen>();
        top = GameObject.FindObjectOfType<Nav1>().gameObject.transform.GetChild(0).gameObject.GetComponent<NavigationEase>();
        left = GameObject.FindObjectOfType<Nav1>().gameObject.transform.GetChild(1).gameObject.GetComponent<NavigationEase>();
        right = GameObject.FindObjectOfType<Nav1>().gameObject.transform.GetChild(2).gameObject.GetComponent<NavigationEase>();
        down = GameObject.FindObjectOfType<Nav1>().gameObject.transform.GetChild(3).gameObject.GetComponent<NavigationEase>();

        topright = GameObject.FindObjectOfType<Nav2>().gameObject.transform.GetChild(0).gameObject.GetComponent<NavigationEase>();
        topleft = GameObject.FindObjectOfType<Nav2>().gameObject.transform.GetChild(1).gameObject.GetComponent<NavigationEase>();
        bottomright = GameObject.FindObjectOfType<Nav2>().gameObject.transform.GetChild(2).gameObject.GetComponent<NavigationEase>();
        bottomleft = GameObject.FindObjectOfType<Nav2>().gameObject.transform.GetChild(3).gameObject.GetComponent<NavigationEase>();

        playerSet = false;
        diceCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // will only execute once entire map has been generated and player has not been set
        if (map.mapSet && !playerSet)
        {
            SetPlayer(PlayerID);
            playerSet = true;   // player has been set hence this condition will never be true again
        }
        
        if (diceCount > 0)
        {
            if (right.rightClicked)
                SetPlayerNewPosition(placedOnBlock, "r");
            else if (left.leftClicked)
                SetPlayerNewPosition(placedOnBlock, "l");
            else if (top.topClicked)
                SetPlayerNewPosition(placedOnBlock, "t");
            else if (down.downClicked)
                SetPlayerNewPosition(placedOnBlock, "b");
            else if (topright.toprightClicked)
                SetPlayerNewPosition(placedOnBlock, "tr");
            else if (topleft.topleftClicked)
                SetPlayerNewPosition(placedOnBlock, "tl");
            else if (bottomleft.bottomleftClicked)
                SetPlayerNewPosition(placedOnBlock, "bl");
            else if (bottomright.bottomrightClicked)
                SetPlayerNewPosition(placedOnBlock, "br");
        }
    }
  
    void SetPlayer(int ID)
    {
        if (ID == 1)
        {
            foreach (Block b in map.blocks)
            {
                bool flag = false;
                if (b.pos.x == 0 && b.pos.y == 0)
                {
                    GetComponent<RectTransform>().transform.position = new Vector3(b.obj.GetComponent<RectTransform>().transform.position.x,
                                                                                    b.obj.GetComponent<RectTransform>().transform.position.y, -1);
                    placedOnBlock = b;
                    b.player = this;
                    flag = true;
                }
                if (flag)
                    break;
            }
        }
        else
        {
            foreach (Block b in map.blocks)
            {
                bool flag = false;
                if (b.pos.x == 19 && b.pos.y == 19)
                {
                    GetComponent<RectTransform>().transform.position = new Vector3(b.obj.GetComponent<RectTransform>().transform.position.x,
                                                                                    b.obj.GetComponent<RectTransform>().transform.position.y, -1);
                    placedOnBlock = b;
                    b.player = this;
                    flag = true;
                }
                if (flag)
                    break;
            }
        }
    }

    void SetPlayerNewPosition(Block b, string direction)
    {
        bool flag = true;
        diceCount--;
        if (direction == "r" && b.right != null)
        {
            GetComponent<RectTransform>().transform.position = new Vector3(b.right.obj.GetComponent<RectTransform>().transform.position.x, b.right.obj.transform.position.y, 0);
            b.right.player = this;
            placedOnBlock = b.right;
            right.rightClicked = false;
        }
        else if (direction == "l" && b.left != null)
        {
            GetComponent<RectTransform>().transform.position = new Vector3(b.left.obj.GetComponent<RectTransform>().transform.position.x, b.left.obj.transform.position.y, 0);
            b.left.player = this;
            placedOnBlock = b.left;
            left.leftClicked = false;
        }
        else if (direction == "t" && b.top != null)
        {
            GetComponent<RectTransform>().transform.position = new Vector3(b.top.obj.GetComponent<RectTransform>().transform.position.x, b.top.obj.transform.position.y, 0);
            b.top.player = this;
            placedOnBlock = b.top;
            top.topClicked = false;
        }
        else if (direction == "b" && b.bottom != null)
        {
            GetComponent<RectTransform>().transform.position = new Vector3(b.bottom.obj.GetComponent<RectTransform>().transform.position.x, b.bottom.obj.transform.position.y, 0);
            b.bottom.player = this;
            placedOnBlock = b.bottom;
            down.downClicked = false;
        }
        else if (direction == "tr" && b.diagonalTopRight != null) // top-right
        {
            GetComponent<RectTransform>().transform.position = new Vector3(b.diagonalTopRight.obj.GetComponent<RectTransform>().transform.position.x, b.diagonalTopRight.obj.transform.position.y, 0);
            b.diagonalTopRight.player = this;
            placedOnBlock = b.diagonalTopRight;
            topright.toprightClicked = false;
        }
        else if (direction == "tl" && b.diagonalTopLeft != null)    // top - left
        {
            GetComponent<RectTransform>().transform.position = new Vector3(b.diagonalTopLeft.obj.GetComponent<RectTransform>().transform.position.x, b.diagonalTopLeft.obj.transform.position.y, 0);
            b.diagonalTopLeft.player = this;
            placedOnBlock = b.diagonalTopLeft;
            topleft.topleftClicked = false;
        }
        else if (direction == "br" && b.diagonalBottomRight != null)    // bottom-right
        {
            GetComponent<RectTransform>().transform.position = new Vector3(b.diagonalBottomRight.obj.GetComponent<RectTransform>().transform.position.x, b.diagonalBottomRight.obj.transform.position.y, 0);
            b.diagonalBottomRight.player = this;
            placedOnBlock = b.diagonalBottomRight;
            bottomright.bottomrightClicked = false;
        }
        else if (direction == "bl" && b.diagonalBottomLeft != null)    // bottom-left
        {
            GetComponent<RectTransform>().transform.position = new Vector3(b.diagonalBottomLeft.obj.GetComponent<RectTransform>().transform.position.x, b.diagonalBottomLeft.obj.transform.position.y, 0);
            b.diagonalBottomLeft.player = this;
            placedOnBlock = b.diagonalBottomLeft;
            bottomleft.bottomleftClicked = false;
        }
        else
        {
            flag = false;
            diceCount++;
        }
        if (flag)
        {
            b.player = null;
        }
        Debug.Log(direction + " " + diceCount + " " + PlayerID);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
    }
}
