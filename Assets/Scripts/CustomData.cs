using UnityEngine;

public class CustomData : MonoBehaviour
{

    public class Beads
{
    public Vector3 SpawnPosition;
    public Transform SpawnPositionTransform;
    public GameObject SpawnReference;
    public Beads(Vector3 position, GameObject reference)
    {
        SpawnPosition = position;
        SpawnReference = reference;
    }
}
    public class PickUpObject
    {
        public PickUpType Type { get; internal set; }
        public GameObject Object { get; internal set; }
    }
    public enum PickUpType
    {
        none,
        beads
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