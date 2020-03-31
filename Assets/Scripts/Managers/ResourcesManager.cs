using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : Singleton<ResourcesManager>
{
    private string basePath = "Resource";
    public GameObject GetPowerBehaviourPrefab(PowerType name)
    {
        return Resources.Load("Prefabs/PowersEffect" + name.ToString()) as GameObject;
    }
}
