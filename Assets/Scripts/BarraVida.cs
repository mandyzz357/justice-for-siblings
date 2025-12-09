using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    [Header("Sprites da barra (0 = morto, último = cheio)")]
    public Sprite[] lifeSprites;

    private Image uiImage;

    void Awake()
    {
        uiImage = GetComponent<Image>();
    }

    public void AtualizarBarra(int vidaAtual)
    {
        // Garante que o índice nunca passa do tamanho do array
        vidaAtual = Mathf.Clamp(vidaAtual, 0, lifeSprites.Length - 1);

        // Troca o sprite da barra
        uiImage.sprite = lifeSprites[vidaAtual];
    }
}

