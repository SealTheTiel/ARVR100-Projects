using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    List<ARAgent> agents = new List<ARAgent>();
    [SerializeField]
    private GameObject beacon;
    
    // Start is called before the first frame update
    void Start()
    {
        agents = new List<ARAgent>(GetComponentsInChildren<ARAgent>());   
    }

    // Update is called once per frame
    void Update()
    {
        if (!beacon.activeSelf) {
            StopAllAgents();
            return;
        }
        foreach (ARAgent agent in agents) {
            agent.MoveAgent(beacon.transform.position);
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
