using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(UnitHealthBehaviour))]
public class UnitEditor : Editor
{public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}