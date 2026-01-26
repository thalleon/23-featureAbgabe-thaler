using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class homeScript : MonoBehaviour
{
    public string sceneErkennen;
    public string sceneErstellen;
    public string sceneSimon;

    public void erkennen ()
    {
        SceneManager.LoadScene(sceneErkennen);
    }

    public void erstellen ()
    {
        SceneManager.LoadScene(sceneErstellen);
    }

    public void simon ()
    {
        SceneManager.LoadScene(sceneSimon);
    }
}
