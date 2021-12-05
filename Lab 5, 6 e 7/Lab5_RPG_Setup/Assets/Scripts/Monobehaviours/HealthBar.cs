using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PontosDano pontosDano;       // Objeto de leitura dos dados de quantos pontos tem o Player
    public Player caractere;            // receberá o objeto do Player
    public Image medidorImagem;          // recebe a barra de medição

    public Text pdTexto;                // recebe os dados de PD 

    float maxPontosDano;                // armazena a quantidade limite de "saúde" do Player
    
    void /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    Start()
    {
        maxPontosDano = caractere.MaxPontosDano; 
    }
    void /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    Update()
    {
        if (caractere != null)
        {
            medidorImagem.fillAmount = pontosDano.valor / maxPontosDano;
            pdTexto.text = "PD: " + (medidorImagem.fillAmount *100);
        }
    }
}
