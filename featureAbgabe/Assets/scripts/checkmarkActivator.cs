using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkmarkActivator : MonoBehaviour
{
    public GameObject checkmark;

    public photoManager photoManager;

    public void ActivateCheckmark()
    {
        checkmark.SetActive(true);
        
    }

    public void AddNumber()
    {
        photoManager.score++;
    }

}
