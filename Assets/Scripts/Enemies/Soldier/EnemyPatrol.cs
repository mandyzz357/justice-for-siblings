// EnemyPatrol.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("Configurações de Patrulha")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private Transform pontoA;
    [SerializeField] private Transform pontoB;
    [SerializeField] private int vidas = 4;

    private Transform targetPoint;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private bool isChasingPlayer = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        rb.gravityScale = 0;
    }

    void OnEnable()
    {
        // Se não tiver pontos atribuídos, tenta encontrar automaticamente:
        if (pontoA == null || pontoB == null)
        {
            TryAutoFindPatrolPoints();
        }

        // Se ainda faltar pontos, não continue (evita warnings repetidos).
        if (!HasPatrolPoints())
        {
            // Não desabilita o componente — apenas não inicia a patrulha.
            // Isso evita o spam de warning no console quando SoldierRange liga/desliga.
            return;
        }

        // Escolhe o ponto mais próximo ao reativar
        float distA = Vector2.Distance(transform.position, pontoA.position);
        float distB = Vector2.Distance(transform.position, pontoB.position);

        targetPoint = (distA < distB) ? pontoA : pontoB;

        // Ajusta o flip
        sr.flipX = (targetPoint == pontoB);
    }

    void FixedUpdate()
    {
        // Se estiver atacando, não patrulha
        if (isChasingPlayer)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (!HasPatrolPoints())
            return;

        // Chegou no ponto alvo
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            rb.linearVelocity = Vector2.zero;

            // Troca para o outro ponto
            if (targetPoint == pontoA)
            {
                targetPoint = pontoB;
                sr.flipX = true;
            }
            else
            {
                targetPoint = pontoA;
                sr.flipX = false;
            }
        }
        else
        {
            // Move em direção ao ponto desejado
            Vector2 direction = (targetPoint.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
    }

    // Chamado pelo SoldierRange quando vê o player
    public void PararPatrulha()
    {
        isChasingPlayer = true;
        if (rb != null) rb.linearVelocity = Vector2.zero;
    }

    // Chamado pelo SoldierRange quando perde o player
    public void RetomarPatrulha()
    {
        // só retoma se os pontos existirem
        if (!HasPatrolPoints())
            return;

        isChasingPlayer = false;

        // Escolhe o ponto mais próximo ao retornar
        float distA = Vector2.Distance(transform.position, pontoA.position);
        float distB = Vector2.Distance(transform.position, pontoB.position);

        targetPoint = (distA < distB) ? pontoA : pontoB;
        sr.flipX = (targetPoint == pontoB);
    }

    // Dano e morte
    public void ReceberDano()
    {
        vidas--;
        if (vidas <= 0)
            Destroy(gameObject);
    }

    // Útil para outros scripts verificarem se há pontos válidos
    public bool HasPatrolPoints()
    {
        return pontoA != null && pontoB != null;
    }

    // Tenta encontrar pontos automaticamente:
    private void TryAutoFindPatrolPoints()
    {
        // 1) procura filhos com nomes "PontoA" e "PontoB"
        Transform tA = transform.Find("PontoA");
        Transform tB = transform.Find("PontoB");

        if (tA != null && tB != null)
        {
            pontoA = tA;
            pontoB = tB;
            return;
        }

        // 2) procura na cena objetos com nomes "PontoA" / "PontoB"
        GameObject objA = GameObject.Find("PontoA");
        GameObject objB = GameObject.Find("PontoB");
        if (objA != null && objB != null)
        {
            pontoA = objA.transform;
            pontoB = objB.transform;
            return;
        }

        // 3) (opcional) procura por children com tag "PatrolPoint" e atribui dois primeiros encontrados
        // Isso exige que você marque os pontos com a tag "PatrolPoint"
        Transform[] children = GetComponentsInChildren<Transform>(true);
        Transform foundA = null, foundB = null;
        foreach (var c in children)
        {
            if (c == this.transform) continue;
            if (c.CompareTag("PatrolPoint"))
            {
                if (foundA == null) foundA = c;
                else if (foundB == null) { foundB = c; break; }
            }
        }
        if (foundA != null && foundB != null)
        {
            pontoA = foundA;
            pontoB = foundB;
            return;
        }

    }
}
