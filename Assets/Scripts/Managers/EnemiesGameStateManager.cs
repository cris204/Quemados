using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGameStateManager : MonoBehaviour
{
    private static EnemiesGameStateManager instance;
    public static EnemiesGameStateManager Instance {
        get {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }
    }


}

public enum EnemyBehaviourState { 
    Agressive,
    Normal,
    Defenssive
}
