using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// read only class for holding all data for use in other classes.
public static class CoordinateData
{
    public static readonly int chunkWidth = 5;
    public static readonly int chunkHeight = 1;

    public static readonly int TextureAtlasSizeBlocks = 4;
    public static float NormalizedBlockTextureSize 
    {
        get { return 1f / (float)TextureAtlasSizeBlocks; }
    }
    // coordinates of the block/voxel storage
    public static readonly Vector3[] coordVerts = new Vector3[8]
    {
       new Vector3(0f,0f,0f),
       new Vector3(1.0f,0f,0f),
       new Vector3(1.0f,1.0f,0f),
       new Vector3(0f,1.0f,0f),
       new Vector3(0f,0f,1.0f),
       new Vector3(1.0f,0f,1.0f),
       new Vector3(1.0f,1.0f,1.0f),
       new Vector3(0f,1.0f,1.0f)
   };
    // check for faces touching
    public static readonly Vector3[] touchCheck = new Vector3[6]
    {
       new Vector3(0f,0f,-1f), // check backface
       new Vector3(0f,0f,1f), // check front face
       new Vector3(0f,1f,0f), // check top
       new Vector3(0f,-1f,0f),// check bottom
       new Vector3(-1f,0f,0f),// check left
       new Vector3(1f,0f,0f) // check right
    };
    // coordinates of the two triables that make up each face of the block(2 triables per face)
    public static readonly int[,] coordTris = new int[6,4]
    {

        // 0,1,2,2,1,3 - used for tracking the triablnes at runtime.
        {0,3,1,2 }, // back face
        {5,6,4,7 }, // front face
        {3,7,2,6 }, // top face
        {1,5,0,4 },// bottom face
        {4,7,0,3 },// left face
        {1,2,5,6 }//right face
    };
    // storage for the material coordinates to the faces.
    public static readonly Vector2[] coordUVS = new Vector2[4]
    {
        new Vector2(0.0f,0.0f),
        new Vector2(0.0f,1.0f),
        new Vector2(1.0f,0.0f),
        new Vector2(1.0f,1.0f)
    };

}
