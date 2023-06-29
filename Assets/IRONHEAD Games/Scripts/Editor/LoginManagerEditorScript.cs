using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LoginManager))]
public class LoginManagerEditorScript : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.HelpBox("This script is for connecting to Photon Server", MessageType.Info);

        LoginManager loginManager = (LoginManager)target; //the target inhereted from Editor class
        if (GUILayout.Button("Connect Anonymously")) //once click the button
        {
            loginManager.ConnectAnonymously();
        }
    }
}
