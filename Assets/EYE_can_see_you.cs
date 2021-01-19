using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EYE_can_see_you : MonoBehaviour
{
    [SerializeField] GameObject[] eyeParts;
    GameObject player;
    public bool focus;
    [SerializeField] [Range(0, 0.05f)] float[] distance;
    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(CloseEye());
    }
    void Update()
    {
        if (focus)
        {
            Vector2 direction = transform.position - player.transform.position;
            float dis;
            Mathf.Clamp(dis = Vector2.Distance(transform.position, player.transform.position), 0, 4);
            eyeParts[0].transform.localPosition = new Vector2((-distance[0] * direction.normalized.x) * dis / 4, (-distance[0] * direction.normalized.y) * dis / 4);
            for (int i = 1; i < eyeParts.Length; i++)
            {
                eyeParts[i].transform.position = new Vector2(eyeParts[i - 1].transform.position.x + ((-distance[i] * direction.normalized.x) * dis / 4), eyeParts[i - 1].transform.position.y + ((-distance[i] * direction.normalized.y) * dis / 4));
            }
        }
        else
        {
            for (int i = 0; i < eyeParts.Length; i++)
            {
                eyeParts[i].transform.localPosition = Vector2.zero;
            }
        }
    }
    IEnumerator CloseEye()
    {
        focus = false;
        yield return new WaitForSecondsRealtime(1);
        focus = true;
        yield return new WaitForSecondsRealtime(Random.Range(2f, 10f));
        StartCoroutine(CloseEye());
    }
}
