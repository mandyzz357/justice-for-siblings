using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float speed;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (gameMonange.spawnPosition != Vector3.zero)
        {
            transform.position = gameMonange.spawnPosition;
        }

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.linearVelocity = direction.normalized * speed;

        if(direction.x != 0)
        {
            ResetLayers();

            anim.SetLayerWeight(2, 1);

            if (direction.x > 0)
            {
                sprite.flipX = false;
            }

            else if (direction.x < 0)
            {
                sprite.flipX = true;
            }
        }

        if (direction.y > 0 && direction.x == 0)
        {
            ResetLayers();
            anim.SetLayerWeight(1, 1);

        }

        if (direction.y < 0 && direction.x == 0)
        {
            ResetLayers();
            anim.SetLayerWeight(0, 1);

        }

        if (direction != Vector2.zero)
        {
            anim.SetBool("walking", true);
        }

        else
        {
            anim.SetBool("walking", false);
        }
     }


    private void ResetLayers()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 0);
        anim.SetLayerWeight(2, 0);
      

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Transicaopika"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Laborotorio2");
        }

        if (collision.CompareTag("voltar"))
        {

            gameMonange.spawnPosition = new Vector3(8.42f, -3.45f, 0f);

            UnityEngine.SceneManagement.SceneManager.LoadScene("Laborotorio1");
            
        }


    }


}
