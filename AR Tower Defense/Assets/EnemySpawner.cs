
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    private GameObject spawnedEnemy;
    private bool spawnCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        spawnedEnemy = Instantiate(enemy, gameObject.GetNamedChild("Muzzle").transform.position + transform.forward * 2, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnedEnemy != null) { return; }
        if (spawnCooldown) { return; }
        StartCoroutine(SpawnEnemy());
        spawnCooldown = true;
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(5);
        spawnedEnemy = Instantiate(enemy, gameObject.GetNamedChild("Muzzle").transform.position + transform.forward * 2, transform.rotation);
        spawnCooldown = false;
    }
}
