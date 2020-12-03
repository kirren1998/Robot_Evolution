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

            GameObject shit = GameObject.Find("ShitHolder");
            dist = 0.25f;
            if (!Selection.gameObjects[0].CompareTag("Vent")) return;
            GameObject ventEntrance = Selection.gameObjects[0];
            GameObject ventPath = shit.GetComponent<Shit_Holder_script>().Shit[0];
            
            for (int i = 0; i < ventEntrance.transform.childCount; i++)
            {
                GameObject start, end;
                if (i == 0)
                    start = ventEntrance;
                else start = ventEntrance.transform.GetChild(i - 1).gameObject;
                end = ventEntrance.transform.GetChild(i).gameObject;
                float distance = Vector3.Distance(start.transform.position, end.transform.position);
                int ventCount = Mathf.RoundToInt(distance / dist);
                Vector3 direction = (end.transform.position - start.transform.position).normalized;
                for (int a = 0; a < ventCount - 1; a++)
                {
                    GameObject vent = Instantiate(ventPath, start.transform.position + (direction * ((a + 1) * dist)), 
                        Quaternion.FromToRotation(start.transform.position, 
                        end.transform.position).normalized, GameObject.Find("ShitHolder").transform);
                    if (end.transform.childCount > 0)
                        end.GetComponent<Vent_Node_Wrong_Path_Script>().ventPath.Add(vent);
                }
            }
        }
    }
}
