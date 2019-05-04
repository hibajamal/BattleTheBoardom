using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public Sprite PistolUI;
    public Sprite RifleUI;
    public Sprite SniperUI;
    public Sprite KnifeUI;
    public int PlayerID;    //IF 1 then keep p1 on top left corner then if 2 then keep on bottom rigth corner

    /* stats player */
    public GameObject stat;
    
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

    private NavigationEase shoot;
    private GameObject knife;
    /*interactables*/
    public List<GameObject> Chance;
    public List<GameObject> treasureChest;

    public int coinCount = 0;

    private bool playerSet;

    public int diceCount;


    float currentPosition = 0;
    bool newMove = true;

    /* shooting */
    public Transform firePoint;
    public GameObject bullet;

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

        shoot = GameObject.FindObjectOfType<Nav1>().gameObject.transform.GetChild(4).gameObject.GetComponent<NavigationEase>();
        knife = GameObject.FindObjectOfType<Nav1>().gameObject.transform.GetChild(5).gameObject;

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

        if (diceCount > 0 && (right.rightClicked || left.leftClicked || down.downClicked || top.topClicked || topleft.topleftClicked 
                                || topright.toprightClicked || bottomleft.bottomleftClicked || bottomright.bottomrightClicked) && playerSet)
        {
            if (right.rightClicked)
                SetPlayerNewPosition("r");
            else if (left.leftClicked)
                SetPlayerNewPosition("l");
            else if (top.topClicked)
                SetPlayerNewPosition("t");
            else if (down.downClicked)
                SetPlayerNewPosition("b");
            else if (topright.toprightClicked)
                SetPlayerNewPosition("tr");
            else if (topleft.topleftClicked)
                SetPlayerNewPosition("tl");
            else if (bottomleft.bottomleftClicked)
                SetPlayerNewPosition("bl");
            else if (bottomright.bottomrightClicked)
                SetPlayerNewPosition("br");
        }
        
        if (diceCount == 0 && (shoot.shootClicked || knife.GetComponent<NavigationEase>().knifeClicked) && !top.topClicked
            && !down.downClicked && !left.leftClicked
            && !right.rightClicked && !prefaceShootOnce)
        {
            ShootPreface();
        }
        else if (diceCount == 0 && (shoot.shootClicked || knife.GetComponent<NavigationEase>().knifeClicked) && (top.topClicked
            || down.downClicked || left.leftClicked
            || right.rightClicked))
        {
            Debug.Log("shoot else condition");
            Shoot();
        }

        if (diceCount == 0)
        {
            newMove = true;
        }
        else
        {
            newMove = false;
        }

        if (stat.transform.Find("WeaponUI").Find("CurrentWeapon").tag != "Knife")
        {
            knife.SetActive(false);
            gun.SetActive(true);
        }
        else
        {
            gun.SetActive(false);
            knife.SetActive(true);
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
                    Debug.Log("player set " + PlayerID + " "+(b.right!=null));
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
                    Debug.Log("player set "+PlayerID);
                    b.player = this;
                    flag = true;
                }
                if (flag)
                    break;
            }
        }
    }

    string currentlyFacing;

    void SetPlayerNewPosition(string direction)
    {
        currentlyFacing = direction;
        bool flag = true;
        diceCount--;
        Debug.Log("CURRENT POSITION BEFORE MOVEMNET::::::"+currentPosition);
        if (direction == "r" && placedOnBlock.right != null)
        {
            GetComponent<RectTransform>().transform.position = new Vector3(placedOnBlock.right.obj.GetComponent<RectTransform>().transform.position.x, 
                                                                           placedOnBlock.right.obj.transform.position.y, 0);
            if (!newMove) GetComponent<RectTransform>().transform.Rotate(0, 0, -currentPosition);
            if (PlayerID == 2)
            {
                currentPosition = 270;
                GetComponent<RectTransform>().transform.Rotate(new Vector3(0, 0, currentPosition));
            }
            else
            {
                currentPosition = 0;
                GetComponent<RectTransform>().transform.Rotate(new Vector3(0, 0, 0));
            }
            placedOnBlock.right.player = this;
            placedOnBlock = placedOnBlock.right;
            right.rightClicked = false;
        }
        else if (direction == "l" && placedOnBlock.left != null)
        {
            GetComponent<RectTransform>().transform.position = new Vector3(placedOnBlock.left.obj.GetComponent<RectTransform>().transform.position.x, 
                                                                            placedOnBlock.left.obj.transform.position.y, 0);

            if (!newMove) GetComponent<RectTransform>().transform.Rotate(0,0,-currentPosition);
            if (PlayerID == 2)
            {
                currentPosition = 90;
                GetComponent<RectTransform>().transform.Rotate(new Vector3 (0, 0, 90));
            }
            else
            {
                currentPosition = 180;
                GetComponent<RectTransform>().transform.Rotate(new Vector3(0, 0, 180));
            }
            placedOnBlock.left.player = this;
            placedOnBlock = placedOnBlock.left;
            left.leftClicked = false;
        }
        else if (direction == "t" && placedOnBlock.top != null)
        {
            GetComponent<RectTransform>().transform.position = new Vector3(placedOnBlock.top.obj.GetComponent<RectTransform>().transform.position.x, 
                                                                            placedOnBlock.top.obj.transform.position.y, 0);
            if (!newMove) GetComponent<RectTransform>().transform.Rotate(0, 0, -currentPosition);
            if (PlayerID == 2)
            {
                currentPosition = 0;
                GetComponent<RectTransform>().transform.Rotate(new Vector3(0, 0, currentPosition));
            }
            else
            {
                currentPosition = 90;
                GetComponent<RectTransform>().transform.Rotate(new Vector3(0, 0, currentPosition));
            }
            placedOnBlock.top.player = this;
            placedOnBlock = placedOnBlock.top;
            top.topClicked = false;
        }
        else if (direction == "b" && placedOnBlock.bottom != null)
        {
            GetComponent<RectTransform>().transform.position = new Vector3(placedOnBlock.bottom.obj.GetComponent<RectTransform>().transform.position.x,
                                                                            placedOnBlock.bottom.obj.transform.position.y, 0);
            if (!newMove) GetComponent<RectTransform>().transform.Rotate(0, 0, -currentPosition);
            if (PlayerID == 2)
            {
                currentPosition = 180;
                GetComponent<RectTransform>().transform.Rotate(new Vector3(0, 0, currentPosition));
            }
            else
            {
                currentPosition = 270;
                GetComponent<RectTransform>().transform.Rotate(new Vector3(0, 0, currentPosition));
            }
            placedOnBlock.bottom.player = this;
            placedOnBlock = placedOnBlock.bottom;
            down.downClicked = false;
        }
        else if (direction == "tr" && placedOnBlock.diagonalTopRight != null) // top-right
        {
            GetComponent<RectTransform>().transform.position = new Vector3(placedOnBlock.diagonalTopRight.obj.GetComponent<RectTransform>().transform.position.x, 
                                                                            placedOnBlock.diagonalTopRight.obj.transform.position.y, 0);
            placedOnBlock.diagonalTopRight.player = this;
            placedOnBlock = placedOnBlock.diagonalTopRight;
            topright.toprightClicked = false;
        }
        else if (direction == "tl" && placedOnBlock.diagonalTopLeft != null)    // top - left
        {
            GetComponent<RectTransform>().transform.position = new Vector3(placedOnBlock.diagonalTopLeft.obj.GetComponent<RectTransform>().transform.position.x, 
                                                                            placedOnBlock.diagonalTopLeft.obj.transform.position.y, 0);
            
            placedOnBlock.diagonalTopLeft.player = this;
            placedOnBlock = placedOnBlock.diagonalTopLeft;
            topleft.topleftClicked = false;
        }
        else if (direction == "br" && placedOnBlock.diagonalBottomRight != null)    // bottom-right
        {
            GetComponent<RectTransform>().transform.position = new Vector3(placedOnBlock.diagonalBottomRight.obj.GetComponent<RectTransform>().transform.position.x, 
                                                                            placedOnBlock.diagonalBottomRight.obj.transform.position.y, 0);
            
            placedOnBlock.diagonalBottomRight.player = this;
            placedOnBlock = placedOnBlock.diagonalBottomRight;
            bottomright.bottomrightClicked = false;
        }
        else if (direction == "bl" && placedOnBlock.diagonalBottomLeft != null)    // bottom-left
        {
            GetComponent<RectTransform>().transform.position = new Vector3(placedOnBlock.diagonalBottomLeft.obj.GetComponent<RectTransform>().transform.position.x, 
                                                                            placedOnBlock.diagonalBottomLeft.obj.transform.position.y, 0);
            
            placedOnBlock.diagonalBottomLeft.player = this;
            placedOnBlock = placedOnBlock.diagonalBottomLeft;
            bottomleft.bottomleftClicked = false;
        }
        else
        {
            flag = false;
            diceCount++;
            right.rightClicked = left.leftClicked = top.topClicked = down.downClicked =
                topright.toprightClicked = topleft.topleftClicked = bottomleft.bottomleftClicked = bottomright.bottomrightClicked = false;
        }
        if (flag)
        {
            placedOnBlock.player = null;
            GameObject.FindObjectOfType<ClickControl>().transform.Find("Text").GetComponent<Text>().text = diceCount.ToString();
            Debug.Log("Current Rotation:::: "+currentPosition);
            PlayerInteraction();
        }
        Debug.Log(direction + " " + diceCount + " " + PlayerID);
    }

    void GenChance()
    {
         
         FindObjectOfType<CardsScript>().GenChance();
        
    }

    void GenTreasure()
    {
        FindObjectOfType<CardsScript>().GenTreasure();
    }

    void PlayerInteraction()
    {
        if (placedOnBlock.ObjectPlaced != null && placedOnBlock.obj != null)
        {
            Debug.Log(placedOnBlock.ObjectPlaced.tag);
            if (placedOnBlock.ObjectPlaced.tag == "treasurechest" && diceCount == 0)
            {
                GenTreasure();
                if (FindObjectOfType<CardsScript>().curr.tag == "Pistol")
                {

                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite = PistolUI;
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Pistol";

                }
                else if (FindObjectOfType<CardsScript>().curr.tag == "Rifle")
                {
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite =RifleUI;
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Rifle";
                }
                else if (FindObjectOfType<CardsScript>().curr.tag == "Sniper")
                {
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite = SniperUI;
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Sniper";
                }
                else if (FindObjectOfType<CardsScript>().curr.tag == "Drop")
                {
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite = KnifeUI;
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Knife";
                }
                else if (FindObjectOfType<CardsScript>().curr.tag == "HealthMinusOne")
                {
                    int a = int.Parse(stat.transform.Find("Health").Find("Number").GetComponent<Text>().text);
                    a--;
                    stat.transform.Find("Health").Find("Number").GetComponent<Text>().text = a.ToString();


                }
                else if (FindObjectOfType<CardsScript>().curr.tag == "HealthMinusTwo")
                {
                    int a = int.Parse(stat.transform.Find("Health").Find("Number").GetComponent<Text>().text);
                    a-=2;
                    stat.transform.Find("Health").Find("Number").GetComponent<Text>().text = a.ToString();


                }
            }
            else if (placedOnBlock.ObjectPlaced.tag == "chance" && diceCount == 0)
            {
                GenChance();
                if (FindObjectOfType<CardsScript>().curr.tag == "Pistol")
                {

                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite = PistolUI;
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Pistol";

                }
                else if (FindObjectOfType<CardsScript>().curr.tag == "Rifle")
                {
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite = RifleUI;
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Rifle";
                }
                else if (FindObjectOfType<CardsScript>().curr.tag == "Sniper")
                {
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite = SniperUI;
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Sniper";
                }
                else if (FindObjectOfType<CardsScript>().curr.tag == "Drop")
                {
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite = KnifeUI;
                    stat.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Knife";
                }
                else if (FindObjectOfType<CardsScript>().curr.tag == "HealthMinusOne")
                {
                    int a = int.Parse(stat.transform.Find("Health").Find("Number").GetComponent<Text>().text);
                    a--;
                    stat.transform.Find("Health").Find("Number").GetComponent<Text>().text = a.ToString();


                }
                else if (FindObjectOfType<CardsScript>().curr.tag == "HealthMinusTwo")
                {
                    int a = int.Parse(stat.transform.Find("Health").Find("Number").GetComponent<Text>().text);
                    a -= 2;
                    stat.transform.Find("Health").Find("Number").GetComponent<Text>().text = a.ToString();
                }

            }
            else if (placedOnBlock.ObjectPlaced.tag == "coins")
            {
                coinCount++;
                stat.transform.Find("Coin").Find("Number").GetComponent<Text>().text = coinCount.ToString();
                Destroy(placedOnBlock.ObjectPlaced);
                placedOnBlock.ObjectPlaced = null;
                map.GenerateCoinInTime(); 
            }
            else if (placedOnBlock.ObjectPlaced.tag == "helicopter" && diceCount == 0)
            {
                // import helicopter list and place on associated helicopter
                foreach(Helicopter heli in map.helicopters)
                {
                    if (heli.helicopter == placedOnBlock.ObjectPlaced)
                    {
                        // get helipad and place player
                        GetComponent<RectTransform>().transform.position = new Vector3(heli.helipad.GetComponent<RectTransform>().transform.position.x,
                                                                                       heli.helipad.GetComponent<RectTransform>().transform.position.y, 
                                                                                       0);
                        placedOnBlock = heli.block;
                        break;
                    }
                }
            }
            else if (placedOnBlock.ObjectPlaced.tag == "quicksand" && diceCount == 0)
            {
                int a = int.Parse(stat.transform.Find("Health").Find("Number").GetComponent<Text>().text);
                a -= 2;
                stat.transform.Find("Health").Find("Number").GetComponent<Text>().text = a.ToString();
            }
        }
    }

    GameObject bullet2;
    public GameObject knifeWeapon;
    public GameObject gun;
    bool prefaceShootOnce = false;
    bool clicked = false;
    bool ShootOnce = false;
    
    void ShootPreface()
    {
        FindObjectOfType<BackgroundControl>().normalModeActive = false;
            transform.GetChild(3).gameObject.SetActive(true);

            stat.transform.parent.GetChild(2).gameObject.SetActive(false);
            if (placedOnBlock.type != 0)
            {
                stat.transform.parent.GetChild(5).gameObject.SetActive(true);
            }
            else
            {
                stat.transform.parent.GetChild(4).gameObject.SetActive(true);
            }


            StartCoroutine(BulletBack());
            prefaceShootOnce = true;
    }

    public GameObject opponent;
    public AudioSource rifle;
    public AudioSource pistol;
    public AudioSource sniper;


    void Shoot()
    {
        if (stat.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Knife")
        {
            float angle = 0;
            /*while (angle < 20000)
            {
                transform.Rotate(0, 0, 0.1f);
                angle += 0.1f;
            }*/
            Debug.Log(angle);
            
        }
        else
        {
            Debug.Log("i have been chpsen");
                bullet2 = Instantiate(bullet, this.transform);
                bullet2.SetActive(true);
                Rigidbody2D rb = bullet2.GetComponent<Rigidbody2D>();
            Vector3 curr = placedOnBlock.obj.transform.position;
            float tileSize = placedOnBlock.obj.GetComponent<RectTransform>().rect.width;
            int factor = 10;
            Vector3 target = curr - new Vector3(0, placedOnBlock.obj.transform.position.y + factor * tileSize, 0);
            
            int range = 1;

            if (stat.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Pistol")
            {
                pistol.Play();
                range = 4;
            }
            else if (stat.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Rifle")
            {
                rifle.Play();
                range = 6;
            }
            else if (stat.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Sniper")
            {
                sniper.Play();
                range = 9;
            }
            
            if (top.topClicked)
            {
                //target = curr - new Vector3(0, placedOnBlock.obj.transform.position.y + factor * tileSize, 0);
                rb.velocity = bullet2.transform.up*1000;
            }
            else if (right.rightClicked)
            {
                //target = curr - new Vector3(placedOnBlock.obj.transform.position.y + factor * tileSize, 0, 0);
                rb.velocity = bullet2.transform.right * 1000;
            }
            else if (left.leftClicked)
            {
                //target = curr + new Vector3(-(placedOnBlock.obj.transform.position.y + factor * tileSize), 0, 0);
                rb.velocity = -(bullet2.transform.right * 1000);
            }
            else if (down.downClicked)
            {
                //target = curr + new Vector3(0, -(placedOnBlock.obj.transform.position.y + factor * tileSize), 0);
                rb.velocity = -(bullet2.transform.up * 1000);
            }
            StartCoroutine(waitForbullet());
            killOpponent(range);
        }
        shoot.shootClicked = knife.GetComponent<NavigationEase>().knifeClicked = false;
        right.rightClicked = left.leftClicked = top.topClicked = down.downClicked =
                topright.toprightClicked = topleft.topleftClicked = bottomleft.bottomleftClicked = bottomright.bottomrightClicked = false;
    }

    IEnumerator BulletBack()
    {
        yield return new WaitForSeconds(1);
        //Destroy(bullet2);
        FindObjectOfType<BackgroundControl>().normalModeActive = true;

        transform.GetChild(3).gameObject.SetActive(false);
        stat.transform.parent.GetChild(2).gameObject.SetActive(true);
        stat.transform.parent.GetChild(4).gameObject.SetActive(false);
        stat.transform.parent.GetChild(5).gameObject.SetActive(false);
    }

    private IEnumerator waitForbullet()
    {
        yield return new WaitForSeconds(0.1f);
        Destroy(bullet2);
    }

    public GameObject opponentStat;

    void killOpponent(int range)
    {
        int i = 0;
        foreach(Block b in map.blocks)
        {
            if (b == placedOnBlock)
                break;
            i++;
        }
        if (top.topClicked)
        {
            while (range > 0 && i > 0)
            {
                if (opponent.GetComponent<Player>().placedOnBlock.pos == map.blocks[i].pos)
                {
                    int a = int.Parse(stat.transform.Find("Health").Find("Number").GetComponent<Text>().text);
                    a-=2;
                    stat.transform.Find("Health").Find("Number").GetComponent<Text>().text = a.ToString();
                    break;
                }
                i -= 20;
                range--;
                Debug.Log("player not found");
                }
        }
        else if (right.rightClicked)
        {
            while (range > 0 && i < 400)
            {
                if (opponent.GetComponent<Player>().placedOnBlock.pos == map.blocks[i].pos)
                {
                    int a = int.Parse(stat.transform.Find("Health").Find("Number").GetComponent<Text>().text);
                    a -= 2;
                    stat.transform.Find("Health").Find("Number").GetComponent<Text>().text = a.ToString();
                    break;
                }
                i +=1;
                range--;
                Debug.Log("player not found");
            }
        }
        else if (left.leftClicked)
        {
            while (range > 0 && i > 0)
            {
                if (opponent.GetComponent<Player>().placedOnBlock.pos == map.blocks[i].pos)
                {
                    int a = int.Parse(stat.transform.Find("Health").Find("Number").GetComponent<Text>().text);
                    a -= 2;
                    stat.transform.Find("Health").Find("Number").GetComponent<Text>().text = a.ToString();
                    break;
                }
                i -=1;
                range--;
                Debug.Log("player not found");
            }
        }
        else if (down.downClicked)
        {
            while (range > 0 && i < 400)
            {
                if (opponent.GetComponent<Player>().placedOnBlock.pos == map.blocks[i].pos)
                {
                    int a = int.Parse(stat.transform.Find("Health").Find("Number").GetComponent<Text>().text);
                    a -= 2;
                    stat.transform.Find("Health").Find("Number").GetComponent<Text>().text = a.ToString();
                    break;
                }
                i += 20;
                range--;
                Debug.Log("player not found");
            }
        }
    }
}
