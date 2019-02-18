﻿using System.Collections;
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


    private void Start()
    {
        MapGenerate();
    }
    void MapGenerate()
    {

        /*string holderName = "Generate Map";

        if (transform.FindChild(holderName))
        {
            DestroyImmediate(transform.FindChild(holderName));
        }*/



        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePos = new Vector3(mapSize.x/2 + 0.5f + x , 0 ,mapSize.y/2 + 0.5f + y );
                Transform newTile = Instantiate(tilePrefab, tilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;

                //this is to scale the outline width wrt percent.
                //when outlinePercent is 1 then tile scale becomes 0 which means no tile and the outline is more.
                newTile.localScale = Vector3.one * (1-outlinePercent);
            }
        }
    }
}
