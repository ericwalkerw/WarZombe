using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject butlletPrefab;
    [SerializeField] private GameObject BloodPre;
    [SerializeField] private Transform gunPoint;
    private Rigidbody2D player;
    private Vector2 input;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        player.velocity = input.normalized * speed;

        transform.up = (MousePos.GetMouse() - (Vector2)transform.position).normalized;
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
    }

    private void Shoot()
    {
        Instantiate(butlletPrefab,gunPoint.position,transform.rotation);
    }

    public void Die()
    {
        Instantiate(BloodPre, transform.position, Quaternion.identity);
        
        FindObjectOfType<TMP_Text>().enabled = true;
        Destroy(gameObject);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var Zombie = collision.collider.GetComponent<Enemy>();

        if (Zombie != null)
        {
            Die();
        }
    }
}
