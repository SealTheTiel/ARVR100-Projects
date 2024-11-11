using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class TurretBehavior : MonoBehaviour
{
    private bool canFire = true;
    //serializefield slider
    private int firingSpeed = 2;
    [SerializeField]
    private GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Fire()
    {
        if (!canFire) { return; }
        print("------------------------------");
        canFire = false;
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("fire");

        GameObject spawnedBullet = Instantiate(bullet, gameObject.GetNamedChild("Muzzle").transform.position, transform.rotation);
        spawnedBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
        StartCoroutine(FireCoroutine());
    }

    IEnumerator FireCoroutine()
    {
        print("Fire");
        print(1f/firingSpeed);
        yield return new WaitForSeconds(1f/firingSpeed);
        print("Ready to fire");
        canFire = true;
    }
}
