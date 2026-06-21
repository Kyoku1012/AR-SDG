// using UnityEngine;



// public class TreeSpawnManager : MonoBehaviour
// {
//     public GameObject treePrefab;
//     public Transform imageTargetTransform;

//     private int treeCount = 0;

//     public void PlantOneTree()
//     {
//         treeCount++;

//         Vector3 offset = new Vector3(treeCount * 0.75f, 0, 0);

//         GameObject newTree = Instantiate(
//             treePrefab,
//             imageTargetTransform.position + offset,
//             imageTargetTransform.rotation,
//             imageTargetTransform
//         );

//         newTree.transform.localPosition = offset;
//         newTree.transform.localRotation = Quaternion.identity;
//         newTree.transform.localScale = Vector3.one * 0.3f;
//     }
// }


// using System.Collections.Generic;
// using UnityEngine;

// public class TreeSpawnManager : MonoBehaviour
// {
//     public GameObject treePrefab;
//     public Transform imageTargetTransform;

//     public float minDistance = 0.75f;
//     public Vector2 spawnRange = new Vector2(2.1f, 2.2f); 
//     public int maxAttempts = 100;

//     private List<Vector3> plantedPositions = new List<Vector3>();

//     public void PlantOneTree()
//     {
//         Vector3 offset = GetRandomValidPosition();

//         if (offset == Vector3.negativeInfinity)
//         {
//             Debug.LogWarning("Could not find a valid position to plant a new tree after " + maxAttempts + " attempts.");
//             return;
//         }

//         GameObject newTree = Instantiate(
//             treePrefab,
//             imageTargetTransform
//         );

//         newTree.transform.localPosition = offset;
//         newTree.transform.localRotation = Quaternion.identity;
//         newTree.transform.localScale = Vector3.one * 0.3f;

//         plantedPositions.Add(offset);
//     }

//     Vector3 GetRandomValidPosition()
//     {
//         for (int i = 0; i < maxAttempts; i++)
//         {
//             float x = Random.Range(0f, spawnRange.x);
//             float z = Random.Range(0f, spawnRange.y);

//             Vector3 randomPos = new Vector3(x, 0f, z);

//             bool valid = true;

//             foreach (Vector3 plantedPos in plantedPositions)
//             {
//                 if (Vector3.Distance(randomPos, plantedPos) < minDistance)
//                 {
//                     valid = false;
//                     break;
//                 }
//             }

//             if (valid)
//             {
//                 return randomPos;
//             }
//         }

//         return Vector3.negativeInfinity;
//     }
// }

using System.Collections.Generic;
using UnityEngine;

public class TreeSpawnManager : MonoBehaviour
{
    public GameObject treePrefab;
    public Transform imageTargetTransform;

    public float minDistance = 0.75f;
    public float xRange = 2.1f;
    public float zRange = 2.2f;
    public int maxAttempts = 100;

    private List<Vector3> plantedPositions = new List<Vector3>();

    public void PlantOneTree()
    {
        Vector3 offset = GetRandomValidPosition();

        if (offset == Vector3.negativeInfinity)
        {
            Debug.LogWarning("Could not find a valid position to plant a new tree after " + maxAttempts + " attempts.");
            return;
        }

        GameObject newTree = Instantiate(treePrefab, imageTargetTransform);

        newTree.transform.localPosition = offset;
        newTree.transform.localRotation = Quaternion.identity;
        newTree.transform.localScale = Vector3.one * 0.3f;

        plantedPositions.Add(offset);
    }

    Vector3 GetRandomValidPosition()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            float x = Random.Range(-xRange, xRange);
            float z = Random.Range(-zRange, zRange);

            Vector3 randomPos = new Vector3(x, 0f, z);

            bool valid = true;

            foreach (Vector3 plantedPos in plantedPositions)
            {
                if (Vector3.Distance(randomPos, plantedPos) < minDistance)
                {
                    valid = false;
                    break;
                }
            }

            if (valid)
            {
                return randomPos;
            }
        }

        return Vector3.negativeInfinity;
    }
}