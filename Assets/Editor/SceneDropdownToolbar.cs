using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class CustomToolbarSceneSelector
{
    private static string[] scenePaths;
    private static string[] sceneNames;
    private static int selectedSceneIndex = 0;

    static CustomToolbarSceneSelector()
    {
        // Carica le scene disponibili
        LoadScenes();

        // Aggancia l'evento di disegno della Toolbar
        ToolbarCallback.OnToolbarGUI += OnToolbarGUI;
    }

    private static void LoadScenes()
    {
        var scenes = EditorBuildSettings.scenes;
        scenePaths = new string[scenes.Length];
        sceneNames = new string[scenes.Length];

        for (int i = 0; i < scenes.Length; i++)
        {
            scenePaths[i] = scenes[i].path;
            sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(scenes[i].path);
        }
    }

    private static void OnToolbarGUI()
    {
        GUILayout.FlexibleSpace();

        // Disegna la Combo Box per selezionare la scena
        GUILayout.Label("Scene:", GUILayout.Width(50));
        int newSelectedIndex = EditorGUILayout.Popup(selectedSceneIndex, sceneNames, GUILayout.Width(200));
        if (newSelectedIndex != selectedSceneIndex)
        {
            selectedSceneIndex = newSelectedIndex;
            OpenScene(scenePaths[selectedSceneIndex]);
        }
    }

    private static void OpenScene(string scenePath)
    {
        if (EditorApplication.isPlaying)
        {
            Debug.LogWarning("Cannot switch scenes while in Play mode!");
            return;
        }

        UnityEditor.SceneManagement.EditorSceneManager.OpenScene(scenePath);
    }
}

// Supporto per disegnare nella Toolbar
public static class ToolbarCallback
{
    public static System.Action OnToolbarGUI;

    static ToolbarCallback()
    {
        EditorApplication.update += Update;
    }

    private static void Update()
    {
        if (OnToolbarGUI != null)
        {
            SceneView.duringSceneGui -= DuringSceneGUI;
            SceneView.duringSceneGui += DuringSceneGUI;
        }
    }

    private static void DuringSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();
        OnToolbarGUI?.Invoke();
        Handles.EndGUI();
    }
}
