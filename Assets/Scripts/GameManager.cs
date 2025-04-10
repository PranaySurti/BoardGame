using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject getPickUpObject;
    public GameObject GetPickUpObject => getPickUpObject;

    public static GameManager Instance { get; private set; }
    public GameObject PickupBeads;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool SetPickUp(GameObject _picked)
    {
        if (getPickUpObject == null)
        {
            getPickUpObject = _picked;
            Debug.Log("Picked up: " + _picked.name + " | getPickUpObject: " + getPickUpObject);
            return true;
        }
        Debug.Log("Failed to pick up: getPickUpObject already set to " + getPickUpObject);
        return false;
    }

    public void DropPickup()
    {
        if (getPickUpObject != null)
        {
            Debug.Log("Dropping pickup: " + getPickUpObject.name);
            getPickUpObject = null;
        }
    }
}