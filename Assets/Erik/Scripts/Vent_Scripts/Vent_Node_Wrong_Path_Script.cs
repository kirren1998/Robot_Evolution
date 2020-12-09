using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent_Node_Wrong_Path_Script : MonoBehaviour
{
    public bool leverBool, going, active;
    public List<GameObject> ventPath;
    public GameObject nextInLine;
    private void Update()
    {
        if (going) if (Input.GetKeyDown(KeyCode.E)) active = true;
    }
    public void StartPathChange()
    {
        leverBool = !leverBool;
        StartCoroutine(ChangePath());
    }
    private IEnumerator ChangePath()
    {
        going = true;
        int count = ventPath.Count;
        for (int i = 0; i < count; i++)
        {
            Destroy(ventPath[ventPath.Count - 1]);
            ventPath.RemoveAt(ventPath.Count - 1);
            if (!active)
                yield return new WaitForSecondsRealtime(0.14f);
        }

        GameObject shit = GameObject.Find("ShitHolder");
        GameObject newVentPath = shit.GetComponent<Shit_Holder_script>().Shit[0];
        float dist = 0.25f;

        GameObject start, end;
        start = gameObject;
        if (leverBool) end = gameObject.transform.GetChild(0).gameObject;
        else end = nextInLine;
        float distance = Vector3.Distance(start.transform.position, end.transform.position);
        int ventCount = Mathf.RoundToInt(distance / dist);
        Vector3 direction = (end.transform.position - start.transform.position).normalized;
        Quaternion lookDir = Quaternion.LookRotation(direction);
        for (int a = 0; a < ventCount - 1; a++)
        {
            GameObject vent = Instantiate(newVentPath, start.transform.position + (direction * ((a + 1) * dist)),
                Quaternion.FromToRotation(start.transform.position,
                end.transform.position).normalized, GameObject.Find("ShitHolder").transform);
            if (direction.x < 0)
                vent.transform.rotation = Quaternion.Euler(0, 0, lookDir.eulerAngles.x);
            else
                vent.transform.rotation = Quaternion.Euler(0, 0, -lookDir.eulerAngles.x);
            start.GetComponent<Vent_Node_Wrong_Path_Script>().ventPath.Add(vent);
            if (!active)
                yield return new WaitForSecondsRealtime(0.14f);
        }
        GameObject.Find("Main Camera").GetComponent<Player_Camera_Follow_Script>().EndVentWatching();
        going = false;
        active = false;
    }
}
