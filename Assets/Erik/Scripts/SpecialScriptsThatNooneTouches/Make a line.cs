using UnityEngine;
using UnityEditor;

public class Makealine : EditorWindow
{
    [MenuItem("Window/VentBuilder")]
    public static void ShowWindow()
    {
        GetWindow<Makealine>("VentBuilder");
    }
    private void OnGUI()
    {
        if (GUILayout.Button("Create a vent"))
        {
            GameObject node = new GameObject("Node"), entrance = new GameObject("VentRance"), start = new GameObject("StartNode");
            entrance.AddComponent<Vent_Holder_Script>();
            start.AddComponent<BoxCollider2D>();
            start.AddComponent<SpriteRenderer>();
            start.AddComponent<Vent_Entrance_Script>();
            start.AddComponent<Vent_Node_Wrong_Path_Script>();
            start.GetComponent<BoxCollider2D>().isTrigger = true;
            start.transform.parent = entrance.transform;
            start.transform.localPosition = Vector2.zero;
            node.AddComponent<SpriteRenderer>();
            node.AddComponent<Vent_Node_Wrong_Path_Script>();
            node.transform.parent = entrance.transform;
            node.transform.localPosition = Vector2.zero;
        }
        GUILayout.Label("Simply select the entrance of a vent,", EditorStyles.boldLabel);
        GUILayout.Label("click the 'Click here to make a path' button,", EditorStyles.boldLabel);
        GUILayout.Label("and it will make a path.", EditorStyles.boldLabel);
        GUILayout.Label("If its not named 'StartNode',", EditorStyles.boldLabel);
        GUILayout.Label("nothing will happen!", EditorStyles.boldLabel);
        if (GUILayout.Button("Click here to make path"))
        {
            if (!GameObject.Find("ShitHolder")) { Debug.LogError("There is no 'ShitHolder' in the scene, please drag out an instance of the 'ShitHolder' GameObject into the scene!"); return; }
            if (Selection.objects.Length == 0) { Debug.LogError("Please select an entrance named 'StartNode'"); return; }
            Vent_Holder_Script VHS = Selection.gameObjects[0].transform.parent.GetComponent<Vent_Holder_Script>();
            GameObject shit = GameObject.Find("ShitHolder");
            if (Selection.gameObjects[0].name != "StartNode") { Debug.LogError("The selected GameObject is not a StartNode!"); return; }
            GameObject ventEntrance = Selection.gameObjects[0].transform.parent.gameObject;
            GameObject ventPath = shit.GetComponent<Shit_Holder_script>().Shit[0], lever = shit.GetComponent<Shit_Holder_script>().Shit[1];
            float dist = 0.25f;
            int iterations = 2;
            for (int me = 0; me < iterations; me++)
            {
                int ChildCount = ventEntrance.transform.childCount;
                for (int i = 1; i < ChildCount; i++)
                {
                    GameObject start, end;
                    start = ventEntrance.transform.GetChild(i - 1).gameObject;
                    end = ventEntrance.transform.GetChild(i).gameObject;
                    float distance = Vector3.Distance(start.transform.position, end.transform.position);
                    int ventCount = Mathf.RoundToInt(distance / dist);
                    Vector3 direction = (end.transform.position - start.transform.position);
                    Quaternion lookDir = Quaternion.LookRotation(direction);
                    if (start.GetComponent<Vent_Node_Wrong_Path_Script>().ventPath != null)
                    {
                        int count = start.GetComponent<Vent_Node_Wrong_Path_Script>().ventPath.Count;
                        for (int d = 0; d < count; d++)
                            DestroyImmediate(start.GetComponent<Vent_Node_Wrong_Path_Script>().ventPath[d]);
                        start.GetComponent<Vent_Node_Wrong_Path_Script>().ventPath.Clear();
                    }
                    start.GetComponent<Vent_Node_Wrong_Path_Script>().nextInLine = end;
                    for (int a = 0; a < ventCount - 1; a++)
                    {
                        GameObject vent = Instantiate(ventPath, start.transform.position + (direction.normalized * ((a + 1) * dist)), 
                            Quaternion.identity, shit.transform);
                        vent.transform.rotation = Quaternion.Euler(0, 0, -lookDir.eulerAngles.x);
                        start.GetComponent<Vent_Node_Wrong_Path_Script>().ventPath.Add(vent);
                    }
                    if (start.transform.childCount > 0)
                    {
                        VHS.extraPath.Add(start.gameObject);
                        GameObject LeverConnector = Instantiate(lever, start.transform.position,
                        Quaternion.identity, shit.transform.GetChild(0));
                        LeverConnector.GetComponent<Lever_Script>().leverConnection = start.gameObject;
                    }
                }
                if (VHS.extraPath.Count > 0)
                {
                    ventEntrance = VHS.extraPath[0];
                    VHS.extraPath.RemoveAt(0);
                    iterations += 1;
                }
            }
        }
        GUILayout.Label("If you want to know what switch is connected", EditorStyles.boldLabel);
        GUILayout.Label("to each specific drumm,", EditorStyles.boldLabel);
        GUILayout.Label("simply press the button called", EditorStyles.boldLabel);
        GUILayout.Label("'Check Connections'", EditorStyles.boldLabel);
        GUILayout.Label("and smile as you get enlitend! :P", EditorStyles.boldLabel);
        if (GUILayout.RepeatButton("Check Connections"))
            for (int i = 0; i < GameObject.Find("ShitHolder").transform.GetChild(0).childCount; i++)
            {
                Debug.DrawRay(GameObject.Find("ShitHolder").transform.GetChild(0).GetChild(i).transform.position, 
                    GameObject.Find("ShitHolder").transform.GetChild(0).GetChild(i).GetComponent<Lever_Script>().leverConnection.transform.position - 
                    GameObject.Find("ShitHolder").transform.GetChild(0).GetChild(i).transform.position, Color.green);
                Debug.DrawRay(GameObject.Find("ShitHolder").transform.GetChild(0).GetChild(i).GetComponent<Lever_Script>().leverConnection.transform.position, 
                    GameObject.Find("ShitHolder").transform.GetChild(0).GetChild(i).GetComponent<Lever_Script>().leverConnection.transform.GetChild(0).transform.position -
                    GameObject.Find("ShitHolder").transform.GetChild(0).GetChild(i).GetComponent<Lever_Script>().leverConnection.transform.position, Color.red);
            }
    }
}
