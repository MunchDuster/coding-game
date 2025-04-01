using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Tester))]
[CanEditMultipleObjects]
public class LookAtPointEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Tester tester = (Tester)target;
        
        if (GUILayout.Button("Test"))
        {
            tester.Start();
        }
    }
}