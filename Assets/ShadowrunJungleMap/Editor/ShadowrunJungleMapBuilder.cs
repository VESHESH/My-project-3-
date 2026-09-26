using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ShadowrunJungleMapBuilder : MonoBehaviour
{
    [Header("Optional: assign the imported OBJ here")]
    public GameObject importedMap;

    [ContextMenu("Create Gameplay Markers")]
    public void CreateGameplayMarkers()
    {
        CreateMarker("Start_ScrollHandoff", new Vector3(50, 1.2f, 14), Color.yellow);
        CreateMarker("Checkpoint_A", new Vector3(53, 3.5f, 49), Color.cyan);
        CreateMarker("Checkpoint_B", new Vector3(58, 3.5f, 78), Color.cyan);
        CreateMarker("Checkpoint_C", new Vector3(61, 3.5f, 115), Color.cyan);
        CreateMarker("Boss_RivalNinja", new Vector3(62, 3.5f, 135), new Color(1f, .2f, .55f));
        CreateMarker("Goal_SisterTemple", new Vector3(62, 6f, 144), Color.magenta);
        Debug.Log("Shadowrun markers created. Attach your player, enemy AI, and checkpoint logic to these named objects.");
    }

    private void CreateMarker(string markerName, Vector3 position, Color color)
    {
        var old = GameObject.Find(markerName); if (old != null) DestroyImmediate(old);
        var marker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        marker.name = markerName; marker.transform.position = position; marker.transform.localScale = new Vector3(1.5f, .15f, 1.5f);
        var mat = new Material(Shader.Find("Standard")); mat.color = color; mat.EnableKeyword("_EMISSION"); mat.SetColor("_EmissionColor", color * 2f);
        marker.GetComponent<Renderer>().sharedMaterial = mat;
    }

#if UNITY_EDITOR
    [MenuItem("Shadowrun/Create Jungle Map Root")]
    public static void CreateRoot()
    {
        var root = new GameObject("SHADOWRUN_JUNGLE_LEVEL");
        var builder = root.AddComponent<ShadowrunJungleMapBuilder>();
        Selection.activeGameObject = root;
        Undo.RegisterCreatedObjectUndo(root, "Create Shadowrun Jungle Level");
        Debug.Log("Drag ShadowrunJungleMap.obj into the scene as a child of SHADOWRUN_JUNGLE_LEVEL, then use the component context menu to create markers.");
    }
#endif
}
