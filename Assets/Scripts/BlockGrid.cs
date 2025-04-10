using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockGrid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{

    [SerializeField] bool isDragging = false;
    [SerializeField] public GameObject ChildRef;

    //List<Player> playerList = new List<Player>();
    [SerializeField] public List<GameObject> BlockCell = new List<GameObject>();

    [SerializeField] int Placementindex = 0;
    [SerializeField] bool IsPlaced = false;

    // ON POINTER ENTER 
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("I pointer Enter");
        if ( !eventData.dragging) return;
        if ( GameManager.Instance.GetPickUpObject == null) return;
        isDragging = eventData.dragging;

        for( int i = 0; i < BlockCell.Count; i ++ ){
            if ( BlockCell[i].GetComponent<GridCell>().childRef != null )
            {
                if ( BlockCell[i].GetComponent<GridCell>().childRef.gameObject.GetInstanceID() == GameManager.Instance.GetPickUpObject.gameObject.GetInstanceID() ) break;
                continue;
            } 
            else
            {
                Debug.Log("updating position Index : " + i );
                GameManager.Instance.GetPickUpObject.GetComponent<PickUpBeads>().UpdateDrag(false);
                GameManager.Instance.GetPickUpObject.transform.position = BlockCell[i].transform.position;
                BlockCell[i].GetComponent<GridCell>().childRef = GameManager.Instance.GetPickUpObject;
                Placementindex = i;
                IsPlaced = true;
                break;
            }
        }
    }

    // on POINTER DOWN
    public void OnPointerDown(PointerEventData eventData)
    {
        // if( ChildRef != null ){
        //     ChildRef.transform.SetParent(null);
        //     BoardReset();
        // }
    }

    // ON POINTER EXIT 
    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragging){
            isDragging = false;
            if ( !IsPlaced ) {
                BlockCell[Placementindex].GetComponent<GridCell>().childRef = null;
            }else{
                int MaxLength =  BlockCell[Placementindex].GetComponent<GridCell>().childRef.GetComponent<PickUpBeads>().Number; 
                for(int i = Placementindex + 1; i <= MaxLength ; i ++ ){
                    BlockCell[i].GetComponent<GridCell>().childRef = BlockCell[Placementindex].GetComponent<GridCell>().childRef;
                }
            }
            
        }

        
    }

    // ON POINTER UP -----> NOT TO DO FOR NOW 
    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void BoardReset(){
        // ChildRef = null;
    }
}
