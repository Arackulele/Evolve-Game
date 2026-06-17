using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopUI : MonoBehaviour
{

    public List<Upgrade> Upgrades = new List<Upgrade>();

    public GameObject shop1;

    public CharacterController2D cha;

    int price1;

    string SelectedGun;

    string SelectedSideGun1;

    string SelectedSideGun2;

    string SelectedTail;

    public GameObject shop2;

    int price2;

    public GameObject shop3;

    int price3;

    public GameObject shop4;

    int price4;

    public GameObject sideshop1;

    public GameObject sideshop2;

    public GameObject sideshop3;

    public GameObject sideshop4;

    List<GameObject> SideShops;

    Upgrade[] sideshopselected = new Upgrade[4];

    int[] sideshopprices = new int[4];

    void AddUpgradestoList()
    {

        Upgrades.Add(new Upgrade("Strength", "Create projectiles that deal more damage.", 8)
        { damagebuff = 0.2f });

        Upgrades.Add(new Upgrade("Rapid", "Create projectiles faster.", 10)
        { attackspeedbuff = 0.07f });

        Upgrades.Add(new Upgrade("Tough", "Gain more max Health.", 7)
        { maxhealthbuff = 1.4f });


        Upgrades.Add(new Upgrade("Repair", "Increase healing drastically.", 15)
        { healingbuff = 0.0005f });
        
        Upgrades.Add(new Upgrade("Restrict", "Increase healing, but decrease maximum health.", 9)
        { healingbuff = 0.001f, maxhealthbuff = -2, incompatible = new List<string> { "Expand" } });

        Upgrades.Add(new Upgrade("Expand", "Increase maximum health, but decrease healing.", 9)
        { healingbuff = -0.0003f, maxhealthbuff = 4, incompatible = new List<string> { "Restrict" } });



        Upgrades.Add(new Upgrade("Accelerate", "Upgrade your Aerodynamics, letting you shoot and move slightly faster.", 9)
        { speedbuff = 0.6f, attackspeedbuff = 0.1f });

        Upgrades.Add(new Upgrade("Fortify", "Upgrade your outer Shell, letting you deal slightly more damage and gaining some health.", 11)
        { maxhealthbuff = 1f, damagebuff = 0.6f });


        Upgrades.Add(new Upgrade("Shrink", "Become smaller, allowing you to move faster at the cost of some health.", 14)
        { sizechange = - 0.2f, maxhealthbuff = -2f, speedbuff = 3f, incompatible = new List<string> { "Grow" } });

        Upgrades.Add(new Upgrade("Grow", "Become larger, gaining more health but making it more difficult to maneuver.", 14)
        { sizechange = 0.2f, maxhealthbuff = 3f, speedbuff = -2f, incompatible = new List<string> { "Shrink" } });


        Upgrades.Add(new Upgrade("Flurry", "Overdrive your guns, firing projectiles much more rapidly. Projectiles created will be weaker.", 16)
        { damagebuff = -0.2f, attackspeedbuff = 0.4f });

        Upgrades.Add(new Upgrade("Steer", "Improves your side fins. Accelarate much faster.", 10)
        { accelerationbuff = 3f });

        Upgrades.Add(new Upgrade("Flank", "Improve damage of any side Armaments.", 13)
        { sidegunDamageBuff = 0.1f });

        Upgrades.Add(new Upgrade("Rapid-Flank", "Improve attack speed of any side Armaments.", 13)
        { sidegunAttackSpeedBuff = 0.5f });


        Upgrades.Add(new Upgrade("Aspect", "Greatly improve performance of any side Armaments, at the cost of your main gun.", 14)
        { sidegunDamageBuff = 0.7f, sidegunAttackSpeedBuff = 0.2f, damagebuff = -0.3f, incompatible = new List<string> { "Focus" } });

        Upgrades.Add(new Upgrade("Focus", "Greatly improve performance of main Armament, at the cost of side guns.", 17)
        { sidegunDamageBuff = -0.2f, sidegunAttackSpeedBuff = -0.2f, damagebuff = 1f, attackspeedbuff = 0.4f, incompatible = new List<string> { "Aspect" } });



    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {

        if (InputSystem.actions.FindAction("UpDPad").ReadValue<float>() > 0 && StaticVars.money >= price1)
        {

            StaticVars.SpendMoney(price1);

            Time.timeScale = 1;
            this.gameObject.SetActive(false);
            cha.SetGun(SelectedGun);

        }

        if (InputSystem.actions.FindAction("LeftDPad").ReadValue<float>() > 0 && StaticVars.money >= price2)
        {

            StaticVars.SpendMoney(price2);

            Time.timeScale = 1;
            this.gameObject.SetActive(false);
            cha.SetSideGun(SelectedSideGun1, true);
            cha.SetSideGun(SelectedSideGun1, false);


        }

        if (InputSystem.actions.FindAction("RightDPad").ReadValue<float>() > 0 && StaticVars.money >= price3)
        {

            StaticVars.SpendMoney(price3);

            Time.timeScale = 1;
            this.gameObject.SetActive(false);
            cha.SetSideGun(SelectedSideGun2, false);
            cha.SetSideGun(SelectedSideGun2, true);


        }

        if (InputSystem.actions.FindAction("DownDPad").ReadValue<float>() > 0 && StaticVars.money >= price4)
        {

            StaticVars.SpendMoney(price4);

            Time.timeScale = 1;
            this.gameObject.SetActive(false);
            cha.SetTail(SelectedTail);
        }

        if (InputSystem.actions.FindAction("XButton").triggered || InputSystem.actions.FindAction("SquareButton").triggered || InputSystem.actions.FindAction("TriangleButton").triggered || InputSystem.actions.FindAction("CircleButton").triggered)
        { 

        if (InputSystem.actions.FindAction("XButton").triggered && StaticVars.money >= sideshopprices[0])
        {

           StaticVars.SpendMoney(sideshopprices[0]);

            cha.Upgrades.Add(sideshopselected[0]);

            UpgradeApplier.ApplyUpgradesToPlayer();


        }

        if (InputSystem.actions.FindAction("SquareButton").triggered && StaticVars.money >= sideshopprices[1])
        {

            StaticVars.SpendMoney(sideshopprices[1]);

            cha.Upgrades.Add(sideshopselected[1]);

            UpgradeApplier.ApplyUpgradesToPlayer();


        }

        if (InputSystem.actions.FindAction("TriangleButton").triggered && StaticVars.money >= sideshopprices[2])
        {

            StaticVars.SpendMoney(sideshopprices[2]);

            cha.Upgrades.Add(sideshopselected[2]);

            UpgradeApplier.ApplyUpgradesToPlayer();


        }

        if (InputSystem.actions.FindAction("CircleButton").triggered && StaticVars.money >= sideshopprices[3])
        {

            StaticVars.SpendMoney(sideshopprices[3]);

            cha.Upgrades.Add(sideshopselected[3]);

            UpgradeApplier.ApplyUpgradesToPlayer();


        }

        foreach(var i in SideShops)
            {

                i.SetActive(false);

            }


        }


        if (InputSystem.actions.FindAction("Cancel").triggered && isActiveAndEnabled)
        {
            Time.timeScale = 1;

            //wait  some time

            StartCoroutine(setA());

        }

    }

    public IEnumerator setA()
    {
        yield return new WaitForSeconds(0.01f);
        gameObject.SetActive(false);
        yield break;
    }

    private TMP_Text SelectedGunText;

    private TMP_Text SelectedGunText2;

    private TMP_Text SelectedGunText3;

    private TMP_Text SelectedGunText4;

    private TMP_Text SelectedGunPrice;

    private TMP_Text SelectedGunPrice2;

    private TMP_Text SelectedGunPrice3;

    private TMP_Text SelectedGunPrice4;

    private bool firstTime = true;

    float pricemult = 1;

    public void UpdateShop(int wave)
    {


        pricemult = 1 + (cha.Upgrades.Count * 0.2f);


        SideShops = new List<GameObject>() { sideshop1, sideshop2, sideshop3, sideshop4 };

        foreach (var i in SideShops)
        {

            i.SetActive(true);

        }


        UpdateSidePrices();

        if (firstTime) { 
        firstTime = false;

        SelectedGunText = shop1.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        SelectedGunText2 = shop2.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        SelectedGunText3 = shop3.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

        SelectedGunText4 = shop4.transform.GetChild(0).gameObject.GetComponent<TMP_Text>();


        SelectedGunPrice = shop1.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();

        SelectedGunPrice2 = shop2.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();

        SelectedGunPrice3 = shop3.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();

        SelectedGunPrice4 = shop4.transform.GetChild(1).gameObject.GetComponent<TMP_Text>();

        }

        List<string> sidegunlist = new List<string>() { "Spike", "Burst", "Launch" };

        SelectedSideGun1 = sidegunlist.GetRandomItem();

        //maingun
        List<string> gunlist = new List<string>() { "Needle", "Fragment", "Propel", "Torrent" };
        SelectedGun = gunlist.GetRandomItem();
        SelectedGunText.SetText("Gun: " + SelectedGun);

        switch (SelectedGun)
        {
            default:
            case "Needle":
                {
                    SelectedGunPrice.SetText("8 Points");
                    price1 = (int)(8 * pricemult);
                    break;
                }

            case "Fragment":
                {
                    SelectedGunPrice.SetText("12 Points");
                    price1 = (int)(12 * pricemult);
                    break;
                }

            case "Propel":
                {
                    SelectedGunPrice.SetText("16 Points");
                    price1 = (int)(16 * pricemult);
                    break;
                }

            case "Torrent":
                {
                    SelectedGunPrice.SetText("24 Points");
                    price1 = (int)(24 * pricemult);
                    break;
                }
        }


        SelectedSideGun2 = sidegunlist.GetRandomItem();

        SelectedGunText2.SetText("Side Gun: " + SelectedSideGun1);
        SelectedGunText3.SetText("Side Gun: " + SelectedSideGun2);

        switch (SelectedSideGun1)
        {
            default:
            case "Spike":
                {
                    SelectedGunPrice2.SetText("7 Points");
                    price2 = (int)(7 * pricemult);
                    break;
                }

            case "Burst":
                {
                    SelectedGunPrice2.SetText("15 Points");
                    price2 = (int)(15 * pricemult);
                    break;
                }

            case "Launch":
                {
                    SelectedGunPrice2.SetText("13 Points");
                    price2 = (int)(13 * pricemult);
                    break;
                }

        }

        switch (SelectedSideGun2)
        {
            default:
            case "Spike":
                {
                    SelectedGunPrice3.SetText("7 Points");
                    price3 = (int)(7 * pricemult);
                    break;
                }

            case "Burst":
                {
                    SelectedGunPrice3.SetText("15 Points");
                    price3 = (int)(15 * pricemult);
                    break;
                }

            case "Launch":
                {
                    SelectedGunPrice3.SetText("13 Points");
                    price3 = (int)(13 * pricemult);
                    break;
                }

        }

        List<string> tailist = new List<string>() { "Flow", "Crush", "Toxin" };

        SelectedTail = tailist.GetRandomItem();


        SelectedGunText4.SetText("Tail: " + SelectedTail);

        switch (SelectedTail)
        {
            default:
            case "Flow":
                {
                    SelectedGunPrice4.SetText("13 Points");
                    price4 = (int)(13 * pricemult);
                    break;
                }

            case "Crush":
                {
                    SelectedGunPrice4.SetText("19 Points");
                    price4 = (int)(19 * pricemult);
                    break;
                }

            case "Toxin":
                {
                    SelectedGunPrice4.SetText("23 Points");
                    price4 = (int)(23 * pricemult);
                    break;
                }

        }


    }

    public void UpdateSidePrices()
    {

        sideshopselected = new Upgrade[4];

        if (firstTime) AddUpgradestoList();

        Upgrades = RemoveIncompatibleUpgrades(new Upgrade("", "", -1), Upgrades);

        List<Upgrade> CurUpgrades = new List<Upgrade>(Upgrades);


        sideshopselected[0] = CurUpgrades.GetRandomItem();

        AddUpgradeToShopCard(sideshop1, sideshopselected[0], 0);

        CurUpgrades = RemoveIncompatibleUpgrades(sideshopselected[0], CurUpgrades);


        sideshopselected[1] = CurUpgrades.GetRandomItem();

        AddUpgradeToShopCard(sideshop2, sideshopselected[1], 1);

        CurUpgrades = RemoveIncompatibleUpgrades(sideshopselected[1], CurUpgrades);


        sideshopselected[2] = CurUpgrades.GetRandomItem();

        AddUpgradeToShopCard(sideshop3, sideshopselected[2], 2);

        CurUpgrades = RemoveIncompatibleUpgrades(sideshopselected[2], CurUpgrades);


        sideshopselected[3] = CurUpgrades.GetRandomItem();

        AddUpgradeToShopCard(sideshop4, sideshopselected[3], 3);

        CurUpgrades = RemoveIncompatibleUpgrades(sideshopselected[3], CurUpgrades);

    }

    private void AddUpgradeToShopCard(GameObject shopcard, Upgrade Selected, int num) 
    {

        shopcard.transform.GetChild(0).GetComponent<TMP_Text>().SetText(Selected.Name);

        sideshopprices[num] = (int)(Selected.price * pricemult);
        shopcard.transform.GetChild(1).GetComponent<TMP_Text>().SetText(sideshopprices[num] + " Points");

        shopcard.transform.GetChild(2).GetComponent<TMP_Text>().SetText(Selected.Description);


    }

    private List<Upgrade> RemoveIncompatibleUpgrades(Upgrade U, List<Upgrade> Cu)
    {

        List<Upgrade> filtered = new List<Upgrade>();

    foreach (var i in Cu)
        {

            if (U.incompatible.Contains(i.Name));

            else if (cha.Upgrades.Contains(i));

            else if (sideshopselected.ToList().Contains(i));

            else filtered.Add(i);

        }

        return filtered;

    }
}
