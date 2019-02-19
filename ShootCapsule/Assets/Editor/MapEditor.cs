using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
//inherited from the Editor base class
public class MapEditor : Editor
{
    //prewritten method for making custom inspector
    //inside the below func we can add our own custom GUI for the inspector of a specific object class.
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        //the object being inspected
        MapGenerator maps = target as MapGenerator;

        maps.GenerateMap();

    }
}
