using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OwnSceneLoadManager : MonoBehaviour
{
    private static OwnSceneLoadManager instance;
    public static OwnSceneLoadManager Instance {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }


}
