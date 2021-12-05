using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]

public class Perambular : MonoBehaviour
{
    public float velocidadePerseguicao;                     // velocidade do "Inimigo" na area de spot
    public float velocidadePerambular;                      // velocidade do "Inimigo" passeando
    float velocidadeCorrente;                               // velocidade do "Inimigo" atribuída

    public float intervaloMudancaDirecao;                   // tempo para alterar a direção

    public bool perseguePlayer;                             // indicador de perseguidor ou não

    Coroutine MoverCoroutine;                               // Coroutine Mover

    Rigidbody2D rb2D;                                       // armazena o componente rigidbody2D
    Animator animator;                                      // armazena o componente Animator

    Transform alvoTransform = null;                         // armazena o componente Transform do Alvo

    Vector3 posicaoFinal;                                   // Vetor da posição final 
    float anguloAtual = 0;                                  // Angulo atribuido

    CircleCollider2D circleCollider;                        // Armazena o componente de Spot

    void /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    Start()
    {
        animator = GetComponent<Animator>();
        velocidadeCorrente = velocidadePerambular;
        rb2D = GetComponent<Rigidbody2D>();
        StartCoroutine(RotinaPerambular());
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void 
    /// <summary>
    /// Responsavel pelas linhas guias do Gizmos
    /// </summary>
    OnDrawGizmos()
    {
        if (circleCollider != null)
        {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    } 

    public IEnumerator 
    /// <summary>
    /// Rotina de movimento da IA do inimigo
    /// </summary>
    RotinaPerambular()
    {
        while (true)
        {
            EscolheNovoPontoFinal();
            if (MoverCoroutine != null)
            {
                StopCoroutine(MoverCoroutine);
            }
            MoverCoroutine = StartCoroutine(Mover(rb2D, velocidadeCorrente));
            yield return new WaitForSeconds(intervaloMudancaDirecao);
        }
    }

    void 
    /// <summary>
    /// Responsável por escolher um novo ponto final aleatorio para o trajeto do inimigo
    /// </summary>
    EscolheNovoPontoFinal()
    {
        anguloAtual += Random.Range(0, 360);
        anguloAtual = Mathf.Repeat(anguloAtual, 360);
        posicaoFinal += Vector3ParaAngulo(anguloAtual);
    }

    Vector3 
    /// <summary>
    /// Calculo do angulo do vetor
    /// </summary>
    Vector3ParaAngulo(float anguloEntradaGraus)
    {
        float anguloEntradaRadianos = anguloEntradaGraus * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(anguloEntradaRadianos), Mathf.Sin(anguloEntradaRadianos), 0);
    }

    public IEnumerator 
    /// <summary>
    /// Responsável pelo movimento da IA do inimigo
    /// </summary>
    Mover (Rigidbody2D rBParaMover, float velocidade)
    {
        float distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;
        while (distanciaFaltante > float.Epsilon)
        {
            if (alvoTransform != null)
            {
                posicaoFinal = alvoTransform.position;
            }
            if (rBParaMover != null)
            {
                animator.SetBool("Caminhando", true);
                Vector3 novaPosicao = Vector3.MoveTowards(rBParaMover.position, posicaoFinal, velocidade * Time.deltaTime);
                rb2D.MovePosition(novaPosicao);
                distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("Caminhando", false);
    }

    private void /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
    OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && perseguePlayer)
        {
            velocidadeCorrente = velocidadePerseguicao;
            alvoTransform = collision.transform.gameObject.transform;
            if (MoverCoroutine != null)
            {
                StopCoroutine(MoverCoroutine);
            }
            MoverCoroutine = StartCoroutine(Mover(rb2D, velocidadeCorrente));
        }
    }

    private void /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
    OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Caminhando", false);
            velocidadeCorrente = velocidadePerambular;
            if(MoverCoroutine != null)
            {
                StopCoroutine(MoverCoroutine);
            }
            alvoTransform = null;
        }
    }
    void /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    Update()
    {
        Debug.DrawLine(rb2D.position, posicaoFinal, Color.red);
    }
}
