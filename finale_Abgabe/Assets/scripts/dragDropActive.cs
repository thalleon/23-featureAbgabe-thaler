using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragDropActive : MonoBehaviour
{

    public Canvas canvasDragDrop;

    public GameObject pfeil;

    public float triggerTime = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForDragDropActive(triggerTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator WaitForDragDropActive(float timeinseconds)
    {
        yield return new WaitForSeconds(triggerTime);

        canvasDragDrop.gameObject.SetActive(true);

        pfeil.SetActive(true);

        Debug.Log("Button Sind Aktiv");
    }

    
}
