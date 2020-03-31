using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton <T> : MonoBehaviour where T : MonoBehaviour
{

    private static T instance;
    public static T Instance { 
        get {
            if(instance == null) {
                CreateInstance();
            }
            return instance;
        }
    } 

    public static void Init()
    {
        CreateInstance();
    }

    private static void CreateInstance()
    {
        GameObject newObject = new GameObject();
        instance = newObject.AddComponent<T>();
        newObject.name = instance.ToString();
        DontDestroyOnLoad(newObject);
    }

    public static bool HasInstance()
    {
        return instance != null;
    }
}
