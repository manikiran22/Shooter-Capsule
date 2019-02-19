using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Vector2 mapSize;

    //outline is the gaps between two tiles
    //it will shrink the tiles and increase the outline 
    [Range(0, 1)]
    public float outlinePercent;

    List<Coord> allTileCoord;
    Queue<Coord> shuffleCoord;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {


        //this is to loop all the coordinates of all the tiles 
        allTileCoord = new List<Coord>();
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                allTileCoord.Add(new Coord(x, y));
            }
        }

        shuffleCoord = new Queue<Coord>(Utility.ShuffleArray(allTileCoord.ToArray(), 0));



        string holderName = "Generate Map";

        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }     

        //creates a mapholder transform whihc is the child of Generate map
        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePos = new Vector3(mapSize.x/2 + 0.5f + x , 0 ,mapSize.y/2 + 0.5f + y );
                Transform newTile = Instantiate(tilePrefab, tilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;

                //this is to scale the outline width wrt percent.
                //when outlinePercent is 1 then tile scale becomes 0 which means no tile and the outline is more.
                newTile.localScale = Vector3.one * (1-outlinePercent);
                
                //the tile prefabs transform is now a child of the mapHolder.
                newTile.parent = mapHolder;
            }
        }
    }

    public struct Coord
    {
        int x;
        int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }
    }

}
