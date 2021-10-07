using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandCardHover : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header ("Card Properties")]
    public HandCard card;
    public CanvasGroup canvasGroup;

    [Header("Card Hover")]
    public bool canHover = false;

    [Header("Card Drag")]
    public bool canDrag = false;
    public GameObject EmptyCard;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!canHover) return;
        card.transform.localScale = new Vector2(0.8f, 0.8f);
        card.transform.localPosition = new Vector2(card.transform.localPosition.x, 190);
        int index = card.transform.GetSiblingIndex();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!canHover) return;

        // Return to normal
        card.transform.localScale = new Vector2(0.5f, 0.5f);
        card.transform.localPosition = new Vector2(card.transform.localPosition.x, 0);
        int index = card.handIndex;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
