using UnityEngine;
using System.Collections;

public class vidasSoldier : MonoBehaviour
{
    [SerializeField] private int vida = 4;
    private Animator anim;
    private bool morreu = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ReceberDano()
    {
        if (morreu) return;

        vida--;

        Debug.Log("Soldier tomou dano! Vida restante: " + vida);

        if (vida <= 0)
        {
            StartCoroutine(Morrer());
        }
    }

    private IEnumerator Morrer()
    {
        morreu = true;

        anim.SetTrigger("die");

        // Espera até a animação realmente começar
        yield return new WaitUntil(() =>
            anim.GetCurrentAnimatorStateInfo(0).IsName("die")
        );

        // Duração REAL da animação
        float duracao = anim.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(duracao);

        Destroy(gameObject);
    }
}

