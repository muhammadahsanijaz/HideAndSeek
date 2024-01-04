using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorScript : MonoBehaviour
{
    [MenuItem("GameObject/Lumbra/2D/FOV/FOV 2D", false, 29)]
    private static void NewMenuOption00()
    {
        PrefabUtility.InstantiatePrefab(Resources.Load("FOV 2D") as GameObject);
    }
    [MenuItem("GameObject/Lumbra/2D/FOV/FOV 2D Shadowness", false, 29)]
    private static void NewMenuOption01()
    {
        PrefabUtility.InstantiatePrefab(Resources.Load("FOV 2D StencilShadow") as GameObject);
    }
    [MenuItem("GameObject/Lumbra/2D/Lights/Radial Light", false, 29)]
    private static void NewMenuOption02()
    {
        PrefabUtility.InstantiatePrefab(Resources.Load("RadialLight") as GameObject);
    }
    [MenuItem("GameObject/Lumbra/2D/Lights/Spot Light", false, 29)]
    private static void NewMenuOption03()
    {
        PrefabUtility.InstantiatePrefab(Resources.Load("SpotLight") as GameObject);
    }
    [MenuItem("GameObject/Lumbra/2D/FOV/StencilShadow2D", false, 200)]
    private static void NewMenuOption04()
    {
        PrefabUtility.InstantiatePrefab(Resources.Load("StencilShadow2D") as GameObject);
    }
    [MenuItem("GameObject/Lumbra/3D/FOV 3D", false, 29)]
    private static void NewMenuOption05()
    {
        PrefabUtility.InstantiatePrefab(Resources.Load("FOV 3D") as GameObject);
    }
    [MenuItem("GameObject/Lumbra/3D/FOV 3D Shadowness", false, 29)]
    private static void NewMenuOption06()
    {
        PrefabUtility.InstantiatePrefab(Resources.Load("FOV 3D StencilShadow") as GameObject);
    }
    [MenuItem("GameObject/Lumbra/3D/StencilShadow3D", false, 200)]
    private static void NewMenuOption07()
    {
        PrefabUtility.InstantiatePrefab(Resources.Load("StencilShadow3D") as GameObject);
    }
}
