using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoomManager))]
public class RoomManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script is for creating and joining room", MessageType.Info);

        RoomManager roomManager = (RoomManager)target; //the target inhereted from Editor class
        if (GUILayout.Button("Join Random Room")) //once click the button
        {
            roomManager.JoinRandomRoom();
        }
    }
}
