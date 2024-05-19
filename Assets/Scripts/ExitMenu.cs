using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitMenu : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Game Quitted");
        Application.Quit();
    }
}
