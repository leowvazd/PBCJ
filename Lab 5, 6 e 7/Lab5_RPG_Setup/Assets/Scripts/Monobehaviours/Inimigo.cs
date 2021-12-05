using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : Caractere
{
    float pontosVida;                       // equivalente à saúde do inimigo
    public int forcaDano;                   // poder de dano
    
    Coroutine danoCoroutine;                // Coroutine do dano

    void /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    Start()
    {
        
    }
    private void 
    /// <summary>
    /// Responsável pelo reset do player
    /// </summary>
    OnEnable()
    {
        ResetCaractere();
    }

    void /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="collision">The Collision2D data associated with this collision.</param>
    OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (danoCoroutine == null)
            {
                // Debug.Log("Hit Player Coroutine");
                danoCoroutine = StartCoroutine(player.DanoCaractere(forcaDano, 1.0f));
                
            }

        }
    }

    void /// <summary>
    /// Sent when a collider on another object stops touching this
    /// object's collider (2D physics only).
    /// </summary>
    /// <param name="collision">The Collision2D data associated with this collision.</param>
    OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (danoCoroutine != null)
            {
                StopCoroutine(danoCoroutine);
                danoCoroutine = null;
            }
        } 
    }

    public override IEnumerator 
    /// <summary>
    /// IEnumerator responsável pelo dano do player
    /// </summary>
    DanoCaractere(int dano, float intervalo)
    {
        while(true) 
        {
            StartCoroutine(FlickerCaractere());
            pontosVida = pontosVida - dano;
            if (pontosVida <= float.Epsilon)
            {
                KillCaractere();                 
                break;
            }
            
            if (intervalo > float.Epsilon)
            {
                yield return new WaitForSeconds(intervalo);
            }

            else
            {
                break;
            }
        }
    }

    public override void 
    /// <summary>
    /// Classe override que reseta o player com a vida inicial
    /// </summary>
    ResetCaractere()
    {
        pontosVida = inicioPontosDano;
    }

    void /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    Update()
    {
        
    }
}
