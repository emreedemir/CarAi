using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Car : MonoBehaviour
{

    public Transform currentTarget;

    public WayPoint wayPoint;

    // Start is called before the first frame update
    void Start()
    {
        wayPoint = FindObjectOfType<WayPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wayPoint.points == null)
        {
            return;
        }
        Vector3 nextPoint;

        Sensor();

        if (currentTarget == null)
        {

            currentTarget = GetShortestPoint(wayPoint.points, currentTarget);
        }
        else
        {
            if (Vector3.Distance(transform.position, currentTarget.position) < 2)
            {
                currentTarget = GetShortestPoint(wayPoint.points, currentTarget);
            }
        }

        if (currentTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.position, 10 * Time.deltaTime);

            transform.rotation = Quaternion.LookRotation(currentTarget.position - transform.position, transform.forward);
        }
    }

    public Transform GetShortestPoint(List<Transform> points, Transform current)
    {

        if (current == null)
        {
            Vector3 carPosition = transform.position;

            float distance = Mathf.Infinity;

            Transform target = null;

            for (int i = 0; i < points.Count; i++)
            {
                if (currentTarget != points[i])
                {
                    float newDistance = Vector3.Distance(transform.position, points[i].position);

                    if (newDistance < distance)
                    {
                        distance = newDistance;

                        target = points[i];
                    }
                }
            }

            return target;
        }
        else
        {
            int index = points.IndexOf(current);

            if (index + 1 == points.Count)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            return points[index];
        }
    }


    public void Sensor()
    {
        for (int i = 0; i < 4; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Quaternion.AngleAxis(-10 * i, transform.up) * transform.forward, out hit, 5))
            {

            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(-10 * i, transform.up) * transform.forward * 5, Color.red);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Quaternion.AngleAxis(10 * i, transform.up) * transform.forward, out hit, 5))
            {

            }
            else
            {
                Debug.DrawLine(transform.position, transform.position + Quaternion.AngleAxis(10 * i, transform.up) * transform.forward * 5, Color.red);
            }
        }

    }
}
