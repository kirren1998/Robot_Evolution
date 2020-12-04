using UnityEngine;
using UnityEditor;

public class Makealine : EditorWindow
{
    float dist;
    
    [MenuItem("Window/VentBuilder")]
    public static void ShowWindow()
    {
        GetWindow<Makealine>("VentBuilder");
    }
    private void OnGUI()
    {
        GUILayout.Label("Simply select the entrance of a vent,", EditorStyles.boldLabel);
        GUILayout.Label("click the 'Click here to make a path' button,", EditorStyles.boldLabel);
        GUILayout.Label("and it will make a path.", EditorStyles.boldLabel);
        GUILayout.Label("If its not an entrance of a vent,", EditorStyles.boldLabel);
        GUILayout.Label("nothing will happen!", EditorStyles.boldLabel);

        if (GUILayout.Button("Click here to make path"))
        {
            Vent_Holder_Script VHS = Selection.gameObjects[0].transform.parent.GetComponent<Vent_Holder_Script>();

            GameObject shit = GameObject.Find("ShitHolder");
            dist = 0.25f;
            if (!Selection.gameObjects[0].CompareTag("Vent")) return;
            GameObject ventEntrance = Selection.gameObjects[0].transform.parent.gameObject;
            GameObject ventPath = shit.GetComponent<Shit_Holder_script>().Shit[0], lever = shit.GetComponent<Shit_Holder_script>().Shit[1];
            int yeet = 2;
            for (int me = 0; me < yeet; me++)
            {
                int ChildCount = ventEntrance.transform.childCount;
                for (int i = 1; i < ChildCount; i++)
                {
                    GameObject start, end;
                    start = ventEntrance.transform.GetChild(i - 1).gameObject;
                    end = ventEntrance.transform.GetChild(i).gameObject;
                    float distance = Vector3.Distance(start.transform.position, end.transform.position);
                    int ventCount = Mathf.RoundToInt(distance / dist);
                    Vector3 direction = (end.transform.position - start.transform.position).normalized;
                    int count = start.GetComponent<Vent_Node_Wrong_Path_Script>().ventPath.Count;
                    for (int d = 0; d < count; d++)
                    {
                        DestroyImmediate(start.GetComponent<Vent_Node_Wrong_Path_Script>().ventPath[d]);
                    }
                    start.GetComponent<Vent_Node_Wrong_Path_Script>().ventPath.Clear();
                    start.GetComponent<Vent_Node_Wrong_Path_Script>().nextInLine = end;
                    for (int a = 0; a < ventCount - 1; a++)
                    {
                        GameObject vent = Instantiate(ventPath, start.transform.position + (direction * ((a + 1) * dist)), 
                            Quaternion.FromToRotation(start.transform.position, 
                            end.transform.position).normalized, GameObject.Find("ShitHolder").transform);
                        start.GetComponent<Vent_Node_Wrong_Path_Script>().ventPath.Add(vent);
                    }
                    if (start.transform.childCount > 0)
                    {
                        VHS.extraPath.Add(start.gameObject);
                        GameObject LeverConnector = Instantiate(lever, start.transform.position,
                        Quaternion.identity, shit.transform.parent);
                        LeverConnector.GetComponent<Lever_Script>().leverConnection = start.gameObject;
                    }
                }
                if (VHS.extraPath.Count > 0)
                {
                    ventEntrance = VHS.extraPath[0];
                    VHS.extraPath.RemoveAt(0);
                    yeet += 1;
                }
        }
        }
    }
}
