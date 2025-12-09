using UnityEngine;
using System.Collections;

public class AtaquePlayer : MonoBehaviour
{
    [SerializeField] private Transform pontoAtaqueDireita;
    [SerializeField] private Transform pontoAtaqueEsquerda;
    [SerializeField] private float raioAtaque;
    [SerializeField] private LayerMask layersAtaque;
    [SerializeField] private Player player;
    [SerializeField] private Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Entrou no ataque");
            Atacar();

        }
    }
     
    private void OnDrawGizmos()
    {
        if (pontoAtaqueDireita != null)
            Gizmos.DrawWireSphere(pontoAtaqueDireita.position, raioAtaque);

        if (pontoAtaqueEsquerda != null)
            Gizmos.DrawWireSphere(pontoAtaqueEsquerda.position, raioAtaque);
    }

    private void Atacar()
    {
        Debug.Log("Entrou na função ataque");
        anim.SetTrigger("attack");

        // Espera 3 segundos antes do ataque realmente acertar
        //yield return new WaitForSeconds(3f);

        Transform pontoAtaque =
            (player.direcaoMovimento == DirecaoMovimento.Direita)
            ? pontoAtaqueDireita
            : pontoAtaqueEsquerda;

        Collider2D colliderSoldier = Physics2D.OverlapCircle(
            pontoAtaque.position,
            raioAtaque,
            layersAtaque
        );

        if (colliderSoldier != null)
        {
            Debug.Log("Atacando objeto: " + colliderSoldier.name);

            vidasSoldier soldier = colliderSoldier.GetComponentInParent<vidasSoldier>();

            if (soldier != null)
            {
                soldier.ReceberDano();
            }
        }
    }
}

