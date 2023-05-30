using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int tileIndexX { get; private set; }
    public int tileIndexZ { get; private set; }

    public bool isStepped = false;
    
    public void SetIndexTile(int tileIndexX, int tileIndexZ)
    {
        this.tileIndexX = tileIndexX;
        this.tileIndexZ = tileIndexZ;
    }
}
