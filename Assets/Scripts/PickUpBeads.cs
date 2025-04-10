using TMPro;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PickUpBeads : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Camera mainCamera;
    private bool isDragging = false;
    private Vector3 offset;
    public int Number;

    private float fixedY; 

    private MeshRenderer meshRenderer;
    private Collider objectCollider;
    private Rigidbody objectRigidbody;
    
    [SerializeField] Transform DefaultParent;
    [SerializeField] private Material MatReference, GlowMaterial;
    public bool IsSorted { get; internal set; }
    public GameObject PickupBeads { get; set; }

    Vector3 BasePosition;
    Quaternion BaseRotation;


    private void Awake()
    {
        mainCamera = Camera.main;
        objectCollider = GetComponent<Collider>();
        objectRigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(SetKinematic()); 
        BasePosition = transform.position;
        BaseRotation = transform.rotation;
    }

    public void UpdateDrag(bool IsDrag)
    {
        isDragging = IsDrag;
    }

        public void OnPointerUp(PointerEventData eventData)
    {
        // if(transform.parent && GetComponentInParent<BlockGrid>()){
        //     GetComponentInParent<BlockGrid>().OnPointerUp(eventData);
        // }
        // else if( transform.parent && GetComponentInParent<BeadsManager>() ){
        //     GetComponentInParent<BeadsManager>().OnPointerUp(eventData);
        // }else{
        //     BeadsManager.Instance.ResetSkittle(gameObject);
        // }

        transform.position = BasePosition;
        transform.rotation = BaseRotation;

        

        isDragging = false;
        if (objectCollider != null) objectCollider.enabled = true;
        if (objectRigidbody != null) objectRigidbody.isKinematic = false;
        GameManager.Instance.DropPickup();
        StartCoroutine(SetKinematic());
    }

    /////////////////////////////////////////////////////////////////////////////
    public void OnPointerDown(PointerEventData eventData)
    {
        if ( !GameManager.Instance.SetPickUp(gameObject)) return; 
        isDragging = true;
        if (objectCollider != null) objectCollider.enabled = false;
        if (objectRigidbody != null) objectRigidbody.isKinematic = true;
        fixedY = 2.5f; //transform.position.y + 8f; 
        Vector2 pointerScreenPos = Pointer.current.position.ReadValue(); // Get pointer position
        Vector3 newWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(pointerScreenPos.x, pointerScreenPos.y, fixedY ));
        transform.position = newWorldPosition; // + (Vector3.up / 2);
        // meshRenderer.material = GlowMaterial; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 pointerScreenPos = Pointer.current.position.ReadValue();
            Vector3 newWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(pointerScreenPos.x, pointerScreenPos.y, fixedY));
            transform.position = newWorldPosition;
        }
    }

    public IEnumerator SetKinematic()
    {
        yield return new WaitForSeconds(2f);
        if (objectRigidbody != null)
        {
            yield return new WaitUntil(() => objectRigidbody.linearVelocity.magnitude < 0.15f);
            objectRigidbody.isKinematic = true;
        }
    }

    public void ResetData()
    {
        UpdateDrag(false);
    }
}