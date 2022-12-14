using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D bullet;

    // Start is called before the first frame update
    void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        bullet.velocity = transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var Zomebie = collision.collider.GetComponent<Enemy>();
        var Skeleton = collision.collider.GetComponent<Skeleton>();

        if (Zomebie != null)
        {
            Zomebie.Die();         
        }
        if (Skeleton != null)
        {
            Skeleton.Die();
        }
        Destroy(gameObject);
    }
}
