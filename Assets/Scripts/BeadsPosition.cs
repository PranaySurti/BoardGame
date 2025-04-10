using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static CustomData;

public class BeadsPosition : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler, IBoardReset
{
    [SerializeField] bool IsDragging = false;
    public bool ISBeadPlaced { get; private set; } = false;
    public GameObject BeadsRef { get; private set; }

        public void SetBeadsRef(GameObject bead)
        {
            BeadsRef = bead;
            ISBeadPlaced = true;
        }

        public void ClearBead()
        {
            BeadsRef = null;
            ISBeadPlaced = false;
        }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Onpointer enter on beads position "+eventData.dragging);
        if ( !eventData.dragging) return;
        //if ( BeadsRef ) return;
        if (ISBeadPlaced) return;
        if ( !GameManager.Instance.PickupBeads ) return;
        
        IsDragging = eventData.dragging;
        
        Debug.Log("reach after drag ");
        GameManager.Instance.PickupBeads.GetComponent<PickUpBeads>().UpdateDrag(false);
        GameManager.Instance.PickupBeads.transform.position = transform.position;
        GameManager.Instance.PickupBeads.transform.SetParent(transform);
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if ( BeadsRef != null)
        {
            BeadsRef.transform.SetParent(null);
            SetBeadsRef(GameManager.Instance.GetPickUpObject);
            BeadsRef = null;
            BoardRestart();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Onpointer exit on beads position ");
               if (IsDragging)
        {
            IsDragging = false;
            GameManager.Instance.GetPickUpObject.GetComponent<PickUpBeads>().UpdateDrag(true);

        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsDragging)
        {
            IsDragging = false;
        }
        GameManager.Instance.GetPickUpObject.transform.position = transform.position;
        BeadsRef = GameManager.Instance.GetPickUpObject;
    }

        public void BoardRestart()
    {
        BeadsRef = null;
    }

    internal void UpdateDrag(bool v)
    {
        throw new NotImplementedException();
    }

    internal void MarkAsPlaced()
    {
        ISBeadPlaced = true; // Set the placement status
        Debug.Log("Marked " + gameObject.name + " as placed");
    }
}

internal interface IBoardReset
{

}