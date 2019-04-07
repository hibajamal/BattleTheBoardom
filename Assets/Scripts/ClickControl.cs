using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class ClickControl : MonoBehaviour
{
    public GameObject WaitForConfirmation;
    public GameObject Buy;
    public GameObject CanvasForMainGame;
    public GameObject settingsScreen = null;
    public GameObject pauseScreen;
    public GameObject Roll;
    public GameObject Market;
    public GameObject Back;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject GoBackOriginal1;
    public GameObject GoBackOriginal2;
    public GameObject GoBackOriginal3;
    public GameObject GoBackOriginal4;
    public GameObject Gun;
    public GameObject Rifle;
    public GameObject Sniper;
    public GameObject YouHaveNCoins;
    public GameObject InsufficientFunds;
    public Sprite PistolUI;
    public Sprite RifleUI;
    public Sprite SniperUI;



    public RectTransform large;

    void Start()
    {
    	
    }
    
    public void OnMouseDown()
    {
        if (gameObject.tag != "Settings" && gameObject.tag != "Exit")
        {
            print(gameObject.tag);
            GameObject.Find("Background").GetComponent<ElementsAnim>().clicked = true;
            // invoke function for time delay before scene change
            StartCoroutine(Awaiting());
        }
        else if (gameObject.tag == "Settings")
        {
            Instantiate(settingsScreen);
            //peeche ka sab disable karo
        }
        


    }
    public void openPause()
    {
    	pauseScreen.SetActive(true);
    }
    public void ZoomQuad()
    {

        Roll.SetActive(false);
        Market.SetActive(false);
        Back.SetActive(true);
        this.transform.SetAsLastSibling();
        this.transform.position = large.transform.position;
        this.transform.rotation= large.transform.rotation;
        this.transform.localScale = large.transform.localScale*2;
        Debug.Log("ZoomQuad Called");
        
        this.GetComponent<Button>().interactable = false;

    }

    public void UnzoomQuad()
    {
        GameObject a = null;
        GameObject GoBackOriginal = null;
        IList<string> s = new List<string>(){"To1st", "To2nd", "To3rd", "To4th"};
        foreach(string i in s)
        {
            if (GameObject.Find(i).GetComponent<Button>().interactable == false )
            {
                a = GameObject.Find(i);
                if (i == "To1st")
                {
                    GoBackOriginal = GoBackOriginal1;
                }
                if (i == "To2nd")
                {
                    GoBackOriginal = GoBackOriginal2;
                }
                if (i == "To3rd")
                {
                    GoBackOriginal = GoBackOriginal3;
                }
                if (i == "To4th")
                {
                    GoBackOriginal = GoBackOriginal4;
                }
                break;
            }
        }
        
        a.GetComponent<Button>().interactable = true;
        Back.SetActive(false);
        Roll.SetActive(true);
        Market.SetActive(true);

        a.transform.position = GoBackOriginal.transform.position;
        a.transform.rotation = GoBackOriginal.transform.rotation;
        a.transform.localScale = GoBackOriginal.transform.localScale;


    }
    public void HelperToCallInChangeWeapon()
    {
        if (Player1.activeSelf)
        {
            if (Player1.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Pistol")
            {
                if (Gun.activeSelf)
                {
                    Buy.GetComponent<Button>().interactable = false;
                }
                else
                {
                    Buy.GetComponent<Button>().interactable = true;
                }
            }
            else if (Player1.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Rifle")
            {
                if (Rifle.activeSelf)
                {
                    Buy.GetComponent<Button>().interactable = false;
                }
                else
                {
                    Buy.GetComponent<Button>().interactable = true;
                }
            }
            else if (Player1.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Sniper")
            {
                if (Sniper.activeSelf)
                {
                    Buy.GetComponent<Button>().interactable = false;
                }
                else
                {
                    Buy.GetComponent<Button>().interactable = true;
                }
            }

        }
        else
        {
            if (Player2.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Pistol")
            {
                if (Gun.activeSelf)
                {
                    Buy.GetComponent<Button>().interactable = false;
                }
                else
                {
                    Buy.GetComponent<Button>().interactable = true;
                }
            }
            else if (Player2.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Rifle")
            {
                if (Rifle.activeSelf)
                {
                    Buy.GetComponent<Button>().interactable = false;
                }
                else
                {
                    Buy.GetComponent<Button>().interactable = true;
                }
            }
            else if (Player2.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Sniper")
            {
                if (Sniper.activeSelf)
                {
                    Buy.GetComponent<Button>().interactable = false;
                }
                else
                {
                    Buy.GetComponent<Button>().interactable = true;
                }
            }
        }
    }
    public void SellWeapon()
    {
        if (WaitForConfirmation.activeSelf)
        {
            WaitForConfirmation.SetActive(false);
        }
        else
        {

            if (Gun.activeSelf)
            {
                if (int.Parse(YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text) < 10)
                {
                    InsufficientFunds.SetActive(true);
                }
                else
                {
                    WaitForConfirmation.SetActive(true);
                }
            }
            else if (Rifle.activeSelf)
            {
                if (int.Parse(YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text) < 15)
                {
                    InsufficientFunds.SetActive(true);
                }
                else
                {

                    WaitForConfirmation.SetActive(true);

                }
            }
            else if (Sniper.activeSelf)
            {
                if (int.Parse(YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text) < 20)
                {
                    InsufficientFunds.SetActive(true);
                }
                else
                {

                    WaitForConfirmation.SetActive(true);

                }
            }
        }
    }
        
    public void closePause()
    {
    	pauseScreen.SetActive(false);
    }
    public void ChangeWeaponInMarketRight()
    {

        if (Gun.activeSelf)
        {
            
            Gun.SetActive(false);
            Rifle.SetActive(true);

        }
        else if (Rifle.activeSelf)
        {

            Rifle.SetActive(false);
            Sniper.SetActive(true);
        }
        else
        {
            Sniper.SetActive(false);
            Gun.SetActive(true);            
        }
        HelperToCallInChangeWeapon();
    }
    public void ChangeWeaponInMarketLeft()
    {
       
        if (Gun.activeSelf)
        {
            Gun.SetActive(false);
            Sniper.SetActive(true);

        }
        else if (Sniper.activeSelf)
        {
            Sniper.SetActive(false);
            Rifle.SetActive(true);
            
        }
        else
        {
            Rifle.SetActive(false);
            Gun.SetActive(true);
        }
        HelperToCallInChangeWeapon();
    }
    public void openMarket()
    {

        HelperToCallInChangeWeapon();
        string temp;
        if (Player1.activeSelf)
        {
            
            temp = Player1.transform.Find("Coin").Find("Number").GetComponent<Text>().text;
            
        }
        else
        {
            temp = Player2.transform.Find("Coin").transform.Find("Number").GetComponent<Text>().text;
            
        }
        
        CanvasForMainGame.SetActive(false);

        YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text = temp;

        Market.SetActive(true);
    }
    public void CloseMarket()
    {
        Buy.GetComponent<Button>().interactable = true;
        CanvasForMainGame.SetActive(true);
        Market.SetActive(false);
    }
    public void BuyWeapon()
    {

        
        
        if(Gun.activeSelf == true)
        {
            
            if (int.Parse(YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text) < 10)
            {
                InsufficientFunds.SetActive(true);
            }
            else
            {
                int a = int.Parse(YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text) - 10;
                
                if (Player1.activeSelf)
                {
                    Player1.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite = PistolUI;
                    if (Player1.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Rifle")
                    {
                        a += 7;  
                    }
                    else if (Player1.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Sniper")
                    {
                        a += 10;
                    }
                    
                    YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text = a.ToString();
                    Player1.transform.Find("Coin").Find("Number").GetComponent<Text>().text = a.ToString();
                    
                    Player1.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Pistol";
                    

                }
                else if (Player2.activeSelf)
                {
                    if (Player2.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Rifle")
                    {
                        a += 7;
                    }
                    else if (Player2.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Sniper")
                    {
                        a += 10;
                    }

                    YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text = a.ToString();
                    Player2.transform.Find("Coin").Find("Number").GetComponent<Text>().text = a.ToString();
                    Player2.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite = PistolUI;
                    Player2.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Pistol";

                }
                
                Buy.GetComponent<Button>().interactable = false;
            }
            
            
        }
        if (Rifle.activeSelf == true)
        {

            if (int.Parse(YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text) < 15)
            {
                InsufficientFunds.SetActive(true);
            }
            else
            {
                int a = int.Parse(YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text) - 15;
                
                if (Player1.activeSelf)
                {
                    if (Player1.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Pistol")
                    {
                        a += 5;
                    }
                    else if (Player1.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Sniper")
                    {
                        a += 10;
                    }
                    YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text = a.ToString();
                    Player1.transform.Find("Coin").Find("Number").GetComponent<Text>().text = a.ToString();
                    Player1.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite = RifleUI;
                    Player1.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Rifle";
                }
                else
                {
                    if (Player2.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Rifle")
                    {
                        a += 5;
                    }
                    else if (Player2.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Sniper")
                    {
                        a += 10;
                    }
                    YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text = a.ToString();
                    Player2.transform.Find("Coin").Find("Number").GetComponent<Text>().text = a.ToString();
                    Player2.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite = RifleUI;
                    Player2.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Rifle";
                }
                Buy.GetComponent<Button>().interactable = false;
            }
        }
        if (Sniper.activeSelf == true)
        {

            if (int.Parse(YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text) < 20)
            {
                InsufficientFunds.SetActive(true);
            }
            else
            {
                int a = int.Parse(YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text) - 20;
                if (Player1.activeSelf)
                {
                    if (Player1.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Pistol")
                    {
                        a += 5;
                    }
                    else if (Player1.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Rifle")
                    {
                        a += 7;
                    }
                    YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text = a.ToString();
                    Player1.transform.Find("Coin").Find("Number").GetComponent<Text>().text = a.ToString();
                    Player1.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite = SniperUI;
                    Player1.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Sniper";
                }
                else
                {
                    if (Player2.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Pistol")
                    {
                        a += 5;
                    }
                    else if (Player2.transform.Find("WeaponUI").Find("CurrentWeapon").tag == "Rifle")
                    {
                        a += 7;
                    }
                    YouHaveNCoins.transform.Find("Number").GetComponent<Text>().text = a.ToString();

                    Player2.transform.Find("Coin").Find("Number").GetComponent<Text>().text = a.ToString();
                    Player2.transform.Find("WeaponUI").Find("CurrentWeapon").GetComponent<Image>().sprite = SniperUI;
                    Player2.transform.Find("WeaponUI").Find("CurrentWeapon").tag = "Sniper";
                }
                Buy.GetComponent<Button>().interactable = false;

            }

        }
        WaitForConfirmation.SetActive(false);

    }
    public void Alright()
    {
        InsufficientFunds.SetActive(false);
    }


    public void RollDice()
    {
        if (Player1.activeSelf == true)
        {
            Player2.SetActive(true);
            Player1.SetActive(false);
        }
        else if(Player2.activeSelf)
        {
            Player2.SetActive(false);
            Player1.SetActive(true);
        }

        int a = Random.Range(1, 10);

        this.transform.Find("Text").GetComponent<Text>().text = a.ToString();
    }

    IEnumerator Awaiting()
    {
        print(Time.time);
        yield return new WaitForSeconds(2);
        if (gameObject.tag == "Play")
        {
            Application.LoadLevel("AfterClickOnPlay");
            Debug.Log(gameObject.tag);
        }
        else if (gameObject.tag == "Continue")
            Debug.Log(gameObject.tag);
        else if (gameObject.tag == "Instructions")
            SceneManager.LoadScene("InstructionsScreen");
        else if (gameObject.tag == "Exit")
            Application.LoadLevel("");
        print(Time.time);
        
        
    }

}
