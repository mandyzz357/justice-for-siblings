using UnityEngine;

public class SoldierController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 10; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        speed *= -1;
        flip();
    }



    private void flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
    }

    private void Move()

    {
        rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y); 
    }

















}
