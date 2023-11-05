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

    // bool for checking if face is touching another face.
    bool[,,] coordMap = new bool[CoordinateData.chunkWidth, CoordinateData.chunkHeight, CoordinateData.chunkWidth];
    // Start is called before the first frame update
    void Start()
    {
        PopulateCoordMap();
        CreateChunk();
        CreateMesh();
    }
    // populate the coordinate map for chunk
    void PopulateCoordMap()
    {
        for (int x = 0; x < CoordinateData.chunkWidth; x++)
        {
            for (int y = 0; y < CoordinateData.chunkHeight; y++)
            {
                for (int z = 0; z < CoordinateData.chunkWidth; z++)
                {
                    coordMap[x, y, z] = true;
                }

            }
        }
    }
    // create the chunk using the mesh
    void CreateChunk()
    {
        // draw the chunk for the max size set
        for (int x = 0; x < CoordinateData.chunkWidth; x++)
        {
            for (int y = 0; y < CoordinateData.chunkHeight; y++)
            {
                for (int z = 0; z < CoordinateData.chunkWidth; z++)
                {
                    AddCoorDataToChunk(new Vector3(x, y, z));
                }

            }
        }
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
    // check coordniates for faces - return the coordinates
    bool CheckCoord(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);
        int z = Mathf.FloorToInt(position.z);

        if (x < 0 || x > CoordinateData.chunkWidth - 1 || y < 0||y>CoordinateData.chunkHeight-1 ||z<0||z>CoordinateData.chunkWidth-1)
        {
            return false;
        }
        return coordMap[x,y,z];
    }
    void AddCoorDataToChunk(Vector3 position)
    {
        for (int j = 0; j < 6; j++)
        {
            if(!CheckCoord(position+CoordinateData.touchCheck[j]))
            {
                for (int i = 0; i < 6; i++)
                {
                    int triangleIndex = CoordinateData.coordTris[j, i];
                    vertices.Add(CoordinateData.coordVerts[triangleIndex] + position);

                    triangles.Add(vertexIndex);
                    uvs.Add(CoordinateData.coordUVS[i]);
                    vertexIndex++;
                }
            }

        }
    }

}
