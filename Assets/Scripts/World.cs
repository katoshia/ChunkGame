using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public Material material;
    public BlockType[] blockTypes;

    Chunk[,] chunks = new Chunk[CoordinateData.worldSizeInChunks, CoordinateData.worldSizeInChunks];

    private void Start()
    {
        GenerateID();
    }

    void GenerateID()
    {
        for(int i=0;i<CoordinateData.worldSizeInChunks;i++)
        {
            for (int j = 0; j < CoordinateData.worldSizeInChunks; j++)
            {
                CreateNewChunk(i, j);
            }

        }
    }
    public byte GetBlock (Vector3 pos)
    {
        if (!IsBlockInWorld(pos))
            return 0;
        if (pos.y < 1)
            return 1;
        else if (pos.y == CoordinateData.chunkHeight - 1)
            return 3;
        else
            return 2;
    }
    void CreateNewChunk(int x, int z)
    {
        chunks[x, z] = new Chunk(new ChunkCard(x, z), this);
    }
    bool isChunkInWorld (ChunkCard card)
    {
        if (card.x > 0 && card.x < CoordinateData.worldSizeInChunks - 1 && card.z > 0 && card.z < CoordinateData.worldSizeInChunks - 1)
            return true;
        else
            return false;
    }
    bool IsBlockInWorld(Vector3 pos)
    {
        if (pos.x >= 0 && pos.x < CoordinateData.worldSizeinBlocks && pos.y >= 0 && pos.y < CoordinateData.chunkHeight && pos.z >= 0 && pos.z < CoordinateData.worldSizeinBlocks)
            return true;
        else
            return false;
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