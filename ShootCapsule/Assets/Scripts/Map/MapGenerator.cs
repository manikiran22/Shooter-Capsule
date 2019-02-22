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

    //allTile is the all tiles in the scene 
    List<Coord> allTileCoord;
    Queue<Coord> shuffleCoord;

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


        //step9
        int obstacleCount = 50;
        for (int i = 0; i < obstacleCount; i++)
        {   
            //returning structure from a function
            Coord randomCoordGetter = GetRandomCoord();
            //randomCoord = CoordToPos);
            Vector3 obstaclePos = CoordToPos(randomCoordGetter.x, randomCoordGetter.y);

            Transform newObstacle = Instantiate(obstaclePrefab, obstaclePos + Vector3.up * 0.5f ,Quaternion.identity) as Transform;
            newObstacle.parent = mapHolder;
        }

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
