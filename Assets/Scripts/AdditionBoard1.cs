// using UnityEngine;
// using static CustomData;

// public class AdditionBoard : MonoBehaviour
// {

//     public static AdditionBoard Instance;
//     // [SerializeField] Beads[] beadsArray;
//     [SerializeField] GameObject BeadsPrefab;
//     [SerializeField] GameObject[] BeadsInstantiateReference;

//     [SerializeField] private PickUpObject pickUpObject = new PickUpObject();
//     [SerializeField] public PickUpObject GetPickUpObject => pickUpObject;

//     public bool SetPickUp(PickUpType pickUpType, GameObject Obj)
//     {
//         if ( pickUpObject.Type == PickUpType.none )
//         {
//             pickUpObject.Object = Obj;
//             pickUpObject.Type = pickUpType;
//             return true;
//         }
//         return false;
//     }

//     public void DropPickup()
//     {
//         pickUpObject = new PickUpObject();
//         // Debug.Log($"pickUpObject null : {pickUpObject.Object == null } And Type : {pickUpObject.Type}");
//     }

//     void Awake()
//     {
//         if ( Instance == null )
//         {
//             Instance = this;
//             // DontDestroyOnLoad(this.gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     public class PickUpObject
//     {
//         public PickUpType Type { get; internal set; }
//         public GameObject Object { get; internal set; }
//     }

// }

// public class PickUpObject
// {
//     public GameObject Object;
//     public PickUpType Type;
//     public PickUpObject()
//     {
//         Object = null;
//         Type = PickUpType.none;
//     }
// }    
