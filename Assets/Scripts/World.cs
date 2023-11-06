using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Material material;
    public BlockType[] blockTypes;

    private void Start()
    {
        //Chunk newChunk = new Chunk(new ChunkCard(0,0), this);
        //Chunk newChunk2 = new Chunk(new ChunkCard(0,0), this);
        GenerateID();
    }

    void GenerateID()
    {
        for(int i=0;i<CoordinateData.worldSizeInChunks;i++)
        {
            for (int j = 0; j < CoordinateData.worldSizeInChunks; j++)
            {
                Chunk newChunk = new Chunk(new ChunkCard(i, j),this);
            }

        }
    }
}




[System.Serializable]
public class BlockType
{
    public string blockName;
    public bool isSolid;

    [Header("Texture Values")]
    public int backFaceTexture;
    public int frontFaceTexture;
    public int topFaceTexture;
    public int bottomFaceTexture;
    public int leftFaceTexture;
    public int rightFaceTexture;
    //[Header("TextureValues")]
    //public int backFaceTexture;
    //public int frontFaceTexture;
    //public int topFaceTexture;
    //public int bottomFaceTexture;
    //public int leftFaceTexture;
    //public int rightFaceTexture;


    // back, front, top, bottom, left, right

    public int getTextureID(int faceIndex)
    {
        switch(faceIndex)
        {
            case 0:
                return backFaceTexture;
            case 1:
                return frontFaceTexture;
            case 2:
                return topFaceTexture;
            case 3:
                return bottomFaceTexture;
            case 4:
                return leftFaceTexture;
            case 5:
                return rightFaceTexture;
            default:
                Debug.Log("Error in GetTextureID; invalid face index.");
                return 0;
        }
    }
}