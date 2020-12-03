using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent_Node_Wrong_Path_Script : MonoBehaviour
{
    public bool leverBool;
    public List<GameObject> ventPath;
    public void StartPathChange()
    {
        leverBool = !leverBool;
    }
    private IEnumerator ChangePath()
    {
        int count = ventPath.Count;
        for (int i = 0; i < count; i++)
        {
            ventPath.RemoveAt(ventPath.Count);
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}
