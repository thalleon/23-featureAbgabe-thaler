using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public int slotNR;

    public DragDrop DragDrop;

    public GameObject greenFlash;
    public GameObject redFlash;

    public dragDropManager dragDropScore;

    public void OnDrop(PointerEventData eventData)
    {
        DragDrop draggedItem = eventData.pointerDrag.GetComponent<DragDrop>();

        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }

        if (slotNR == draggedItem.itemNr)
        {
            Debug.Log("Richtig");
            GreenFalshActive();
            dragDropScore.dragDropScore++;
        }
        else
        {
            Debug.Log("Falsch");
            RedFalshActive();
        }
    }

    public void GreenFalshActive()
    {
        greenFlash.SetActive(true);
        Invoke("GreenFalshDeactive", .3f);
    }

    public void GreenFalshDeactive()
    {
        greenFlash.SetActive(false);
    }

    public void RedFalshActive()
    {
        redFlash.SetActive(true);
        Invoke("RedFalshDeactive", .3f);
    }

    public void RedFalshDeactive()
    {
        redFlash.SetActive(false);
    }

}
