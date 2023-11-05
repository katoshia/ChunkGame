using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    // grab mesh renderer and filter
    public MeshRenderer meshRend;
    public MeshFilter meshFilter;

    // Voxel planning parameters - coordinates to pull from CoordinateData class

    int vertexIndex = 0;
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0;x<CoordinateData.chunkWidth;x++)
        {
            for (int y = 0; y < CoordinateData.chunkHeight; y++)
            {
                for (int z = 0; z < CoordinateData.chunkWidth; z++)
                {
                    AddCoorDataToChunk(new Vector3(x, y, z));
                }

            }
        }

        // will move the voxel/block
        //AddCoorDataToChunk(transform.position+transform.right);
        CreateMesh();

    }
    void CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();

        mesh.RecalculateNormals();
        meshFilter.mesh = mesh;
    }
    void AddCoorDataToChunk(Vector3 position)
    {
        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                int triangleIndex = CoordinateData.coordTris[j, i];
                vertices.Add(CoordinateData.coordVerts[triangleIndex]+position);

                triangles.Add(vertexIndex);
                uvs.Add(CoordinateData.coordUVS[i]);
                vertexIndex++;
            }
        }
    }

}
