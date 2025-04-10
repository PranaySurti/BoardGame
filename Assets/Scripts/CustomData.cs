using UnityEngine;
using System.Collections;

public class CustomData : MonoBehaviour{

    public class rectangles
    {
        public Vector3 SpawnPosition;
        public Transform SpawnPositionTransform;
        public GameObject SpawnReference;
        public rectangles(Vector3 position, GameObject reference)
        {
            SpawnPosition = position;
            SpawnReference = reference;
        }
    }

    public class PickUpObject
    {
        public GameObject Object { get; internal set; }
    }


}

    public static class RandomPositionGenerator
    {
        public static Vector3 GetRandomPosition(Vector3 origin, float radius)
        {
            // Generate a random point inside a sphere of given radius
            Vector3 randomOffset = UnityEngine.Random.insideUnitSphere * radius;

            // Keep the Y value the same (if you want to maintain ground level)
            randomOffset.y = 0; 

            return origin + randomOffset;
        }
    }