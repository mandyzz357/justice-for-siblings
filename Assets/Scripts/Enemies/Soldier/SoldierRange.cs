using UnityEngine;
using System.Collections;

public class SoldierRange : MonoBehaviour
{
    [Header("Ataque à distância")]
    public GameObject bulletPrefab;       
    public Transform playerTransform;
    public Player p1;
    public float cooldownTiro = 1f;      
    public float delayTiroAnim = 0.3f;
    public Transform pontoOrigem;

    [Header("Movimento / Patrulha")]
    private EnemyPatrol patrol;           // Patrulha opcional
    private Rigidbody2D rb;               // Rigidbody do Soldier
    private Animator anim;



    public DirecaoMovimento direcaoMovimento; // Para balas virarem direção

    public bool podeAtirar = true;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
        patrol = GetComponentInParent<EnemyPatrol>();
        rb = GetComponentInParent<Rigidbody2D>();

        // Pega o Player automaticamente se não estiver atribuído
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
        }

        direcaoMovimento = DirecaoMovimento.Direita;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        // Para patrulha quando vê o Player
        if (patrol != null)
            patrol.PararPatrulha();

        // Soldier para ao atirar
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // Atualiza direção olhando para o Player
        if (playerTransform.position.x > transform.position.x)
            direcaoMovimento = DirecaoMovimento.Direita;
        else
            direcaoMovimento = DirecaoMovimento.Esquerda;

        // Ataque à distância
        if (podeAtirar)
        {

            StartCoroutine(AtirarComDelay());
        }
    }

    private IEnumerator AtirarComDelay()
    {
        podeAtirar = false;

        // Trigger de animação da bala
        if (anim != null)
            anim.SetTrigger("atirar");

        Atirar();

        // Espera o frame da animação
        yield return new WaitForSeconds(delayTiroAnim);

        // Instancia a bala
        if (bulletPrefab != null && playerTransform != null)
        {
            Atirar();
            //Bullet bulletScript = bullet.GetComponent<Bullet>();
            //if (bulletScript != null)
            //    bulletScript.SetTarget(playerTransform);

            
        }

        // Aguarda cooldown
        yield return new WaitForSeconds(cooldownTiro);
        podeAtirar = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        // Soldier volta a patrulhar se tiver patrulha
        if (patrol != null && patrol.HasPatrolPoints())
            patrol.RetomarPatrulha();
    }

    void Atirar()
    {
        GameObject bullet = Instantiate(bulletPrefab, pontoOrigem.position, Quaternion.identity);
    }

    private void Update()
    {
        
    }
}
