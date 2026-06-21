using UnityEngine;



public class TreeSpawnManager : MonoBehaviour
{
    public GameObject treePrefab;
    public Transform imageTargetTransform;

    private int treeCount = 0;

    public void PlantOneTree()
    {
        treeCount++;

        Vector3 offset = new Vector3(treeCount * 0.9f, 0, 0);

        GameObject newTree = Instantiate(
            treePrefab,
            imageTargetTransform.position + offset,
            imageTargetTransform.rotation,
            imageTargetTransform
        );

        newTree.transform.localPosition = offset;
        newTree.transform.localRotation = Quaternion.identity;
        newTree.transform.localScale = Vector3.one * 0.5f;
    }
}