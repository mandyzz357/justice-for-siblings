using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Movimento")]
    public float speed = 5f;

    [Header("Vida")]
    public int vidaMax = 10;
    private int vidaAtual;
    public bool Morto;

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    public PlayerHealth pH;

    public DirecaoMovimento direcaoMovimento;

    void Start()
    {
        Morto = false;
        // Inicializa vida
        vidaAtual = vidaMax;
        pH = GetComponent<PlayerHealth>();
        pH.MaxVidaPlayer = vidaMax;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        direcaoMovimento = DirecaoMovimento.Direita;

        // Posicionamento inicial (caso exista)
        if (gameMonange.spawnPosition != Vector3.zero)
            transform.position = gameMonange.spawnPosition;
    }

    void FixedUpdate()
    {
        if (!Morto)
        {


            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            // Movimento
            rb.linearVelocity = direction.normalized * speed;

            // Animação horizontal
            if (direction.x != 0)
            {
                ResetLayers();
                anim.SetLayerWeight(2, 1);
                sprite.flipX = direction.x < 0;
            }

            // Animação vertical
            if (direction.y > 0 && direction.x == 0)
            {
                ResetLayers();
                anim.SetLayerWeight(1, 1);
            }
            else if (direction.y < 0 && direction.x == 0)
            {
                ResetLayers();
                anim.SetLayerWeight(0, 1);
            }

            anim.SetBool("walking", direction != Vector2.zero);

            // Direção para armas / tiros
            if (direction.x > 0) direcaoMovimento = DirecaoMovimento.Direita;
            else if (direction.x < 0) direcaoMovimento = DirecaoMovimento.Esquerda;

        }
    }

    private void ResetLayers()
    {
        anim.SetLayerWeight(0, 0);
        anim.SetLayerWeight(1, 0);
        anim.SetLayerWeight(2, 0);
    }

    // =====================
    // Método público para receber dano
    // =====================
    public void ReceberDano(int dano)
    {
        if (!Morto)
        {
            vidaAtual -= dano;
            //Debug.Log($"Player levou {dano} de dano! Vida atual: {vidaAtual}");

            pH.ReceberDano(vidaAtual);

            if (vidaAtual <= 0)
            {
                Morrer();
                Morto = true;
            }
        }
    }

    private void Morrer()
    {
        Debug.Log("Player morreu!");
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("game over");


        StartCoroutine(Vortemo());
    }

    public IEnumerator Vortemo()
    {

        
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOver");

    }

    private IEnumerator Ganhemo()
    {


        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Vitoria");

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
      
    }
}
