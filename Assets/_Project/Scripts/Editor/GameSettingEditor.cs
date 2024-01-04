using System.Collections;
using System.Collections.Generic;
using UnityEditor;
namespace HideAndSeek
{
    [CustomEditor(typeof(GameSettings))]
    public class GameSettingEditor : Editor
    {
        [MenuItem("HideAndSeek/GameSetting")]
        static void SelectSetting()
        {
            Selection.activeObject = AssetDatabase.LoadMainAssetAtPath("Assets/_Project/Scriptables/GameSettings.asset");
        }
    }
}
