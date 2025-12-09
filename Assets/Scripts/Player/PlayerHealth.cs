using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Configuração de Vida")]
    public int maxHealth = 4;
    public int currentHealth; //em relação ao 4

    public BarraVida barraVida;

    public int MaxVidaPlayer;
    

    
    void Start()
    {
        currentHealth = maxHealth;

        if (barraVida != null)
            barraVida.AtualizarBarra(currentHealth);
    }

    public void ReceberDano(int vidaAtual)
    {
        int convertido = (int) (maxHealth * vidaAtual / MaxVidaPlayer);

        Debug.Log(convertido);

        currentHealth = convertido;
        if (currentHealth < 0)
            currentHealth = 0;

        if (barraVida != null)
            barraVida.AtualizarBarra(currentHealth);

        if (currentHealth <= 0)
            Morrer();
    }

    private void Morrer()
    {
        Debug.Log("O jogador morreu!");
    }
}

