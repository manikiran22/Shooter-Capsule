using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Transform obstaclePrefab;

    public Vector2 mapSize;

    public int seed =10;
    //outline is the gaps between two tiles
    //it will shrink the tiles and increase the outline 
    [Range(0, 1)]
    public float outlinePercent;

    //spawn the number of obstacles by changing the range
    [Range(0, 1)]
    public float obstaclePercent;

    //allTile is the all tiles in the scene 
    List<Coord> allTileCoord;
    Queue<Coord> shuffleCoord;

    Coord mapCenter;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {

        //step-6
        //this is to loop all the coordinates of all the tiles 
        allTileCoord = new List<Coord>();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoord.Add(new Coord(x, y));
            }
        }

        //step7
        //here since we are using the shufflearray method from the utility class the alltilecord's coord's will be changed
        shuffleCoord = new Queue<Coord>(Utility.ShuffleArray(allTileCoord.ToArray(), seed));

        //step12
        //player spawn pos
        mapCenter = new Coord((int)mapSize.x / 2, (int)mapSize.y / 2);

        //STEp-3            
        string holderName = "Generate Map";

        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }     


        //creates a mapholder transform whihc is the child of Generate map
        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        //STEP-1
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {

                //tilePos is the position of the each tile in the given grid matrix
                //here we did vect 2 to vec 3 thats y z has y representation
                //initially: tilePos = new Vector3(mapSize.x / 2 + 0.5f + x, 0, mapSize.y / 2 + 0.5f + y);
                Vector3 tilePos = CoordToPos(x,y);

                //we did euler and 90 cause the quad needs to be rotated
                Transform newTile = Instantiate(tilePrefab, tilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;


                //Step-2
                //this is to scale the outline width wrt percent.
                //when outlinePercent is 1 then tile scale becomes 0 which means no tile and the outline is more.
                newTile.localScale = Vector3.one * (1-outlinePercent);
                
                //Step-4
                //the tile prefabs transform is now a child of the mapHolder.
                newTile.parent = mapHolder;
            }

        }

        //Step11
        bool[,] obstacleMap = new bool[(int)mapSize.x, (int)mapSize.y];

        int currentObstacleCount = 0;

        //step9
        int obstacleCount = (int)(mapSize.x * mapSize.y * obstaclePercent);
        for (int i = 0; i < obstacleCount; i++)
        {   
            //returning structure from a function
            Coord randomCoord =     GetRandomCoord();
            //randomCoord = CoordToPos);

           

            //checking if the random co0 generatd is not the center map as the player spawns there
            if (randomCoord.x != mapCenter.x && randomCoord.y != mapCenter.y && MapIsFullyAccessible(obstacleMap, currentObstacleCount))
            {
                Vector3 obstaclePos = CoordToPos(randomCoord.x, randomCoord.y);

                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePos + Vector3.up * 0.5f, Quaternion.identity) as Transform;
                newObstacle.parent = mapHolder;
            }
        }

    }

    //step13
    //this is for checking if the map is available with no blockers for the obstacles to spawn
    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {

        return false;
    }
    //step10
    public Vector3 CoordToPos(int x, int y)
    {
        return new Vector3(mapSize.x / 2 + 0.5f + x, 0, mapSize.y / 2 + 0.5f + y);
    }

    //step8
    public Coord GetRandomCoord()
    {
        Coord randomCoord = shuffleCoord.Dequeue();
        shuffleCoord.Enqueue(randomCoord);
        return randomCoord;
    }


    //step5
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

}
