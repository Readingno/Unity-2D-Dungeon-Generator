using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DecorateDungeon : MonoBehaviour
{
    [SerializeField]
    private int mapSize;

    private int[,] map;
    
    [SerializeField]
    private Tilemap groundMap;
    [SerializeField]
    private Tilemap wallMap;
    [SerializeField]
    private Tile[] Tiles_0;
    [SerializeField]
    private Tile[] Tiles_1;
    [SerializeField]
    private Tile[] Tiles_2;
    [SerializeField]
    private Tile[] Tiles_3;
    [SerializeField]
    private Tile[] Tiles_4;

    // Start is called before the first frame update
    void Start()
    {
        map = new int[mapSize, mapSize];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetMapData();
            ChangeWall();
        }
    }

    public void Decorate()
    {
        GetMapData();
        ChangeWall();
    }

    void GetMapData()
    {
        for (int y = -mapSize / 2; y < mapSize / 2; y++)
        {
            for (int x = -mapSize / 2; x < mapSize / 2; x++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                if (wallMap.HasTile(pos))
                {
                    map[x + mapSize / 2, y + mapSize / 2] = 1;
                }
                else
                {
                    map[x + mapSize / 2, y + mapSize / 2] = 0;
                }
            }
        }
    }
    /*
     * 1  2  3
     * 4     5
     * 6  7  8
     */
    public void ChangeWall()
    {
        for (int y = 0; y < mapSize; y++)
        {
            for (int x = 0; x < mapSize; x++)
            {
                if (map[x, y] == 1)
                {
                    // none
                    Vector3Int pos = new Vector3Int(x - mapSize / 2, y - mapSize / 2, 0);
                    wallMap.SetTile(pos, Tiles_0[Random.Range(0, 2)]);
                    // 1
                    if (x > 0 && y < mapSize - 1) 
                    {
                        if (map[x - 1, y + 1] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_1[0]);
                        }
                    }
                    // 8
                    if (x < mapSize-1 && y > 0)
                    {
                        if (map[x + 1, y - 1] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_1[1]);
                        }
                    }
                    // 6
                    if (x > 0 && y > 0)
                    {
                        if (map[x - 1, y - 1] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_1[2]);
                        }
                    }
                    // 3
                    if (x < mapSize-1 && y < mapSize - 1)
                    {
                        if (map[x + 1, y + 1] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_1[3]);
                        }
                    }
                    // 2
                    if (y < mapSize-1)
                    {
                        if (map[x, y + 1] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_1[4]);
                        }
                    }
                    // 4
                    if (x > 0)
                    {
                        if (map[x - 1, y] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_1[5]);
                        }
                    }
                    // 5
                    if (x < mapSize-1)
                    {
                        if (map[x + 1, y] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_1[6]);
                        }
                    }
                    // 7
                    if (y > 0)
                    {
                        if (map[x, y - 1] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_1[7]);
                        }
                    }
                    // 2 4
                    if (x > 0 && y < mapSize - 1)
                    {
                        if (map[x - 1, y] == 0 && map[x, y + 1] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_2[0]);
                        }
                    }
                    // 2 5
                    if (x < mapSize - 1 && y < mapSize - 1)
                    {
                        if (map[x + 1, y] == 0 && map[x, y + 1] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_2[1]);
                        }
                    }
                    // 4 7
                    if (x > 0 && y > 0)
                    {
                        if (map[x - 1, y] == 0 && map[x, y - 1] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_2[2]);
                        }
                    }
                    // 5 7
                    if (x < mapSize - 1 && y > 0)
                    {
                        if (map[x + 1, y] == 0 && map[x, y - 1] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_2[3]);
                        }
                    }
                    // 4 5
                    if (x > 0 && x < mapSize - 1)
                    {
                        if (map[x - 1, y] == 0 && map[x + 1, y] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_2[4]);
                        }
                    }
                    // 2 7
                    if (y > 0 && y < mapSize - 1)
                    {
                        if (map[x, y - 1] == 0 && map[x, y + 1] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_2[5]);
                        }
                    }
                    // 2 6 8
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x - 1, y - 1] == 0 && map[x + 1, y - 1] == 0 && map[x, y + 1] == 0 && map[x, y - 1] == 1 && map[x - 1, y] == 1 && map[x + 1, y] == 1)
                        {
                            wallMap.SetTile(pos, Tiles_3[0]);
                        }
                    }
                    // 1 5 6
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x - 1, y - 1] == 0 && map[x + 1, y] == 0 && map[x - 1, y + 1] == 0 && map[x - 1, y] == 1 && map[x, y - 1] == 1 && map[x, y + 1] == 1)
                        {
                            wallMap.SetTile(pos, Tiles_3[1]);
                        }
                    }
                    // 2 4 8
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x + 1, y - 1] == 0 && map[x - 1, y] == 0 && map[x, y + 1] == 0 && map[x + 1, y] == 1 && map[x, y - 1] == 1)
                        {
                            wallMap.SetTile(pos, Tiles_3[2]);
                        }
                    }
                    // 2 5 6
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x - 1, y - 1] == 0 && map[x + 1, y] == 0 && map[x, y + 1] == 0 && map[x - 1, y] == 1 && map[x, y - 1] == 1)
                        {
                            wallMap.SetTile(pos, Tiles_3[3]);
                        }
                    }
                    // 3 4 8
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x + 1, y - 1] == 0 && map[x - 1, y] == 0 && map[x + 1, y + 1] == 0 && map[x + 1, y] == 1 && map[x, y - 1] == 1 && map[x, y + 1] == 1)
                        {
                            wallMap.SetTile(pos, Tiles_3[4]);
                        }
                    }
                    // 1 3 7
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x, y - 1] == 0 && map[x - 1, y + 1] == 0 && map[x + 1, y + 1] == 0 && map[x, y + 1] == 1 && map[x - 1, y] == 1 && map[x + 1, y] == 1)
                        {
                            wallMap.SetTile(pos, Tiles_3[5]);
                        }
                    }
                    // 3 4 7
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x, y - 1] == 0 && map[x - 1, y] == 0 && map[x + 1, y + 1] == 0 && map[x + 1, y] == 1 && map[x, y + 1] == 1)
                        {
                            wallMap.SetTile(pos, Tiles_3[6]);
                        }
                    }
                    // 1 5 7
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x, y - 1] == 0 && map[x + 1, y] == 0 && map[x - 1, y + 1] == 0 && map[x - 1, y] == 1 && map[x, y + 1] == 1)
                        {
                            wallMap.SetTile(pos, Tiles_3[7]);
                        }
                    }
                    // 4 5 7
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x, y - 1] == 0 && map[x - 1, y] == 0 && map[x + 1, y] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_3[8]);
                        }
                    }
                    // 2 4 5
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x, y + 1] == 0 && map[x - 1, y] == 0 && map[x + 1, y] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_3[9]);
                        }
                    }
                    // 2 5 7
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x, y - 1] == 0 && map[x + 1, y] == 0 && map[x, y + 1] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_3[10]);
                        }
                    }
                    // 2 4 7
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x, y - 1] == 0 && map[x - 1, y] == 0 && map[x, y + 1] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_3[11]);
                        }
                    }
                    // 1 3 6 8
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x - 1, y - 1] == 0 && map[x - 1, y + 1] == 0 && map[x + 1, y - 1] == 0 && map[x + 1, y + 1] == 0 && map[x, y + 1] == 1 && map[x, y - 1] == 1 && map[x + 1, y] == 1 && map[x - 1, y] == 1)
                        {
                            wallMap.SetTile(pos, Tiles_4[0]);
                        }
                    }
                    // 2 4 5 7
                    if (x > 0 && x < mapSize - 1 && y > 0 && y < mapSize - 1)
                    {
                        if (map[x, y + 1] == 0 && map[x, y - 1] == 0 && map[x + 1, y] == 0 && map[x - 1, y] == 0)
                        {
                            wallMap.SetTile(pos, Tiles_4[1]);
                        }
                    }
                }
            }
        }
    }
    /*
     * value of each direction have block
     * 00000001    00000010    00000100
     * 
     * 00001000                00010000
     * 
     * 00100000    01000000    10000000
     *
    public void ChangeWall()
    {
        for (int y = 0; y < mapSize; y++) 
        {
            for (int x = 0; x < mapSize; x++)
            {
                if (map[x,y] == 1)
                {
                    int ad = 0;
                    //top left
                    if (x == 0 || y == 0) 
                    {
                        ad += 1;
                    }
                    else if (map[x - 1, y - 1] == 1)
                    {
                        ad += 1;
                    }
                    //top
                    if (y == 0)
                    {
                        ad += 2;
                    }
                    else if (map[x, y - 1] == 1)
                    {
                        ad += 2;
                    }
                    //top right
                    if (x == mapSize || y == 0)
                    {
                        ad += 4;
                    }
                    else if (map[x + 1, y - 1] == 1) 
                    {
                        ad += 4;
                    }
                    //left
                    if (x == 0)
                    {
                        ad += 8;
                    }
                    else if (map[x - 1, y] == 1)
                    {
                        ad += 8;
                    }
                    //right
                    if (x == mapSize)
                    {
                        ad += 16;
                    }
                    else if (map[x + 1, y] == 1)
                    {
                        ad += 16;
                    }
                    //bottom left
                    if (x == 0 || y == mapSize)
                    {
                        ad += 32;
                    }
                    else if (map[x - 1, y + 1] == 1)
                    {
                        ad += 32;
                    }
                    //bottom
                    if (y == mapSize)
                    {
                        ad += 64;
                    }
                    else if (map[x, y + 1] == 1)
                    {
                        ad += 64;
                    }
                    //bottom right
                    if (x == mapSize || y == mapSize)
                    {
                        ad += 128;
                    }
                    else if (map[x + 1, y + 1] == 1)
                    {
                        ad += 128;
                    }
                }
            }
        }
    }
    */
}
