﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zad3 : MonoBehaviour
{
    private float elevatorSpeed = 5f;
    private bool isRunning = false;
    public List<Vector3> waypoints;
    private Vector3 start;
    private Vector3 end;
    private int index = 2;
    private bool reversed = false;
    void Start()
    {
        if (waypoints.Count < 1)
        {
            Debug.Log("You have to input min 1 vectors to start trace");
            enabled = false;
        }
        else
        {
            waypoints.Insert(0, transform.position);
            start = waypoints[0];
            end = waypoints[1];
        }
    }
    void Update()
    {
        if(isRunning)
        {
            if (Vector3.Distance(transform.position, end) <= 0.1f)
            {
                isRunning = false;
                if (index < waypoints.Count)
                {
                    start = end;
                    end = waypoints[index];
                    index++;
                    isRunning = true;
                }
                else
                {
                    isRunning = false;
                    if (reversed==false)
                    {
                        waypoints.Reverse();
                        start = waypoints[0];
                        end = waypoints[1];
                        index = 2;
                        reversed = true;
                        isRunning = true;
                    }
                    else
                    {
                        waypoints.Reverse();
                        start = waypoints[0];
                        end = waypoints[1];
                        index = 2;
                        isRunning = false;
                        reversed = false;
                    }
                }
            }
        }
        
        if (isRunning)
        {
            Vector3 move = (end - start).normalized * elevatorSpeed * Time.deltaTime;
            transform.Translate(move);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.transform.parent = transform;
                isRunning = true;
                Debug.Log("Starting following waypoints");
            }     
    }
}
