using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject getPickUpObject;
    public GameObject GetPickUpObject => getPickUpObject;

    public static GameManager Instance { get; private set; }
    // public GameObject PickupBeads;

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
            return true;
        }
        return false;
    }

    public void DropPickup()
    {
        if (getPickUpObject != null)
        {
            getPickUpObject = null;
        }
    }
}