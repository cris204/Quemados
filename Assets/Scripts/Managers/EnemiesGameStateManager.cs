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

    public List<EnemyHideOut> hideOutList;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }
    }

    //Could receive a position to search the near hideout
    public EnemyHideOut GetHideOut()
    {
        for (int i = 0; i < this.hideOutList.Count; i++) {
            if (this.hideOutList[i].HasFreeSpaces()) {
                this.hideOutList[i].OcuppyHideOut();
                return this.hideOutList[i];
            }
        }
        return null;
    }

}
public enum HideOutOrientation
{
    XAxis,
    ZAxis,
    BothAxis,
}

public class EnemyHideOut : MonoBehaviour
{

    public int spacesToHide;
    public int spacesToHideOccupied;
    public HideOutOrientation orientation;
    private GameObject physicalGO;

    private void Awake()
    {
        this.physicalGO = this.gameObject;
    }

    public GameObject GetHideOutObject()
    {
        return this.physicalGO;
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
