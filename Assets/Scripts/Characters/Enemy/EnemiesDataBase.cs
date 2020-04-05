using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDataBase
{
    public static List<EnemyData> enemies = new List<EnemyData> {
        new EnemyData {
            attackPower = 5,
            moveSpeed = 800,
            distanceStaticAttack = 5,
            distanceMovingAttack = 15,
        },
        new EnemyData {
            attackPower = 10,
            moveSpeed = 500,
            distanceStaticAttack = 10,
            distanceMovingAttack = 30,
        },
        new EnemyData {
            attackPower = 20,
            moveSpeed = 300,
            distanceStaticAttack = 20,
            distanceMovingAttack = 40,
        }
    };
}
