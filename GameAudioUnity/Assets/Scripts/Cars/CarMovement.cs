using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public List<Vector3> waypoints;

    public float speed = 10f;

    public float slowSpeed = 3f;

    public float breakDist = 3f;

    public float rotationSpeed = 3f;

    private Vector3 target;

    private float breakAmount = 5f;

    private int currentIndex;

    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
        currentIndex = 0;
        currentSpeed = slowSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckTarrget();

        CheckSpeed();

        Move();

        Rotate();
    }

    private void Rotate()
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void CheckTarrget()
    {
        if ((target - transform.position).magnitude <= 0.1f)
        {
            IncreaseIndex();
            target = waypoints[currentIndex];
        }
    }

    void CheckSpeed()
    {
        if((target - transform.position).magnitude <= breakDist)
        {
            if(currentSpeed == slowSpeed) { return; }
            currentSpeed = LerpToSpeed(slowSpeed);
        }
        else
        {
            if (currentSpeed == speed) { return; }
            Debug.Log("Accelerating");
            currentSpeed = LerpToSpeed(speed);
        }
    }

    void Move()
    {
        Vector3 direction = target - transform.position;

        direction.Normalize();

        transform.Translate(direction * currentSpeed * Time.deltaTime, Space.World);
    }

    float LerpToSpeed(float targetSpeed)
    {
        return Mathf.Lerp(currentSpeed, targetSpeed, breakAmount * Time.deltaTime);
    }

    void IncreaseIndex()
    {
        currentIndex++;
        if(currentIndex >= waypoints.Count)
        {
            currentIndex = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int i = 0; i < waypoints.Count; i++)
        {
            Gizmos.DrawWireSphere(waypoints[i], 1);
        }
    }
}
