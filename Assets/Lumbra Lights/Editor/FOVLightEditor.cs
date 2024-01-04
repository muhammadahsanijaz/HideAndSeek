using UnityEngine;
using System.Collections;
using UnityEditor;

[System.Serializable]
[CustomEditor(typeof(LightFOV))]
public class FOVLightEditor : Editor
{
    public LightFOV fow;
    static public int selectedLayerTarget;
    static public int selectedLayerOb;
    void OnSceneGUI()
    {
        fow = (LightFOV)target;
        Handles.color = Color.black;
        Handles.DrawWireArc(fow.transform.position, Vector3.forward, Vector3.up, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        Handles.color = Color.green;
        foreach (Transform visibleTarget in fow.visibleTargets)
        {
            //Handles.DrawLine(fow.transform.position, visibleTarget.position);
        }
    }
    public override void OnInspectorGUI()
    {

        fow = (LightFOV)target;
        //////////////////////////////////////////////////////////////////
        GUILayout.Label(Resources.Load("Title") as Texture);
        //////////////////////////////////////////////////////////////////
        GUILayout.BeginVertical(new GUIContent("Main"), "box");
        GUILayout.Space(20);
        fow.viewRadius = EditorGUILayout.Slider(new GUIContent("View Radius", "The total radius of the FOV (Mesh)."), fow.viewRadius, 0, 250);
        fow.viewAngle = EditorGUILayout.Slider(new GUIContent("View Radius", "The angle that will occupy the Mesh, where 360 ​​will be a perfect circle."), fow.viewAngle, 0, 360);
        //serializedObject.FindProperty("targetMask");
        //selectedLayerTarget = EditorGUILayout.LayerField("Layer for Targets", selectedLayerTarget);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("targetMask"), new GUIContent("Target Mask", "The Layer that you want to use for Hidden objects."));
        //fow.targetMask = 1 << selectedLayerTarget;
        serializedObject.ApplyModifiedProperties();
        //serializedObject.FindProperty("obstacleMask");
        //selectedLayerOb = EditorGUILayout.LayerField("Layer for Obstacles", selectedLayerOb);
        //fow.obstacleMask = 1 << selectedLayerOb;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("obstacleMask"), new GUIContent("Obstacle Mask", "The Layer that you want to use for the objects that collide with the FOV."));
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndVertical();
        GUILayout.Space(5);
        //////////////////////////////////////////////////////////////////
        GUILayout.BeginVertical(new GUIContent("Resolution"), "box");
        GUILayout.Space(20);
        fow.meshResolution = EditorGUILayout.Slider(new GUIContent("Mesh Resolution", "The number of verts that will be created in the FOV, more 'Mesh Resolution' and more definition and accuracy. By default, it is  '1'  (Minimum Recommended)."), fow.meshResolution, 1, 10);
        fow.edgeResolveIterations = EditorGUILayout.IntSlider(new GUIContent("Edge Resolve res", "The speed at which you decide the corners in a collision. (4 by default and recommended)"), fow.edgeResolveIterations, 4, 40);
        fow.edgeDstThreshold = EditorGUILayout.Slider(new GUIContent("Edge Threshold", "Edge accuracy (0.5 by default, recommended)."), fow.edgeDstThreshold, 1, 10);
        GUILayout.EndVertical();
        GUILayout.Space(5);
        //////////////////////////////////////////////////////////////////
        fow.ON = EditorGUILayout.BeginToggleGroup("Light features", fow.ON);
        GUILayout.BeginVertical(new GUIContent("Features"), "box");
        GUILayout.Space(20);
        fow.FadeOffRadius = EditorGUILayout.Slider(new GUIContent("Light Radius", "The total radius of the Light renderer."), fow.FadeOffRadius, 0.001f, 150);
        fow.Intensity = EditorGUILayout.Slider(new GUIContent("Intensity Radius", "The Light Intensity. Low number recomended."), fow.Intensity, 0.001f, 30);
        fow.FadeOffColor = EditorGUILayout.ColorField(new GUIContent("Light Color", "The color of the Light."), fow.FadeOffColor);
        GUILayout.EndVertical();
        EditorGUILayout.EndToggleGroup();
        GUILayout.Space(5);
        //////////////////////////////////////////////////////////////////
        fow.eventos = EditorGUILayout.BeginToggleGroup("Enable Events", fow.eventos);
        GUILayout.BeginVertical(new GUIContent("Events"), "box");
        GUILayout.Space(20);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("OnView"));
        GUILayout.Space(10);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("OnNotView"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("TAGS"), true);
        fow.debug = EditorGUILayout.Toggle(new GUIContent("Debug Mode", "Enables Debug mode. (Debug on console'View' or 'NoView'"), fow.debug);
        serializedObject.ApplyModifiedProperties();
        GUILayout.EndVertical();
        EditorGUILayout.EndToggleGroup();
        GUILayout.Space(5);
        if (GUILayout.Button("WRITE A REVIEW"))
        {
            Application.OpenURL("http://u3d.as/1jao");
        }
        //DrawDefaultInspector();
        serializedObject.ApplyModifiedProperties();

    }
}