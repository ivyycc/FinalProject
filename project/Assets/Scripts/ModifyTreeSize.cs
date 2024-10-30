using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyTreeSize : MonoBehaviour
{
  
    void Start()
    {
        // Get the terrain component
        Terrain terrain = GetComponent<Terrain>();

        // Get the terrain data
        TerrainData terrainData = terrain.terrainData;

        // Find the tree prototype you want to modify
        TreePrototype[] treePrototypes = terrainData.treePrototypes;

        if (treePrototypes.Length > 0)
        {
            // Modify the tree's prefab scale
            foreach (TreePrototype treePrototype in treePrototypes)
            {
                // Set the desired scale
                treePrototype.prefab.transform.localScale = new Vector3(10.0f, 10.0f, 10.0f); // Example scale
                Debug.Log("tree size changed:" + treePrototype.prefab.transform.localScale.y);
            }

            // Apply changes to the terrain
            terrainData.treePrototypes = treePrototypes;
        }
    }
}


