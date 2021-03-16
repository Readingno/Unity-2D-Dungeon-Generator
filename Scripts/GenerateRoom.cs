using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateRoom : MonoBehaviour
{

    public int mapSize;
    private int roomSize;
    private int[,] rooms;
    public int[,] map;

    public Tile groundTile;
    public Tile wallTile;
    public Tilemap groundMap;
    public Tilemap wallMap;

    public bool finished;

    // Start is called before the first frame update
    void Start()
    {
        rooms = new int[4, 4];
        roomSize = mapSize / 4;
        mapSize = roomSize * 4;
        finished = true;
    }

    public void GenerateMap()
    {
        InitializeMap();
        StartCoroutine(GenerateRooms());
    }

    void InitializeMap()
    {
        map = new int[mapSize, mapSize];
        for (int x = -mapSize / 2; x < mapSize / 2; x++)
        {
            for (int y = -mapSize / 2; y < mapSize / 2; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                groundMap.SetTile(pos, groundTile);
                wallMap.SetTile(pos, wallTile);
            }
        }
    }

    IEnumerator GenerateRooms()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                rooms[x, y] = 0;
            }
        }
        finished = false;
        int posX = Random.Range(0, 4);
        int posY = 0;
        rooms[posX, posY] = 1;
        int preX = -2;
        int preY = -2;
        bool finish = false;
        //draw a trail
        while (!finish) 
        {
            int dir = Random.Range(1, 6);
            if (dir < 3)    //left
            {
                if (posX == 0 || preX == posX - 1)
                {
                    continue;
                }
                rooms[posX - 1, posY] = 1;
                if (preY == posY - 1)
                {
                    Create_LRD_Room(posX, posY);
                    DrawMap();
                    yield return new WaitForSeconds(0.5f);
                    preX = posX;
                    preY = posY;
                    posX--;
                }
                else
                {
                    Create_LR_Room(posX, posY);
                    DrawMap();
                    yield return new WaitForSeconds(0.5f);
                    preX = posX;
                    preY = posY;
                    posX--;
                }

            }
            else if (dir == 3)   //up
            {
                if (posY == 3)
                {
                    continue;
                }
                rooms[posX, posY + 1] = 1;
                if (preY == posY - 1)
                {
                    Create_LRUD_Room(posX, posY);
                    DrawMap();
                    yield return new WaitForSeconds(0.5f);
                    preX = posX;
                    preY = posY;
                    posY++;
                }
                else
                {
                    Create_LRU_Room(posX, posY);
                    DrawMap();
                    yield return new WaitForSeconds(0.5f);
                    preX = posX;
                    preY = posY;
                    posY++;
                }
            }
            else if (dir > 3)    //right
            {
                if (posX == 3 || preX == posX + 1)
                {
                    continue;
                }
                rooms[posX + 1, posY] = 1;
                if (preY == posY - 1)
                {
                    Create_LRD_Room(posX, posY);
                    DrawMap();
                    yield return new WaitForSeconds(0.5f);
                    preX = posX;
                    preY = posY;
                    posX++;
                }
                else
                {
                    Create_LR_Room(posX, posY);
                    DrawMap();
                    yield return new WaitForSeconds(0.5f);
                    preX = posX;
                    preY = posY;
                    posX++;
                }
            }
            if (preY == 3)
            {
                finish = true;
                Create_LR_Room(posX, posY);
                DrawMap();
                yield return new WaitForSeconds(0.5f);
            }
        }
        //fill the rest rooms
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (rooms[x,y] != 1)
                {
                    int rd = Random.Range(0, 4);
                    switch (rd)
                    {
                        case 0:
                            Create_LR_Room(x, y);
                            break;
                        case 1:
                            Create_LRU_Room(x, y);
                            break;
                        case 2:
                            Create_LRD_Room(x, y);
                            break;
                        case 3:
                            Create_LRUD_Room(x, y);
                            break;
                    }
                }
            }
        }
        DrawMap();
        yield return new WaitForSeconds(0.5f);
        //draw the wall
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                if (x < 2 || x > mapSize - 3 || y < 2 || y > mapSize - 3) 
                {
                    map[x, y] = 1;
                }
            }
        }
        DrawMap();
        finished = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Create_LR_Room(int roomX, int roomY)
    {
        for (int x = roomX * roomSize; x < (roomX + 1) * roomSize; x++)
        {
            for (int y = roomY * roomSize; y < (roomY + 1) * roomSize; y++)
            {
                if (x < roomX * roomSize + 2)
                {
                    if (y < roomY * roomSize + roomSize / 3 || y > (roomY + 1) * roomSize - roomSize / 3 - 1)
                    {
                        map[x, y] = 1;
                    }
                }
                if (x > (roomX + 1) * roomSize - 3)
                {
                    if (y < roomY * roomSize + roomSize / 3 || y > (roomY + 1) * roomSize - roomSize / 3 - 1)
                    {
                        map[x, y] = 1;
                    }
                }
                if (y < roomY * roomSize + 2)
                {
                    map[x, y] = 1;
                }
                if (y > (roomY + 1) * roomSize - 3)
                {
                    map[x, y] = 1;
                }
            }
        }
    }
    void Create_LRU_Room(int roomX, int roomY)
    {
        for (int x = roomX * roomSize; x < (roomX + 1) * roomSize; x++)
        {
            for (int y = roomY * roomSize; y < (roomY + 1) * roomSize; y++)
            {
                if (x < roomX * roomSize + 2)
                {
                    if (y < roomY * roomSize + roomSize / 3 || y > (roomY + 1) * roomSize - roomSize / 3 - 1)
                    {
                        map[x, y] = 1;
                    }
                }
                if (x > (roomX + 1) * roomSize - 3)
                {
                    if (y < roomY * roomSize + roomSize / 3 || y > (roomY + 1) * roomSize - roomSize / 3 - 1)
                    {
                        map[x, y] = 1;
                    }
                }
                if (y < roomY * roomSize + 2)
                {
                    map[x, y] = 1;
                }
                if (y > (roomY + 1) * roomSize - 3)
                {
                    if (x < roomX * roomSize + roomSize / 3 || x > (roomX + 1) * roomSize - roomSize / 3 - 1)
                    {
                        map[x, y] = 1;
                    }
                }
            }
        }
    }
    void Create_LRD_Room(int roomX, int roomY)
    {
        for (int x = roomX * roomSize; x < (roomX + 1) * roomSize; x++)
        {
            for (int y = roomY * roomSize; y < (roomY + 1) * roomSize; y++)
            {
                if (x < roomX * roomSize + 2)
                {
                    if (y < roomY * roomSize + roomSize / 3 || y > (roomY + 1) * roomSize - roomSize / 3 - 1)
                    {
                        map[x, y] = 1;
                    }
                }
                if (x > (roomX + 1) * roomSize - 3)
                {
                    if (y < roomY * roomSize + roomSize / 3 || y > (roomY + 1) * roomSize - roomSize / 3 - 1)
                    {
                        map[x, y] = 1;
                    }
                }
                if (y < roomY * roomSize + 2)
                {
                    if (x < roomX * roomSize + roomSize / 3 || x > (roomX + 1) * roomSize - roomSize / 3 - 1)
                    {
                        map[x, y] = 1;
                    }
                }
                if (y > (roomY + 1) * roomSize - 3)
                {
                    map[x, y] = 1;
                }
            }
        }
    }
    void Create_LRUD_Room(int roomX, int roomY)
    {
        for (int x = roomX * roomSize; x < (roomX + 1) * roomSize; x++)
        {
            for (int y = roomY * roomSize; y < (roomY + 1) * roomSize; y++)
            {
                if (x < roomX * roomSize + 2)
                {
                    if (y < roomY * roomSize + roomSize / 3 || y > (roomY + 1) * roomSize - roomSize / 3 - 1)
                    {
                        map[x, y] = 1;
                    }
                }
                if (x > (roomX + 1) * roomSize - 3)
                {
                    if (y < roomY * roomSize + roomSize / 3 || y > (roomY + 1) * roomSize - roomSize / 3 - 1)
                    {
                        map[x, y] = 1;
                    }
                }
                if (y < roomY * roomSize + 2)
                {
                    if (x < roomX * roomSize + roomSize / 3 || x > (roomX + 1) * roomSize - roomSize / 3 - 1)
                    {
                        map[x, y] = 1;
                    }
                }
                if (y > (roomY + 1) * roomSize - 3)
                {
                    if (x < roomX * roomSize + roomSize / 3 || x > (roomX + 1) * roomSize - roomSize / 3 - 1)
                    {
                        map[x, y] = 1;
                    }
                }
            }
        }
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
