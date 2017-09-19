using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour {
    public GameObject[] _waypoints;
    private Dictionary<string, GameObject> _waypointList = new Dictionary<string, GameObject>();

    public static WaypointManager Instance = null;
    public GameObject activeWaypoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        for (int i = 0; i < _waypoints.Length; i++)
        {
            _waypointList.Add(_waypoints[i].tag, _waypoints[i]);
        }
    }

    public void SetCurrent(string waypointName)
    {
        GameObject previousWaypoint = activeWaypoint;

        if (_waypointList.TryGetValue(waypointName, out activeWaypoint))
        {
            //Cleanup previous waypoint
            if (previousWaypoint != null)
            {
                foreach (Transform child in previousWaypoint.transform)
                {
                    if (child.CompareTag("Canvas"))
                    {
                        child.transform.gameObject.SetActive(false);
                    }
                    else if (child.CompareTag("Waypoint"))
                    {
                        child.transform.gameObject.SetActive(true);
                        child.transform.gameObject.SendMessage("PlaySound");
                    }
                }
            }

            //Setup new waypoint
            foreach (Transform child in activeWaypoint.transform)
            {
                if (child.CompareTag("Canvas"))
                {
                    child.transform.gameObject.SetActive(true);
                }
                else if (child.CompareTag("Waypoint"))
                {
                    child.transform.gameObject.SetActive(false);
                }
            }
        }
    }
}
