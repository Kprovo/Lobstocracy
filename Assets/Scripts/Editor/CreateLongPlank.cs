using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;

public class CreateLongPlank : EditorWindow
{
    // Variables
    public GameObject Prefab;
    public static GameObject ShortPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Floor Pieces/Plank_S.prefab", typeof(GameObject)) as GameObject;
    public static GameObject MediumPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Floor Pieces/Plank_M.prefab", typeof(GameObject)) as GameObject;
    public static GameObject LongPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Floor Pieces/Plank_L.prefab", typeof(GameObject)) as GameObject;
    public string Length = "6";
    public bool EditMode = false;
    public GameObject[] ObjectsToReplace;
    public List<GameObject> TempObjects = new List<GameObject>();

    // Add menu named "My Window" to the Window menu
    [MenuItem("Tools/CreateLongPlank")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        CreateLongPlank window = (CreateLongPlank)EditorWindow.GetWindow(typeof(CreateLongPlank));
        window.Show();
        //LongPrefab = AssetDatabase.LoadAssetAtPath("Assets/Floor Pieces/Plank_L.prefab", typeof(GameObject)) as GameObject;
        //Object prefab = AssetDatabase.LoadAssetAtPath("Assets/GameObject.prefab", typeof(GameObject));
    }
    void OnSelectionChange()
    {
        GetSelection();
        Repaint();
    }
    void OnGUI()
    {
        EditMode = GUILayout.Toggle(EditMode, "Edit");
        if (GUI.changed) {
            if (EditMode)
                GetSelection();
            else
                ResetPreview();
        }
        Length = GUILayout.TextField(Length);
        //KeepOriginalNames = GUILayout.Toggle(KeepOriginalNames, "Keep names");
        //ApplyRotation = GUILayout.Toggle(ApplyRotation, "Apply rotation");
        //ApplyScale = GUILayout.Toggle(ApplyScale, "Apply scale");
        GUILayout.Space(5);
        if (EditMode) {
            ResetPreview();

            GUI.color = Color.yellow;
            if (Prefab != null) {
                GUILayout.Label("Prefab: ");
                GUILayout.Label(Prefab.name);
            } else {
                GUILayout.Label("No prefab selected");
            }
            GUI.color = Color.white;

            GUI.color = Color.yellow;
            if (LongPrefab != null) {
                GUILayout.Label("Long Prefab: ");
                GUILayout.Label(LongPrefab.name);
            } else {
                GUILayout.Label("No long prefab selected");
            }
            GUI.color = Color.white;

            GUILayout.Space(5);
            GUILayout.BeginScrollView(new Vector2());
            /*
            foreach (GameObject go in ObjectsToReplace) {
                GUILayout.Label(go.name);
                if (Prefab != null) {
                    GameObject newObject;
                    newObject = (GameObject)PrefabUtility.InstantiatePrefab(Prefab);
                    newObject.transform.SetParent(go.transform.parent, true);
                    newObject.transform.localPosition = go.transform.localPosition;
                    if (ApplyRotation) {
                        newObject.transform.localRotation = go.transform.localRotation;
                    }
                    if (ApplyScale) {
                        newObject.transform.localScale = go.transform.localScale;
                    }
                    TempObjects.Add(newObject);
                    if (KeepOriginalNames)
                        newObject.transform.name = go.transform.name;
                    go.SetActive(false);
                }
            }*/
            var gob = ObjectsToReplace[0];
            GUILayout.Label(gob.name);
            if (Prefab != null) {
                GameObject newObject;
                newObject = (GameObject)PrefabUtility.InstantiatePrefab(Prefab);
                newObject.transform.SetParent(gob.transform.parent, true);
                newObject.transform.localPosition = gob.transform.localPosition;
                TempObjects.Add(newObject);
            }

            GUILayout.EndScrollView();
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Apply")) {
                foreach (GameObject go in ObjectsToReplace) {
                    DestroyImmediate(go);
                }
                EditMode = false;
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene()); // So that we don't forget to save...
            };
            if (GUILayout.Button("Cancel")) {
                ResetPreview();
                EditMode = false;
            };
            GUILayout.EndHorizontal();
        } else {
            ObjectsToReplace = new GameObject[0];
            TempObjects.Clear();
            Prefab = null;
        }

    }
    void OnDestroy()
    {
        ResetPreview();
    }
    void GetSelection()
    {
        if (EditMode && Selection.activeGameObject != null) {
            PrefabType t = PrefabUtility.GetPrefabType(Selection.activeGameObject);
            if (t == PrefabType.Prefab) //Here goes the fix
            {
                Prefab = Selection.activeGameObject;
            } else {
                ResetPreview();
                ObjectsToReplace = Selection.gameObjects;
            }
        }
    }
    void ResetPreview()
    {
        if (TempObjects != null) {
            foreach (GameObject go in TempObjects) {
                DestroyImmediate(go);
            }
        }
        foreach (GameObject go in ObjectsToReplace) {
            go.SetActive(true);
        }
        TempObjects.Clear();
    }

}
