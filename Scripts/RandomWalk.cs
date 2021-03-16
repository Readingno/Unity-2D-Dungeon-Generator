using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RandomWalk: MonoBehaviour
{

    public Tile groundTile;
    public Tile wallTile;
    public Tilemap groundMap;
    public Tilemap wallMap;
    public int newRouteRate;
    public int roomRate;
    public int maxRouteLength;
    public int maxRoute;
    public int mapSize;
    /*            _____
     *          
     *           size/2-1
     *   |                      |
     *   | size/2   0  size/2-1 |
     *   |                      |
     *           size/2
     *            _____
     */
    public bool finished;

    int routeCount = 0;

    int coroutineCount = 1;

    private void Start()
    {
        finished = true;
        //GenerateMap();
    }

    private void Update()
    {
        if (coroutineCount == 0)
        {
            coroutineCount = -1;
        }
    }

    public void InitializeMap()
    {
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


    public void GenerateMap()
    {
        routeCount = 0;
        coroutineCount = 1;
        InitializeMap();
        int x = 0;
        int y = 0;
        int routeLength = 0;
        DrawMap(x, y, 0);
        Vector2Int previousPos = new Vector2Int(x, y);
        y += 1;
        DrawMap(x, y, 0);
        StartCoroutine(NewRoute(x, y, routeLength, previousPos));

    }

    private IEnumerator NewRoute(int x, int y, int routeLength, Vector2Int previousPos)
    {
        if (routeCount < maxRoute)
        {
            routeCount++;
            while (++routeLength < maxRouteLength)
            {
                //Initialize
                bool routeUsed = false;
                int xOffset = x - previousPos.x; //0
                int yOffset = y - previousPos.y; //3
                int roomSize = 0; //Hallway size
                if (Random.Range(1, 100) <= roomRate)
                    roomSize = Random.Range(1, 2);
                previousPos = new Vector2Int(x, y);

                //Go Straight
                if (Random.Range(1, 100) <= newRouteRate)
                {
                    //if reach the edge, turn back
                    int nxo = xOffset;
                    int nyo = yOffset;
                    if (previousPos.x + xOffset > mapSize / 2 - 1 || previousPos.x + xOffset < -mapSize / 2 + 2 || previousPos.y + yOffset > mapSize / 2 - 1 || previousPos.y + yOffset < -mapSize / 2 + 2) 
                    {
                        nxo = -xOffset;
                        nyo = -yOffset;
                    }
                    if (routeUsed)
                    {
                        DrawMap(previousPos.x + nxo, previousPos.y + nyo, roomSize);
                        coroutineCount++;
                        yield return new WaitForSeconds(0.05f);
                        StartCoroutine(NewRoute(previousPos.x + nxo, previousPos.y + nyo, Random.Range(routeLength, maxRouteLength), previousPos));
                    }
                    else
                    {
                        x = previousPos.x + nxo;
                        y = previousPos.y + nyo;
                        DrawMap(x, y, roomSize);
                        yield return new WaitForSeconds(0.05f);
                        routeUsed = true;
                    }
                }

                //Go left
                if (Random.Range(1, 100) <= newRouteRate)
                {
                    //if reach the edge, turn back
                    int nxo = xOffset;
                    int nyo = yOffset;
                    if (previousPos.x + xOffset > mapSize / 2 - 1 || previousPos.x + xOffset < -mapSize / 2 + 2 || previousPos.y + yOffset > mapSize / 2 - 1 || previousPos.y + yOffset < -mapSize / 2 + 2)
                    {
                        nxo = -xOffset;
                        nyo = -yOffset;
                    }
                    if (routeUsed)
                    {
                        DrawMap(previousPos.x - nyo, previousPos.y + nxo, roomSize);
                        coroutineCount++;
                        yield return new WaitForSeconds(0.05f);
                        StartCoroutine(NewRoute(previousPos.x - nyo, previousPos.y + nxo, Random.Range(routeLength, maxRouteLength), previousPos));
                    }
                    else
                    {
                        y = previousPos.y + nxo;
                        x = previousPos.x - nyo;
                        DrawMap(x, y, roomSize);
                        yield return new WaitForSeconds(0.05f);
                        routeUsed = true;
                    }
                }
                //Go right
                if (Random.Range(1, 100) <= newRouteRate)
                {
                    //if reach the edge, turn back
                    int nxo = xOffset;
                    int nyo = yOffset;
                    if (previousPos.x + xOffset > mapSize / 2 - 1 || previousPos.x + xOffset < -mapSize / 2 + 2 || previousPos.y + yOffset > mapSize / 2 - 1 || previousPos.y + yOffset < -mapSize / 2 + 2)
                    {
                        nxo = -xOffset;
                        nyo = -yOffset;
                    }
                    if (routeUsed)
                    {
                        DrawMap(previousPos.x + nyo, previousPos.y - nxo, roomSize);
                        coroutineCount++;
                        yield return new WaitForSeconds(0.05f);
                        StartCoroutine(NewRoute(previousPos.x + nyo, previousPos.y - nxo, Random.Range(routeLength, maxRouteLength), previousPos));
                    }
                    else
                    {
                        y = previousPos.y - nxo;
                        x = previousPos.x + nyo;
                        DrawMap(x, y, roomSize);
                        yield return new WaitForSeconds(0.05f);
                        routeUsed = true;
                    }
                }

                if (!routeUsed)
                {
                    //if reach the edge, turn back
                    int nxo = xOffset;
                    int nyo = yOffset;
                    if (previousPos.x + xOffset > mapSize / 2 - 1 || previousPos.x + xOffset < -mapSize / 2 + 2 || previousPos.y + yOffset > mapSize / 2 - 1 || previousPos.y + yOffset < -mapSize / 2 + 2)
                    {
                        nxo = -xOffset;
                        nyo = -yOffset;
                    }
                    x = previousPos.x + nxo;
                    y = previousPos.y + nyo;
                    DrawMap(x, y, roomSize);
                    yield return new WaitForSeconds(0.05f);
                }
            }
        }
        coroutineCount--;
    }

    private void DrawMap(int x, int y, int radius)
    {
        for (int tileX = x - radius; tileX <= x + radius; tileX++)
        {
            for (int tileY = y - radius; tileY <= y + radius; tileY++)
            {
                if (tileX > -mapSize / 2 && tileX < mapSize / 2 - 1 && tileY > -mapSize / 2 && tileY < mapSize / 2 - 1)
                {
                    Vector3Int tilePos = new Vector3Int(tileX, tileY, 0);
                    wallMap.SetTile(tilePos, null);
                }
            }
        }
    }
}
