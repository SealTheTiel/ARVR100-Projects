using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private int maxHealth = 8;
    private int health = 8;
    [SerializeField]
    private Scrollbar healthBar;
    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(transform.forward * rigidbody.mass * -10);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.transform.LookAt(Camera.main.transform);
    }
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Turret")) {
            Destroy(other.gameObject);
        }    
    }

    public void TakeDamage() {
        health--;
        healthBar.size = (float)health / maxHealth;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
