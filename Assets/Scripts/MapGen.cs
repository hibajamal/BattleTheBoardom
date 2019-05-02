using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using UnityEngine.EventSystems;

public class PositionOnGrid
{
    public int x, y;

    public PositionOnGrid()
    {
        x = y = 0;
    }

    public PositionOnGrid(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class Block{
    public GameObject obj;  //associated gameobject
    public Player player;
    public PositionOnGrid pos;
    public GameObject ObjectPlaced;
    public int type;
    
    // connections

    public Block right, left, top, bottom, diagonalTopRight
                                         , diagonalTopLeft
                                         , diagonalBottomRight
                                         , diagonalBottomLeft;

    public Block()
    {
        obj = null;
        player = null;
        ObjectPlaced = null;
        right = left = top = bottom = diagonalTopRight = diagonalTopLeft = diagonalBottomRight = diagonalBottomLeft = null;
    }
};

public class Helicopter
{
    public GameObject helicopter;
    public GameObject helipad;
    public Block block;

    public Helicopter()
    {
        helipad = helicopter = null;
        block = null;
    }

    public Helicopter(GameObject h)
    {
        helicopter = h;
        helipad = null;
        block = null;
    }
};

public class MapGen : MonoBehaviour
{
    /* block prefabs */
    public GameObject simpleBlock, bottomRightPoint, bottomLeftPoint, topRightPoint, topLeftPoint, blockSlopePos, blockSlopeNeg;

    /* gameObjects for board */
    public GameObject treasureChest, chanceCard, coins, helicopter, helipad, quickSand;
    

    /*  simpleblock tilesize */
    public float tileSize;

    /* list containing the entire map */
    public List<Block> blocks = new List<Block>();
    public bool mapSet = false;

    /* list containing helicopter-helipad sets */
    public List<Helicopter> helicopters = new List<Helicopter>();

    /* list containing quadrants */
    public List<Block> firstQuad, secQuad, thirdQuad, fourthQuad;

    /* list containing path*/
    List<GameObject> path;
    
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        //Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(5, Screen.height));
        Vector3 worldStart = GetComponent<RectTransform>().transform.position;
        
        /* initialize quads */
        firstQuad = new List<Block>();
        secQuad = new List<Block>();
        thirdQuad = new List<Block>();
        fourthQuad = new List<Block>();

        foreach (string line in File.ReadLines(@"Assets/mapGen.txt", Encoding.UTF8))
        {
            int j = 0;
            foreach (char el in line)
            {
                GameObject newBlock;
                tileSize = simpleBlock.GetComponent<RectTransform>().rect.width;
                Block block = new Block();
                if (el != 'x')
                {
                    // each block assigned position+gameobject
                    if (el == '0')
                    {
                        newBlock = Instantiate(simpleBlock, this.transform);
                        block.type = 0;
                    }
                    else if (el == '1')
                    {
                        newBlock = Instantiate(bottomRightPoint, this.transform);
                        block.type = 1;
                    }
                    else if (el == '2')
                    {
                        newBlock = Instantiate(topRightPoint, this.transform);
                        block.type = 2;
                    }
                    else if (el == '3')
                    {
                        newBlock = Instantiate(topLeftPoint, this.transform);
                        block.type = 3;
                    }
                    else if (el == '4')
                    {
                        newBlock = Instantiate(bottomLeftPoint, this.transform);
                        block.type = 4;
                    }
                    else if (el == '5')
                    {
                        newBlock = Instantiate(blockSlopePos, this.transform);
                        block.type = 5;
                    }
                    else 
                    {
                        newBlock = Instantiate(blockSlopeNeg, this.transform);
                        block.type = 6;
                    }
                    float factor = ((Screen.width * (0.004485f)) + (Screen.height * (-0.0075f)));
                    tileSize *= factor;

                    block.obj = newBlock;
                    newBlock.GetComponent<RectTransform>().transform.position = new Vector3(worldStart.x + tileSize / 2 + (tileSize * j), 
                                                              worldStart.y - tileSize / 2 - (tileSize * i), 
                                                              0);

                }
                block.pos = new PositionOnGrid(j, i);
                PlaceInQuad(block);
                blocks.Add(block);
                ++j;
            }
            ++i;
        }
        CreateConnections();
        mapSet = true;
    }
    
        // Update is called once per frame
        void Update()
    {
        //
    }

    void CreateConnections()
    {
        int i = 0, check;
        
        // create connections
        foreach (Block b in blocks)
        {
            check = i - (20 + 1);   // check diagonal top left
            if (check > 0 && check < 400)   // if within range
            {
                if ((b.type == 0 & (blocks[check].type == 3 || blocks[check].type == 6)) || ((b.type == 1 || b.type == 6) && (blocks[check].type == 0 || blocks[check].type == 3 || blocks[check].type == 6)))
                    if (blocks[check].obj != null)
                        b.diagonalTopLeft = blocks[check];
            }

            check = i - (20);       // check top
            if (check >= 0 && check < 400)   
            {
                if ((blocks[check].type == 0 && b.type == 0 || b.type == 2 || b.type == 3) || (blocks[check].type == 1 || blocks[check].type == 4 && b.type == 0 || b.type == 2 || b.type == 3))
                    if (blocks[check].obj != null)
                        b.top = blocks[check];
            }

            check = i - (20 - 1); // check diagonal top right
            if (check >= 0 && check < 400)   
            {
                if ((b.type == 0 & (blocks[check].type == 2 || blocks[check].type == 5)) || ((b.type == 4 || b.type == 5) && (blocks[check].type == 0 || blocks[check].type == 2 || blocks[check].type == 5)))
                    if (blocks[check].obj != null)
                    {
                        b.diagonalTopRight = blocks[check];
                    }
            }

            check = i - 1; // check left
            if (check >= 0 && check < 400)
            {
                if (blocks[check].type == 1 || blocks[check].type == 2 || blocks[check].type == 0)
                    if (blocks[check].obj != null)
                        b.left = blocks[check];
            }

            check = i + 1; // check right
            if (check >= 0 && check < 400)
            {
                if (blocks[check].type == 0 || blocks[check].type == 3 || blocks[check].type == 4)
                    if (blocks[check].obj != null)
                    {
                        b.right = blocks[check];
                        if (b.pos.x == 0 && b.pos.y == 0)
                            Debug.Log("connected");
                    }
            }

            check = i + (20-1); // check diagonal bottom left
            if (check >= 0 && check < 400)
            {
                if (((b.type == 2 || b.type == 5) && (blocks[check].type == 0 || blocks[check].type == 4 || blocks[check].type == 5)) || (b.type == 0 && (blocks[check].type == 4 || blocks[check].type == 5)))
                    if (blocks[check].obj != null)
                        b.diagonalBottomLeft = blocks[check];
            }

            check = i + 20; // check bottom
            if (check >= 0 && check < 400)
            {
                if ((blocks[check].type == 0 && b.type == 1 || b.type == 4 || b.type == 0) || (blocks[check].type == 2 || blocks[check].type == 3 && b.type == 0 || b.type == 1 || b.type == 4))
                    if (blocks[check].obj != null)
                        b.bottom = blocks[check];
            }

            check = i + (20 + 1); // check diagonal right bottom
            if (check >= 0 && check < 400)
            {
                if (((b.type == 3 || b.type == 6) && (blocks[check].type == 0 || blocks[check].type == 1 || blocks[check].type == 6)) || (b.type == 0 && (blocks[check].type == 1 || blocks[check].type == 6)))
                    if (blocks[check].obj != null)
                        b.diagonalBottomRight = blocks[check];
            }
            ++i;
            if (b.obj != null)
            {
                //place object with random probability 
                int a = Random.Range(0, 100);
                if (b.ObjectPlaced == null)
                    PlaceObjectRandomly(b, a);
                
            }
        }
    }

    void PlaceObjectRandomly(Block b, int probability)
    {
        bool flag = true;
        if (probability >= 10 && probability < /*prob tresure*/ 20)
            PlaceTreasureChest(b);
        else if (probability >= 20 + 10 && probability < 20 + 10 +/*  chance prob  */ 10)   // 30 to 40
            PlaceChance(b);
        else if (probability >= 20 + 10 + 10 && probability < 20 + 10 + 10 +/* prob coin  */10) // 30 to 50
            PlaceCoins(b);
        else if (probability >= 20 + 10 + 10 + 10 && probability < 20 + 10 + 10 + 10 +/* prob helicopter */ 10 / 2) // 50 to 55
            PlaceHelicopter(b);
        else if (probability >= 20 + 10 + 10 + 10 + 10 / 2 && probability < 20 + 10 + 10 + 10 + 10 / 2 + /* prob quicksand */ 10)   // 55 to 65
            PlaceQuickSand(b);
        else
            flag = false;

        /*place position*/
        if (flag)
        {
            b.ObjectPlaced.GetComponent<RectTransform>().transform.position = new Vector3(b.obj.GetComponent<RectTransform>().transform.position.x,
                                                                                           b.obj.GetComponent<RectTransform>().transform.position.y,
                                                                                           0);
        }
    }

    /* for zoom purposes, blocks placed in quads */
    void PlaceInQuad(Block b)
    {
        if (b.pos.x < 10) // lies in left side
        {
            if (b.pos.y < 10)     // lies in the top
            {
                firstQuad.Add(b);
            }
            else            // lies in the bottom
            {
                thirdQuad.Add(b);
            }
        }
        else       // lies in right side
        {
            if (b.pos.y < 10)     // lies in the top
            {
                secQuad.Add(b);
            }
            else            // lies in the bottom
            {
                fourthQuad.Add(b);
            }
        }
    }
    
    void PlaceTreasureChest(Block b)
    {
        b.ObjectPlaced = Instantiate(treasureChest, this.transform);
    }

    void PlaceChance(Block b)
    {
        b.ObjectPlaced = Instantiate(chanceCard, this.transform);
    }

    void PlaceCoins(Block b)
    {
        b.ObjectPlaced = Instantiate(coins, this.transform);
    }

    void PlaceQuickSand(Block b)
    {
        b.ObjectPlaced = Instantiate(quickSand, this.transform);
    }

    void PlaceHelicopter(Block b)
    {
        b.ObjectPlaced = Instantiate(helicopter, this.transform);
        Helicopter h = new Helicopter(b.ObjectPlaced);
        helicopters.Add(h);
        PlaceHelipad(h, PlacedInQuadNo(b));
    }

    void PlaceHelipad(Helicopter helicopter,int quad)
    {
        int placeQuad = (quad % 4) + 1;
        List<Block> b;
        if (placeQuad == 1)
            b = firstQuad;
        else if (placeQuad == 2)
            b = secQuad;
        else if (placeQuad == 3)
            b = thirdQuad;
        else
            b = fourthQuad;

        foreach(Block q in b)
        {
            if (q.ObjectPlaced == null && q.obj != null)
            {
                q.ObjectPlaced = Instantiate(helipad, this.transform);
                q.ObjectPlaced.GetComponent<RectTransform>().transform.position = q.obj.GetComponent<RectTransform>().transform.position;
                helicopter.helipad = q.ObjectPlaced;
                helicopter.block = q;
                break;
            }
        }
    }

    int PlacedInQuadNo(Block b)
    {
        if (b.pos.x < 10)
        {
            if (b.pos.y < 10)
            {
                return 1;
            }
            else
            {
                return 3;
            }
        }
        else
        {
            if (b.pos.y < 10)
            {
                return 2;
            }
            else
            {
                return 4;
            }
        }
    }

    // random new coin to be generated after player steps on one
    public void GenerateCoinInTime()
    {
        Invoke("GenerateCoin", 15);
    }

    void GenerateCoin()
    {
        foreach (Block b in blocks)
        {
            if (b.ObjectPlaced == null && b.obj != null)
            {
                Debug.Log("COIN GENERATED " + b.pos.x + " " + b.pos.y);
                b.ObjectPlaced = Instantiate(coins, this.transform);
                b.ObjectPlaced.GetComponent<RectTransform>().transform.position = new Vector3(b.obj.GetComponent<RectTransform>().transform.position.x,
                                                                                           b.obj.GetComponent<RectTransform>().transform.position.y,
                                                                                           0);
                break;
            }
        }
    }
    
}
