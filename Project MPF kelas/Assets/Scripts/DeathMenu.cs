using UnityEngine;
using System.Collections;

public class DeathMenu : MonoBehaviour {

    public string mainMenuLevel;

    public void RestartGame()
    {
        FindObjectOfType<GameManager>().Reset();
    }

    public void QuitToMenu()
    {
        Application.LoadLevel(mainMenuLevel);
    }
}
