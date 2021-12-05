using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    public float VelocidadeMovimento = 3.0f;            // equivale ao momento (impulso) a ser dado ao player
    Vector2 Movimento = new Vector2();                  // detectar movimento pelo teclado
    Animator animator;                                  // guarda a componente do Controlador de Animação

    // string estadoAnimacao = "EstadoAnimacao";        // guarda o nome do parametro de animação        // Desnecessario com a Blend Tree (andar tree)
    Rigidbody2D rb2D;                                   // guarda a componente CorpoRígido do Player
    
    /* enum EstadosCaractere                        // Desnecessario pela Blend Tree (Andar Tree)
    {
        andaLeste = 1,
        andaOeste = 2,
        andaNorte = 3,
        andaSul = 4,
        idle = 5
    } */

    void /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    void /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    Update()
    {
        UpdateEstado();
    }

    private void /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    FixedUpdate()
    {
        MoveCaractere();
    }

    private void 
    /// <summary>
    /// Responsável pelo movimento do player
    /// </summary>
    MoveCaractere()
    {
        Movimento.x = Input.GetAxisRaw("Horizontal");
        Movimento.y = Input.GetAxisRaw("Vertical");
        Movimento.Normalize();
        rb2D.velocity = Movimento * VelocidadeMovimento;
    }

    void 
    /// <summary>
    /// Atuaização do frame estado do jogo
    /// </summary>
    UpdateEstado()
    {
        if (Mathf.Approximately(Movimento.x, 0) && (Mathf.Approximately(Movimento.y, 0)))
        {
            animator.SetBool("Caminhando", false); 
        }
        else
        {
            animator.SetBool("Caminhando", true);
        }
        animator.SetFloat("dirX", Movimento.x);
        animator.SetFloat("dirY", Movimento.y);
    }

    // UpdateEstado antigo (sem a Blend Tree)
    /*
    private void UpdateEstado()
    {
        if (Movimento.x > 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaLeste);
        }

        else if (Movimento.x < 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaOeste);
        }

        else if (Movimento.y > 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaNorte);
        }

        else if (Movimento.y < 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaSul);
        }

        else
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.idle);
        }
    }
    */
}
