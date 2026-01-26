using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Transform originalParent;

    public int itemNr;

    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        if (eventData.pointerEnter != null)
        {
            HotbarSlot slot = eventData.pointerEnter.GetComponent<HotbarSlot>();

            if (slot != null)
            {
                transform.SetParent(slot.transform, false);
                rectTransform.anchoredPosition = Vector2.zero;
                rectTransform.localScale = Vector3.one;
                return;
            }
            else
            {
                transform.SetParent(originalParent);
                rectTransform.anchoredPosition = Vector2.zero;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
