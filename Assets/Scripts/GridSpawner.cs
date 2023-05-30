using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public int row { get; private set; } = 9;
    public int column { get; private set; } = 6;
    private float _gridSpace = 1.2f;
    [SerializeField] private Tile gridPrefab;
    public Vector3 gridOrigin = Vector3.zero;

    public Tile[,] gridList { get; private set; }

    [SerializeField] private int _amountColorTile;
    [SerializeField] private int _currentIndexTileX;
    [SerializeField] private int _currentIndexTileZ;

    private void Awake()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        gridList = new Tile[row, column];
        for (int x = 0; x < row; x++)
        {
            for (int y = 0; y < column; y++)
            {
                Vector3 spawnPosition = new Vector3(x * _gridSpace, y * _gridSpace, 0) + gridOrigin;
                Tile gridObjects = Instantiate(gridPrefab, spawnPosition, Quaternion.identity, transform);

                gridList[x, y] = gridObjects;

                gridObjects.gameObject.name = "Tile( " + ("X:" + x + " ,Y:" + y + " )");
                gridObjects.SetIndexTile(x, y);
            }
        }
    }
}
