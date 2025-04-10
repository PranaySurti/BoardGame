using TMPro;
using System.Collections;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PickUpBeads : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private bool isDragging = false;
    private float fixedY = 0.034f; // Match the Y-position of BlockBits
    private Vector3 DefaultPosi;
    [SerializeField] private Camera mainCamera;
    [SerializeField] Transform DefaultParent;
    private Collider objectCollider;
    private Vector3 offset;
    [SerializeField] private Material MatReference, GlowMaterial;
    private Rigidbody objectRigidbody;
    public int Number;

    public bool IsSorted { get; internal set; }
    public GameObject PickupBeads { get; set; }

    private void Awake()
    {
        mainCamera = Camera.main;
        DefaultPosi = transform.position;
        DefaultParent = transform.parent;
        objectCollider = GetComponent<Collider>();
        objectRigidbody = GetComponent<Rigidbody>(); // Add this line
    }

    public void UpdateDrag(bool IsDrag)
    {
        isDragging = IsDrag;
    }

    private void Start()
    {
        DefaultPosi = transform.position;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down on: " + gameObject.name + " | EventData: " + eventData.ToString());
        if (!GameManager.Instance.SetPickUp(gameObject)) return;
        isDragging = true;
        if (objectCollider != null) objectCollider.enabled = false;
        Vector2 pointerScreenPos = Pointer.current.position.ReadValue();
        Vector3 newWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(pointerScreenPos.x, pointerScreenPos.y, fixedY));
        transform.position = newWorldPosition;
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

    public void OnPointerUp(PointerEventData eventData)
    {
        if (BeadsManager.Instance != null)
        {
            BeadsManager.Instance.OnPointerUp(eventData);
        }
        else
        {
            Debug.LogWarning("No valid placement handler found for " + (gameObject != null ? gameObject.name : "null"));
        }

        isDragging = false;
        if (objectCollider != null) objectCollider.enabled = true;
        if (objectRigidbody != null) objectRigidbody.isKinematic = false;
        GameManager.Instance.DropPickup();
        StartCoroutine(SetKinematic());
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