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
    public void StartPathChange(bool bil)
    {
        leverBool = !leverBool;
        StartCoroutine(ChangePath());
        active = bil;
    }
    private IEnumerator ChangePath()
    {
        GameObject light = Instantiate(GameObject.Find("ShitHolder").GetComponent<Shit_Holder_script>().Shit[3]);
        going = true;
        int count = ventPath.Count;
        for (int i = 0; i < count; i++)
        {
            light.transform.position = ventPath[ventPath.Count - 1].transform.position;
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
            light.transform.position = vent.transform.position;
            if (direction.x < 0)
                vent.transform.rotation = Quaternion.Euler(0, 0, lookDir.eulerAngles.x);
            else
                vent.transform.rotation = Quaternion.Euler(0, 0, -lookDir.eulerAngles.x);
            start.GetComponent<Vent_Node_Wrong_Path_Script>().ventPath.Add(vent);
            if (!active)
                yield return new WaitForSecondsRealtime(0.14f);
        }
        GameObject.Find("Main Camera").GetComponent<Player_Camera_Follow_Script>().EndVentWatching();
        Destroy(light);
        going = false;
        active = false;
    }
}
