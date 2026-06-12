using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{

    public string Name;

    public string Description;

    public int price;

    public List<string> incompatible = new List<string>();

    public float maxhealthbuff = 0;
    public float speedbuff = 0;
    public float attackspeedbuff = 0;
    public float damagebuff = 0;
    public float sizechange = 0;
    public float accelerationbuff = 0;
    public float sidegunDamageBuff;
    public float sidegunAttackSpeedBuff;
    public float healingbuff;


    public int cooldownMultiplier;

    //unimplemented
    public int projectilesizebuff;
    public int projectileamountbuff;

    public Upgrade(string n, string D, int p)
    {

        Name = n;
        Description = D;
        price = p;
    }

    public virtual void ApplyUpgradeEffects(CharacterController2D player)
    {



    }

}
