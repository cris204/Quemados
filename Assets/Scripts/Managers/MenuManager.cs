using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public void LoadGame()
    {
        OwnSceneLoadManager.Instance.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
