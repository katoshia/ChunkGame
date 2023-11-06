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
    byte[,,] coordMap = new byte[CoordinateData.chunkWidth, CoordinateData.chunkHeight, CoordinateData.chunkWidth];
    World world;
    
    // Start is called before the first frame update
    void Start()
    {
        world = GameObject.Find("World").GetComponent<World>();
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
                    coordMap[x, y, z] = 0;
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
        return world.blockTypes[coordMap[x,y,z]].isSolid;
    }
    void AddCoorDataToChunk(Vector3 position)
    {
        for (int j = 0; j < 6; j++)
        {
            if(!CheckCoord(position+CoordinateData.touchCheck[j]))
            {

                byte blockID = coordMap[(int)position.x, (int)position.y, (int)position.z];
                // add our vertices
                vertices.Add(position + CoordinateData.coordVerts[CoordinateData.coordTris[j, 0]]);
                vertices.Add(position + CoordinateData.coordVerts[CoordinateData.coordTris[j, 1]]);
                vertices.Add(position + CoordinateData.coordVerts[CoordinateData.coordTris[j, 2]]);
                vertices.Add(position + CoordinateData.coordVerts[CoordinateData.coordTris[j, 3]]);

                AddTexture(world.blockTypes[blockID].getTextureID(j));
                // add the triangles - 2 per face.
                // 0,1,2,2,1,3
                triangles.Add(vertexIndex);// 0
                triangles.Add(vertexIndex+1);//1
                triangles.Add(vertexIndex+2);//2
                triangles.Add(vertexIndex+2);//2
                triangles.Add(vertexIndex+1);//1
                triangles.Add(vertexIndex+3);//3
                vertexIndex += 4;
            }

        }
    }
    void AddTexture(int textureID)
    {
        float y = textureID / CoordinateData.TextureAtlasSizeBlocks;
        float x = textureID - (y * CoordinateData.TextureAtlasSizeBlocks);

        x *= CoordinateData.NormalizedBlockTextureSize;
        y *= CoordinateData.NormalizedBlockTextureSize;

        y = 1f - y - CoordinateData.NormalizedBlockTextureSize;

        uvs.Add(new Vector2(x, y));
        uvs.Add(new Vector2(x, y+CoordinateData.NormalizedBlockTextureSize));
        uvs.Add(new Vector2(x+CoordinateData.NormalizedBlockTextureSize, y));
        uvs.Add(new Vector2(x+CoordinateData.NormalizedBlockTextureSize, y+CoordinateData.NormalizedBlockTextureSize));
    }

}
