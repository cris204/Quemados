using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDataBase
{
    public static List<EnemyData> enemies = new List<EnemyData> {
        new EnemyData {
            attackPower = 5,
            moveSpeed = 12,
            distanceStaticAttack = 15,
            distanceMovingAttack = 30,
        },
        new EnemyData {
            attackPower = 10,
            moveSpeed = 10,
            distanceStaticAttack = 20,
            distanceMovingAttack = 40,
        },
        new EnemyData {
            attackPower = 20,
            moveSpeed = 5,
            distanceStaticAttack = 30,
            distanceMovingAttack = 50,
        }
    };
}
