using UnityEngine;
using System.Collections;

public class AtaqueSoldier : MonoBehaviour
{
    [Header("Pontos de ataque")]
    public Transform pontoAtaqueDireita;
    public Transform pontoAtaqueEsquerda;

    [Header("Configuração")]
    public float raioAtaque = 0.4f;
    public LayerMask layersAtaque;
    public Animator anim;
    public GameObject municao;
    public Vector3 pontoOrigem;


    private bool atacando = false;

    public void TentarAtacar(DirecaoMovimento direcao, Transform playerTransform)
    {
        if (!atacando)
            StartCoroutine(Atacar(direcao, playerTransform));
    }

    private IEnumerator Atacar(DirecaoMovimento direcao, Transform playerTransform)
    {
        atacando = true;
        anim.SetTrigger("atirar");

        yield return new WaitForSeconds(0.2f); // sincroniza com animação
        

        Transform pontoAtaque = (direcao == DirecaoMovimento.Direita)
            ? pontoAtaqueDireita
            : pontoAtaqueEsquerda;

        Instantiate(municao, pontoOrigem, Quaternion.identity);

        Collider2D colliderPlayer = Physics2D.OverlapCircle(pontoAtaque.position, raioAtaque, layersAtaque);
        if (colliderPlayer != null)
        {
            Player player = colliderPlayer.GetComponentInParent<Player>();
            if (player != null)
                player.ReceberDano(1);
        }

        yield return new WaitForSeconds(0.3f); // cooldown rápido
        atacando = false;
    }

    private void OnDrawGizmos()
    {
        if (pontoAtaqueDireita != null)
            Gizmos.DrawWireSphere(pontoAtaqueDireita.position, raioAtaque);
        if (pontoAtaqueEsquerda != null)
            Gizmos.DrawWireSphere(pontoAtaqueEsquerda.position, raioAtaque);
    }
}
