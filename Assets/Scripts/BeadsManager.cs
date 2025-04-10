using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using static CustomData;
using System.Collections;

    public class BeadsManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
    {
        public static BeadsManager Instance;

        [SerializeField] private GameObject[] BeadsArray;
        [SerializeField] Transform DropPosition;
        private Vector3[] beadsArrayPosition; 
        private float dropRadius = 2f;

        void Awake()
        {
            // if (Instance == null)
            // {
            //     Instance = this;
            //     beadsArrayPosition = new Vector3[BeadsArray.Length];

            //     for (int i = 0; i < BeadsArray.Length; i++)
            //     {
            //         beadsArrayPosition[i] = BeadsArray[i].transform.position;
            //     }
            // }
            // else
            // {
            //     Debug.Log("Destroying gameobject");
            //     Destroy(gameObject);
            // }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Debug.Log("Pointer Enter");

            if (!eventData.dragging || BeadsArray == null) return;

            var pickup = GameManager.Instance.GetPickUpObject.GetComponent<PickUpBeads>();
            if (pickup.IsSorted) return;

            // for (int i = 0; i < BeadsArray.Length; i++)
            // {
            //     var beadPos = BeadsArray[i].GetComponent<BeadsPosition>();
            //     if (!beadPos.ISBeadPlaced)
            //     {
            //         var pickupObj = GameManager.Instance.GetPickUpObject;
            //         pickupObj.transform.position = BeadsArray[i].transform.position;
            //         pickupObj.transform.SetParent(BeadsArray[i].transform);
            //         pickup.UpdateDrag(false);

            //         int num = pickup.Number;
            //         // for (int j = i; j < num; j++)
            //         // {
            //         //     beadPos.SetBeadsRef(pickupObj);
            //         // }
            //         break;
            //     }
            // }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Debug.Log("Pointer Exit");

            if (!eventData.dragging) return;

            GameManager.Instance.GetPickUpObject.GetComponent<PickUpBeads>().UpdateDrag(true);
        }


        //GROK SUGGESTION 2 CODE:
        public void OnPointerUp(PointerEventData eventData)
        {
            // Debug.Log("Pointer Up");

            if (!eventData.dragging || BeadsArray == null || GameManager.Instance.GetPickUpObject == null) return;

            var pickupObj = GameManager.Instance.GetPickUpObject;
            int num = pickupObj.GetComponent<PickUpBeads>().Number;
            bool placed = false;

            // for (int index = 0; index <= BeadsArray.Length - num; index++)
            // {
            //     var beadPos = BeadsArray[index].GetComponent<BeadsPosition>();
            //     if (!beadPos.ISBeadPlaced)
            //     {
            //         bool canPlace = true;

            //         // Check if all positions in the range are free
            //         for (int j = index; j < index + num && j < BeadsArray.Length; j++)
            //         {
            //             if (BeadsArray[j].GetComponent<BeadsPosition>().ISBeadPlaced)
            //             {
            //                 canPlace = false;
            //                 Debug.Log("Position Already Filled at index " + j);
            //                 break;
            //             }
            //         }

            //         if (canPlace)
            //         {
            //             // Calculate the base position and offset based on the first bead's position
            //             Vector3 basePosition = BeadsArray[index].transform.position;
            //             float cellSize = Vector3.Distance(BeadsArray[1].transform.position, BeadsArray[0].transform.position); // Assume uniform grid spacing
            //             Vector3 offsetDirection = new Vector3(cellSize, 0f, 0f); // Offset along X-axis (adjust if grid is Z-based)

            //             // Place beads with proper world space alignment
            //             for (int j = index; j < index + num && j < BeadsArray.Length; j++)
            //             {
            //                 Vector3 targetPosition = basePosition + ((j - index) * offsetDirection);
            //                 GameObject newBead = Instantiate(pickupObj, targetPosition, Quaternion.identity);
            //                 newBead.transform.SetParent(null); // Avoid parenting to maintain world position
            //                 newBead.GetComponent<PickUpBeads>().UpdateDrag(false);

            //                 BeadsArray[j].GetComponent<BeadsPosition>().SetBeadsRef(newBead);
            //                 Debug.Log("Assigned " + newBead.name + " to BlockBit " + (j + 1) + " at " + targetPosition);
            //             }
            //             placed = true;
            //             //Destroy(pickupObj); // Clean up the original pickup object
            //             break;
            //         }
            //     }
            // }

            if (!placed)
            {
                // Debug.Log("No valid space found for " + pickupObj.name);
                pickupObj.GetComponent<PickUpBeads>().UpdateDrag(true);
            }
        }



        public void ResetSkittle(GameObject obj)
        {
            obj.transform.position = RandomPositionGenerator.GetRandomPosition(DropPosition.position, dropRadius);
            obj.transform.rotation = DropPosition.rotation;
            obj.transform.SetParent(transform);
        }

    }
