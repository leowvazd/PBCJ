using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract indica que a classe não pode ser instanciada, e sim, herdada
public abstract class Caractere : MonoBehaviour
{
    // public int PontosDano;       // versão anterior do valor de "dano"
    
    // public int MaxPontosDano;    // versão anterior do valor max. de "dano"
    public float inicioPontosDano;  // valor minimo de "saúde" do player
    public float MaxPontosDano;     // valor max. permitido de "saúde" do player

    public abstract void ResetCaractere(); // função responsável pelo reset do player

    public virtual IEnumerator 
    /// <summary>
    /// Responsável pelo Flicker do Player
    /// </summary>
    FlickerCaractere()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public abstract IEnumerator DanoCaractere(int dano, float intervalo);   // Classe abstrata do danoa do caractere
    
    public virtual void 
    /// <summary>
    /// Chamada para destruir o objeto do player pós "morte"
    /// </summary>
    KillCaractere()
    {
        Destroy(gameObject);
    }   
}