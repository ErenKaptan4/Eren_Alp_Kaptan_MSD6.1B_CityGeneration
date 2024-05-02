using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class cube3 : MonoBehaviour
{
    protected MeshFilter MeshFilter;
    protected Mesh Mesh;
    private Vector3 size = Vector3.one;
    private List<Material> cubeMaterialsList = new List<Material>();

    public List<int[]> submeshes = new List<int[]> { new int[] {
            //Front
            0,1,2,
            0,2,3,

            //Back
            4,5,6,
            4,6,7,

            //Right
            1,4,7,
            1,7,2,

            //Left
            5,0,3,
            5,3,6, } ,

            new int[]{
            //Top
            5,4,1,
            5,1,0,

            //Bottom
            3,2,7,
            3,7,6,}
    };

    // Start is called before the first frame update
    void Start()
    {

        CreateCube();

    }

    public void CreateCube()
    {
        cubeMaterialsList.Add(Resources.Load<Material>("Materials/HouseTexture"));

        cubeMaterialsList.Add(Resources.Load<Material>("Materials/Roof"));


        Mesh = new Mesh();
        Mesh.name = "NewMesh";

        Mesh.vertices = NewVertCube();
        Mesh.triangles = NewTrisCube();

        //Calcuate
        Mesh.RecalculateNormals();
        Mesh.RecalculateBounds();
        Mesh.RecalculateTangents();

        MeshFilter = gameObject.GetComponent<MeshFilter>();
        MeshFilter.mesh = Mesh;


        Mesh.uv = newUvCube();

        Mesh.subMeshCount = submeshes.Count;

        GetComponent<MeshRenderer>().materials = cubeMaterialsList.ToArray();

        for (int i = 0; i < submeshes.Count; i++)
        {

            if (submeshes[i].Length < 3)
            {
                Mesh.SetTriangles(new int[3] { 0, 0, 0 }, i);
            }
            else
            {
                Mesh.SetTriangles(submeshes[i].ToArray(), i);
            }
        }
    }

    private Vector2[] newUvCube()
    {
        return new Vector2[]
        { 
            //Front
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 0),

            //Back
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 0),

            //Top
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 0),

            //Bottom
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(0, 0),
        };
    }

    private Vector3[] NewVertCube()
    {
        return new Vector3[]
{
            new Vector3(-size.x, size.y, -size.z), //0  top left
            new Vector3(size.x, size.y, -size.z), //1 top right
            new Vector3(size.x, -size.y, -size.z), //2 bottom right
            new Vector3(-size.x, -size.y, -size.z), //3 bottom left

            
            new Vector3(size.x, size.y, size.z), //4 top left
            new Vector3(-size.x, size.y, size.z), //5 top right
            new Vector3(-size.x, -size.y, size.z), //6 bottom right
            new Vector3(size.x, -size.y, size.z), //7 bottom left

            
            new Vector3(-size.x, size.y, size.z), //8 top left
            new Vector3(size.x, size.y, size.z), //9 top right
            new Vector3(size.x, size.y, -size.z), //10 bottom right
            new Vector3(-size.x, size.y, -size.z), //11 bottom left

            
            new Vector3(-size.x, -size.y, size.z), //12 top left
            new Vector3(size.x, -size.y, size.z), //13 top right
            new Vector3(size.x, -size.y, -size.z), //14 bottom right
            new Vector3(-size.x, -size.y, -size.z), //15 bottom left
};
    }

    private int[] NewTrisCube()
    {
        return new int[]
{
            //Front
            0,1,2,
            0,2,3,

            //Back
            4,5,6,
            4,6,7,

            //Right
            1,4,7,
            1,7,2,

            //Left
            5,0,3,
            5,3,6,

            //Top
            5,4,1,
            5,1,0,

            //Bottom
            3,2,7,
            3,7,6,
};
    }
}