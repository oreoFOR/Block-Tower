using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField]Transform hole;
    void CalculateRolls()
    {
        Vector3 myPos = new Vector3(transform.position.x, hole.position.y, transform.position.z);
        Vector3 dir = (hole.position - myPos).normalized;
        float dist = Vector3.Distance(hole.position, myPos);
        print("distance: " + dist);
        float angle = Vector3.Angle(transform.right, dir);
        print("angle: "+angle);
        float vDist = Mathf.Round(Mathf.Sin(angle*Mathf.Deg2Rad) * dist);
        print("vDist: " + vDist);
        float hDist = Mathf.Round(Mathf.Cos(angle * Mathf.Deg2Rad) * dist);
        print("hDist: " + hDist);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CalculateRolls();
        }
    }
}
