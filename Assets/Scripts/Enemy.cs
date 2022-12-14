using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float radius;
    [SerializeField] private GameObject BloodPre;
    [SerializeField] private Transform attackZone;
    [SerializeField] private LayerMask layerName;

    private Player player;
    private float speed;

    private Rigidbody2D Zombie;
    private Animator anim;

    private bool isInAttackZone;

    // Start is called before the first frame update
    private void Start()
    {
        Zombie = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        speed = baseSpeed * (1 + Random.Range(-0.1f, 0.1f));
    }

    // Update is called once per frame
    private void Update()
    {    
        if (player != null) 
        { 
            Attack();
            transform.right = (player.transform.position - transform.position).normalized;
            Zombie.velocity = transform.right * speed;
            SetAnim();
        }
    }
    private void Attack()
    {
        isInAttackZone = Physics2D.OverlapCircle(attackZone.position, radius, layerName);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackZone.position, radius);
    }
    private void SetAnim()
    {
        if (isInAttackZone)
        {
            anim.SetTrigger("attack");
        }
    }

    public void Die()
    {
        Instantiate(BloodPre, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
