using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class WayPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> points;
    private void Start()
    {
        points = transform.OfType<Transform>().ToList();

        StartCoroutine(CreateAllSprites(points));
    }
    public IEnumerator CreateAllSprites(List<Transform> points)
    {
        Debug.Log(points.Count);

        for (int i = 0; i < points.Count; i++)
        {
            Debug.Log(i);

            if (i + 1 == points.Count)
            {

                Transform prev = points[i - 1];

                Transform current = points[i];

                Transform next = points[0];

                CreatePathSprit(current, next, prev);
            }
            else
            {
                Transform prev;

                if (i != 0)
                {
                    prev = points[i - 1];
                }
                else
                {
                    prev = points[points.Count - 1];
                }

                Transform current = points[i];

                Transform next = points[i + 1];

                CreatePathSprit(current, next, prev);
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private void CreatePathSprit(Transform current, Transform next, Transform prev)
    {

        Vector3 directionPrev = prev.position - transform.position;

        Vector3 direction = (next.transform.position - current.transform.position) + directionPrev;

        Vector3 left = /*Quaternion.AngleAxis(-90, transform.up) * direction.normalized * 3 */ 3 * direction.normalized + current.position;

        Vector3 right =/* Quaternion.AngleAxis(90, transform.up) * direction.normalized * 3*/Quaternion.AngleAxis(-180, transform.up) * direction.normalized + current.position;

        GameObject leftOb = Instantiate(new GameObject("Sprit1"));

        GameObject rigtOb = Instantiate(new GameObject("Sprit2"));

        leftOb.transform.position = left;

        rigtOb.transform.position = right;

        leftOb.transform.SetParent(current);

        rigtOb.transform.SetParent(current);

    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Transform next;

            if (i + 1 == points.Count)
            {
                next = points[0];
            }
            else
            {
                next = points[i + 1];
            }

            Debug.DrawLine(points[i].position, next.position);

            Debug.DrawLine(points[i].GetChild(0).position, next.GetChild(0).position, Color.red);

            Debug.DrawLine(points[i].GetChild(1).position, next.GetChild(1).position, Color.red);
        }
    }

}
