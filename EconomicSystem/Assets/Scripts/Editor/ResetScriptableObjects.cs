using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class ResetScriptableObjects : EditorWindow
{
    [MenuItem("Tools/Reset All ScriptableObjects")]
    public static void ResetAll()
    {
        string[] actions = AssetDatabase.FindAssets("t:GameplayAction");
        string[] currencies = AssetDatabase.FindAssets("t:Currency");

        foreach (string guid in actions)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GameplayAction obj = AssetDatabase.LoadAssetAtPath<GameplayAction>(assetPath);

            if (obj != null)
            {
                obj.ResetValues(); // Call the reset method
                EditorUtility.SetDirty(obj); // Mark the object as dirty so it gets saved
            }
        }

        foreach (string guid in currencies)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Currency obj = AssetDatabase.LoadAssetAtPath<Currency>(assetPath);

            if (obj != null)
            {
                obj.ResetValues(); // Call the reset method
                EditorUtility.SetDirty(obj); // Mark the object as dirty so it gets saved
            }
        }

        // Save the changes
        AssetDatabase.SaveAssets();
        Debug.Log("All GameplayActions have been reset.");
    }
}

