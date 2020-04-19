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

public class EnemyHideOut : MonoBehaviour
{
    public enum HideOutOrientation
    {
        XAxis,
        ZAxis,
        BothAxis,
    }

    public int spacesToHide;
    public int spacesToHideOccupied;
    public HideOutOrientation orientation;
    private GameObject physicalGO;

    private void Awake()
    {
        this.physicalGO = this.gameObject;
    }

    public bool HasFreeSpaces()
    {
        return this.spacesToHideOccupied < this.spacesToHide;
    }

    public void OcuppyHideOut()
    {
        this.spacesToHideOccupied++;
    }

    public void SetFreeSpace()
    {
        this.spacesToHideOccupied--;
    }

}

public enum EnemyBehaviourState { 
    Agressive,
    Normal,
    Defenssive
}
