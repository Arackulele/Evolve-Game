using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticVars
{
    public static int highscore = 0;

    public static float health = 0;

    public static float wave = 0;

    public static int money = 5;

    public static int spentmoney = 5;

    public static int metamoney = 0;

    public static void SpendMoney(int amnt)
    {

        money-= amnt;
        spentmoney += amnt;

    }

}
