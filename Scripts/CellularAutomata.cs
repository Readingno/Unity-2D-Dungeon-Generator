using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class CellularAutomata: MonoBehaviour
{
    public int mapSize;
    public int[,] map;

    public string seed;
    public bool useRandomSeed;

    public int randomFillPercent;

    public Tile groundTile;
    public Tile wallTile;
    public Tilemap groundMap;
    public Tilemap wallMap;

    public bool finished;

    // Start is called before the first frame update
    void Start()
    {
        finished = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMap()
    {
        InitializeMap();
        StartCoroutine(SmoothMap());
    }

    void InitializeMap()
    {
        map = new int[mapSize, mapSize];
        if (useRandomSeed)
        {
            seed = System.DateTime.Now.ToString();
        }
        System.Random rd = new System.Random(seed.GetHashCode());
        for (int x = 0; x < mapSize; x++) 
        {
            for (int y = 0; y < mapSize; y++)
            {
                if (x == 0 || x == mapSize - 1 || y == 0 || y == mapSize - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (rd.Next(0, 100) < randomFillPercent) ? 1 : 0;
                }
            }
        }
        DrawMap();
    }

    IEnumerator SmoothMap()
    {
        finished = false;
        for (int i = 0; i < 7; i++)
        {
            yield return new WaitForSeconds(0.5f);
            int[,] nMap = new int[mapSize, mapSize];
            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    int surroundingWallCount = GetSurroundingWallCount(x, y);
                    if (surroundingWallCount > 4)
                    {
                        nMap[x, y] = 1;
                    }
                    else if (surroundingWallCount < 4)
                    {
                        nMap[x, y] = 0;
                    }
                }
            }
            for (int x = 0; x < mapSize; x++)
            {
                for (int y = 0; y < mapSize; y++)
                {
                    map[x, y] = nMap[x, y];
                }
            }
            DrawMap();
        }
        finished = true;
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int x = gridX - 1; x <= gridX + 1; x++)
        {
            for (int y = gridY - 1; y <= gridY + 1; y++)
            {
                if (x >= 0 && x < mapSize && y >= 0 && y < mapSize)
                {
                    if (x != gridX || y != gridY)
                    {
                        wallCount += map[x, y];
                    }
                }
                else
                {
                    wallCount ++;
                }
            }
        }
        return wallCount;
    }

    void DrawMap()
    {
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                Vector3Int pos = new Vector3Int(x - mapSize / 2, y - mapSize / 2, 0);
                if (map[x, y] == 1)
                {
                    wallMap.SetTile(pos, wallTile);
                }
                else
                {
                    wallMap.SetTile(pos, null);
                }
                groundMap.SetTile(pos, groundTile);
            }
        }
    }
}
