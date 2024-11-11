using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;
using Unity.XR.CoreUtils;
using TMPro;

public class AgentManager : MonoBehaviour
{
    List<ARAgent> agents = new List<ARAgent>();
    public GameObject beacon;

    [SerializeField] private GameObject BeaconWarn;

    [SerializeField] private NavMeshSurface navmesh;
    // Start is called before the first frame update
    void Start()
    {
        agents = new List<ARAgent>(GetComponentsInChildren<ARAgent>());   
    }

    // Update is called once per frame
    void Update()
    {
        if (beacon == null) { return; }
        GameObject floor = transform.parent.gameObject.GetNamedChild("Floor");
        //check if beacon is on the navmeshsurface
        if (navmesh.navMeshData != null) {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(beacon.transform.position, out hit, 0.1f, NavMesh.AllAreas)) {
                BeaconWarn.SetActive(false);
                MoveAllAgents(beacon.transform.position);
                return;
            }
        }
        BeaconWarn.SetActive(true);
        StopAllAgents();

    }

    public void MoveAllAgents(Vector3 position) {
        foreach (ARAgent agent in agents) {
            agent.MoveAgent(position);
        }
    }

    public void StopAllAgents() {
        foreach (ARAgent agent in agents) {
            agent.StopAgent();
        }
    }

    public void SetBeacon(GameObject beacon) {
        this.beacon = beacon;
    }
}
