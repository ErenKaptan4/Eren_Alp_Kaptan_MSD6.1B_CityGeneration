using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class TerrainTextureData
{
    public Texture2D terrainTexture;

    public Vector2 tileSize;

    public float minHeight;
    public float maxHeight;

}

[System.Serializable] public class TreeData
{
    public GameObject treeMesh;

    public float minHeight;
    public float maxHeight;

}


public class GenerateRandomHeight : MonoBehaviour
{
    private Terrain terrain;

    private TerrainData terrainData;


    [SerializeField] [Range(0f, 1f)] private float minRandomHeightRange = 0f;
    [SerializeField] [Range(0f, 1f)] private float maxRandomHeightRange = 0.1f;

    [SerializeField] bool flattenTerrain = true;

    [Header("Perlin Noise")][SerializeField] private bool perlinNoise = false;
    [SerializeField] private float perlinNoiseWidthScale = 0.01f;
    [SerializeField] private float perlinNoiseHeightScale = 0.01f;

    //TEXTURE
    [Header("Texture Data")][SerializeField] private List<TerrainTextureData> terrainTextureData;
    [SerializeField] private bool addTerrainTexture = false;
    [SerializeField] private float terrainTextureBlendOffset = 0.01f;

    //TREE
    [Header("Tree Data")][SerializeField] private List<TreeData> treeData;
    [SerializeField] private int maxTrees = 2000;
    [SerializeField] private int treeSpacing = 10;
    [SerializeField] private bool addTrees = false;
    [SerializeField] private int terrainLayerIndex;

    //WATER
    [Header("Water")][SerializeField] private GameObject water;
    [SerializeField] private float waterHeight = 0.3f;

    //CLOUD
    [Header("Cloud")][SerializeField] private GameObject cloud;

    //RAIN
    [Header("Rain")][SerializeField] private GameObject rain;

    //PLAYER
    [Header("Player")][SerializeField] private GameObject player;
    List<Vector3> locations = new List<Vector3>()
    { 
        new Vector3 (75,325,150),
        new Vector3 (100,450,700),
        new Vector3 (700,500,650),
        new Vector3 (700,500,100)

    };


    void Start()
    {

        if(terrainData == null)
        {
            terrain = this.GetComponent<Terrain>();
        }

        if(terrainData == null)
        {
            terrainData = Terrain.activeTerrain.terrainData;
        }

        GenerateHeight();
        addTerrainTextures();
        AddTrees();
        AddWater();
        AddCloud();
        AddRain();
        MovePlayer();

    }

    //function to use perlin noise or not
    private void GenerateHeight()
    {
        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for(int width = 0; width < terrainData.heightmapResolution; width++)
        {
            for(int height = 0; height < terrainData.heightmapResolution; height++)
            {
                
                if (perlinNoise)
                {
                    heightMap[width, height] = Mathf.PerlinNoise(width * perlinNoiseWidthScale, height * perlinNoiseHeightScale);

                }
                else
                {
                    heightMap[width, height] = Random.Range(minRandomHeightRange, maxRandomHeightRange); 
                }
                
            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    //flattens the terrain on destroy (when program stops)
    private void FlattenTerrain()
    {
        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int width = 0; width < terrainData.heightmapResolution; width++)
        {
            for (int height = 0; height < terrainData.heightmapResolution; height++)
            {
                heightMap[width, height] = 0;
            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    private void addTerrainTextures()
    {
        TerrainLayer[] terrainLayers = new TerrainLayer[terrainTextureData.Count];

        for (int i = 0; i < terrainTextureData.Count; i++)
        {
            if (addTerrainTexture)
            {
                terrainLayers[i] = new TerrainLayer();
                terrainLayers[i].diffuseTexture = terrainTextureData[i].terrainTexture;
                terrainLayers[i].tileSize = terrainTextureData[i].tileSize;
            }
            else
            {
                terrainLayers[i] = new TerrainLayer();
                terrainLayers[i].diffuseTexture = null;
            }

        }

        terrainData.terrainLayers = terrainLayers;

        // Get the height map data from the terrain
        float[,] heightMap = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);

        // Create a 3D array to store the alpha map data
        float[,,] alphaMapList = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

        // Iterate over each pixel in the alpha map
        for (int height = 0; height < terrainData.alphamapHeight; height++)
        {
            for (int width = 0; width < terrainData.alphamapWidth; width++)
            {
                // Create a 1D array to store the alpha values for each texture layer
                float[] alphaMap = new float[terrainData.alphamapLayers];

                // Check if the terrainTextureData list is not empty
                if (terrainTextureData != null && terrainTextureData.Count > 0)
                {
                    // Iterate over each texture in terrainTextureData
                    for (int textureIndex = 0; textureIndex < terrainTextureData.Count; textureIndex++)
                    {
                        // Calculate the height range where the texture should be applied
                        float heightBegin = terrainTextureData[textureIndex].minHeight - terrainTextureBlendOffset;
                        float heightEnd = terrainTextureData[textureIndex].maxHeight + terrainTextureBlendOffset;

                        // Check if the height of the current pixel is within the height range of the current texture
                        if (heightMap[width, height] >= heightBegin && heightMap[width, height] <= heightEnd)
                        {
                            // Set the corresponding alpha value to 1
                            alphaMap[textureIndex] = 1;
                        }
                    }

                    Blend(alphaMap);
                }

                // Check if the alphaMap array is not null
                if (alphaMap != null)
                {
                    // Copy the alpha values from the alphaMap array to the alphaMapList array
                    for (int layerIndex = 0; layerIndex < terrainData.alphamapLayers; layerIndex++)
                    {
                        alphaMapList[width, height, layerIndex] = alphaMap[layerIndex];
                    }
                }
            }
        }

        terrainData.SetAlphamaps(0,0 , alphaMapList);

    }


    private void Blend(float[] alphamap)
    {
        float total = 0;

        for(int i = 0; i < alphamap.Length; i++)
        {
            total += alphamap[i];
        }

        for (int i = 0; i < alphamap.Length; i++)
        {
            alphamap[i] = alphamap[i] / total;
        }

    }


    private void AddTrees()
    {
        TreePrototype[] trees = new TreePrototype[treeData.Count];

        for (int i = 0; i < treeData.Count; i++)
        {
            trees[i] = new TreePrototype();
            trees[i].prefab = treeData[i].treeMesh;

        }

        terrainData.treePrototypes = trees;

        List<TreeInstance> treeInstanceList = new List<TreeInstance>();

        if (addTrees)
        {
            for (int z = 0; z < terrainData.size.z; z+= treeSpacing)
            {
                for(int x = 0;  x < terrainData.size.x; x+= treeSpacing)
                {
                    for(int treeIndex  = 0; treeIndex < trees.Length; treeIndex++)
                    {

                        if(treeInstanceList.Count < maxTrees)
                        {

                            float currentHeight = terrainData.GetHeight(x, z) / terrainData.size.y;
                            
                            //check if current height is in the range
                            if(currentHeight >= treeData[treeIndex].minHeight && currentHeight <= treeData[treeIndex].maxHeight)
                            {

                                float randomX = (x + Random.Range(-5.0f,  5.0f)) / terrainData.size.x;
                                float randomZ = (z + Random.Range(-5.0f, 5.0f)) / terrainData.size.z;

                                Vector3 treePosition = new Vector3(randomX * terrainData.size.x, 
                                    currentHeight * terrainData.size.y, 
                                    randomZ * terrainData.size.z) + this.transform.position;

                                RaycastHit raycastHit;

                                int layerMask = 1 << terrainLayerIndex;

                                //raycast to position them correctly
                                if (Physics.Raycast(treePosition, -Vector3.up, out raycastHit, 100, layerMask) ||
                                    Physics.Raycast(treePosition, Vector3.up, out raycastHit, 100, layerMask))
                                {

                                    float treeDistance = (raycastHit.point.y - this.transform.position.y) / terrainData.size.y;

                                    TreeInstance treeInstance = new TreeInstance();

                                    treeInstance.position = new Vector3(randomX, treeDistance, randomZ);
                                    treeInstance.rotation = Random.Range(0, 360);
                                    treeInstance.prototypeIndex = treeIndex;
                                    treeInstance.color = Color.white;
                                    treeInstance.widthScale = 1;
                                    treeInstance.heightScale = 1;

                                    treeInstanceList.Add(treeInstance);

                                }


                            }

                        }

                    }

                }

            }
        }

        terrainData.treeInstances = treeInstanceList.ToArray();

    }


    private void AddWater()
    {
        GameObject waterGameObject = Instantiate(water, this.transform.position, this.transform.rotation);
        waterGameObject.name = "Water";
        waterGameObject.transform.position = this.transform.position + new Vector3(terrainData.size.x / 2,
            waterHeight * terrainData.size.y, terrainData.size.z / 2);
        waterGameObject.transform.localScale = new Vector3(terrainData.size.x /50 , 1, terrainData.size.z / 50);

    }

    public void AddCloud()
    {
        Instantiate(cloud, new Vector3(600, 600, 400), Quaternion.identity);
    }

    public void AddRain()
    {
        Instantiate(rain, new Vector3(600, 600, 400), Quaternion.identity);
    }


    public void MovePlayer()
    {
        var randomSpawn = Random.Range(1, locations.Count);
        locations.Shuffle();

        player.transform.position = locations[randomSpawn];
    }



    private void OnDestroy()
    {
        FlattenTerrain();
    }

}
