using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDataBase
{
    public static List<EnemyData> enemies = new List<EnemyData> {
        new EnemyData {
            hitsToDie = 1,
            attackPower = 5,
            moveSpeed = 12,
            distanceStaticAttack = 15,
            distanceMovingAttack = 30,
            xpToDrop = 5,
        },
        new EnemyData {
            hitsToDie = 2,
            attackPower = 10,
            moveSpeed = 10,
            distanceStaticAttack = 20,
            distanceMovingAttack = 40,
            xpToDrop = 10,
        },
        new EnemyData {
            hitsToDie = 3,
            attackPower = 20,
            moveSpeed = 5,
            distanceStaticAttack = 30,
            distanceMovingAttack = 50,
            xpToDrop = 15,
        }
    };
}
