using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private GameObject BloodPre;

    private Player player;
    private float speed;

    private Rigidbody2D Zombie;

    // Start is called before the first frame update
    private void Start()
    {
        Zombie = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        speed = baseSpeed * (1 + Random.Range(-0.1f, 0.1f));
    }

    // Update is called once per frame
    private void Update()
    {
        if (player != null) 
        { 
        transform.up = (player.transform.position - transform.position).normalized;
        Zombie.velocity = transform.up * speed;
        }
    }

    public void Die()
    {
        Instantiate(BloodPre, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
