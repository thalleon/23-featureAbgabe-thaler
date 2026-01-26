using UnityEngine;

public class ToggleObjects : MonoBehaviour
{
    [Header("Objekte, die sichtbar/unsichtbar geschaltet werden sollen")]
    public GameObject[] objectsToToggle;

    // Beim Start werden alle Objekte versteckt
    private void Start()
    {
        foreach (GameObject obj in objectsToToggle)
        {
            if (obj != null)
            {
                obj.SetActive(false);
            }
        }
    }

    // Wird vom Button aufgerufen
    public void ToggleVisibility()
    {
        foreach (GameObject obj in objectsToToggle)
        {
            if (obj != null)
            {
                bool isActive = obj.activeSelf;
                obj.SetActive(!isActive);
            }
        }
    }
}
