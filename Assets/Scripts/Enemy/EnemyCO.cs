using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCO : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
           // Physics2D.IgnoreCollision(collision.collider.GetComponent<Player>());
        }
    }
}
