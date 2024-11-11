using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;

public class AgentManager : MonoBehaviour
{
    List<ARAgent> agents = new List<ARAgent>();
    private GameObject beacon;
    [SerializeField]
    private NavMeshSurface navmesh;
    // Start is called before the first frame update
    void Start()
    {
        agents = new List<ARAgent>(GetComponentsInChildren<ARAgent>());   
    }

    // Update is called once per frame
    void Update()
    {
        // check if beacon position is within navmeshsurface
        if (navmesh != null && beacon != null) {
            if (navmesh.navMeshData != null) {
                if (NavMesh.SamplePosition(beacon.transform.position, out NavMeshHit hit, 0.1f, NavMesh.AllAreas)) {
                    MoveAllAgents(beacon.transform.position);
                }
            }
        }
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
}
