using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    public string szenenName;

    public void LoadScene()
    {
        SceneManager.LoadScene(szenenName);
    }
}
