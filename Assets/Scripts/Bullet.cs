using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;
    public int damage = 1;
    private Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    public void SetTarget(Transform t) => target = t;

    void FixedUpdate()
    {
        //if (target == null) { Destroy(gameObject); return; }

        //Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = Vector3.left * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.ReceberDano(damage);
                Destroy(gameObject);
            }
        }

        //if (!collision.CompareTag("Enemy") )
        //    Destroy(gameObject);
    }
}

