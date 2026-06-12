using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeApplier
{

    static CharacterController2D player = CharacterController2D.Instance;

    public static void ApplyUpgradesToPlayer()
    {
        player.maxhp = 10;
        player.speed = 6;
        player.baseCooldown = 1;
        player.baseDamage = 0;
        player.baseSideDamage = 0;
        player.basesideCooldown = 1;
        player.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        player.Acceleration = 12;
        player.healrate = 0.0005f;

        foreach (var U in player.Upgrades)
        {

            player.maxhp += U.maxhealthbuff;
            player.speed += U.speedbuff;
            player.baseCooldown += U.attackspeedbuff;
            player.basesideCooldown -= U.sidegunAttackSpeedBuff;
            player.baseDamage += U.damagebuff;
            player.transform.localScale = new Vector3(player.transform.localScale.x + U.sizechange, player.transform.localScale.y + U.sizechange, player.transform.localScale.z + U.sizechange);
            player.Acceleration += U.accelerationbuff;
            player.healrate += U.healingbuff;

            U.ApplyUpgradeEffects(player);
        }


    }
}
