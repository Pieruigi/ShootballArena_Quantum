using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class SceneSelector : EditorWindow
{
    private string[] sceneNames;
    private int selectedSceneIndex = 0;

    [MenuItem("Tools/Scene Selector")]
    public static void ShowWindow()
    {
        GetWindow<SceneSelector>("Scene Selector");
    }

    private void OnEnable()
    {
        LoadScenes();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Select a Scene to Open", EditorStyles.boldLabel);

        if (sceneNames != null && sceneNames.Length > 0)
        {
            selectedSceneIndex = EditorGUILayout.Popup("Scenes", selectedSceneIndex, sceneNames);

            if (GUILayout.Button("Open Scene"))
            {
                OpenScene(sceneNames[selectedSceneIndex]);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("No scenes found in the Build Settings!", MessageType.Warning);
        }
    }

    private void LoadScenes()
    {
        var scenes = EditorBuildSettings.scenes;
        sceneNames = new string[scenes.Length];
        for (int i = 0; i < scenes.Length; i++)
        {
            sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(scenes[i].path);
        }
    }

    private void OpenScene(string sceneName)
    {
        string scenePath = null;
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (System.IO.Path.GetFileNameWithoutExtension(scene.path) == sceneName)
            {
                scenePath = scene.path;
                break;
            }
        }

        if (!string.IsNullOrEmpty(scenePath))
        {
            EditorSceneManager.OpenScene(scenePath);
        }
    }
}
