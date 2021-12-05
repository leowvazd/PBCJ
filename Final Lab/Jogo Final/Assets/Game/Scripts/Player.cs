using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Resposavel pelo objeto player do jogo
/// </summary>
public class Player : MonoBehaviour
{
    public UnityEvent onPlayerHitted;    // defino a classe  onPlayerHitted como public 

    public Transform startPlayerPosition; // defino a classe startPlayerPosition como public 
    private Rigidbody2D plyRB; // defino a classe plyRB como private
    private Animator animator; // defino a classe  Animator como private
    private bool canJump; // defino a classe canjump como uma variavel booleana
    
    private bool isEnabled; // defino a classe isEnabled como booleana

    public float jumpFactor = 5f; // defino a classe jumpFactor como public  e float
    public float forwardFactor = 0.2f; // defino a classe forwardFactor como public e float

    public float forwardForce = 0f;  // defino a classe forwardForce como public e float

    public AudioSource jumping; // defino a classe audiosource como public 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        plyRB = gameObject.GetComponent<Rigidbody2D>(); // habilito forças físicas a atuarem sobre plyRB
        animator = gameObject.GetComponent<Animator>();  // habilito animações no meu animator
        canJump = true; // defino a variavel de pular ligada 
        isEnabled = false; // defino a variavel de pular desligada 

        float camWidth = Camera.main.orthographicSize * Camera.main.aspect; // defino float no meu volume de visualização da câmera ortográfica
        startPlayerPosition.localPosition = new Vector3 (gameObject.transform.localScale.x - camWidth, startPlayerPosition.localPosition.y, startPlayerPosition.localPosition.z); // defino a posição do jogador

        gameObject.transform.localPosition = startPlayerPosition.localPosition; // mudo meu gameObject.transform.localPosition para startPlayerPosition.localPosition

        jumping = GetComponent<AudioSource>(); // defino um som para minha variavel de pulo
    }

    /// <summary>
    /// Responsavel por ativar a animaçao e o jump do objeto jogador
    /// </summary>
    public void SetActive()
    {
        isEnabled = true; //  defino a variavel de pular desligada 
        canJump = true; // defino a variavel de pular ligada 
        animator.Play("player_running"); // animação do meu jogador correndo
        plyRB.constraints = RigidbodyConstraints2D.FreezeRotation; // desligo a rotação do meu rigidbody

        gameObject.transform.localPosition = startPlayerPosition.localPosition; // mudo meu gameObject.transform.localPosition para startPlayerPosition.localPosition
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))// se apertar o espaço
        {
            Jump(); // o dinossauro pula
        }
    }

    /// <summary>
    /// Responsavel pela configuração do pulo do player
    /// </summary>
    void Jump()
    {
        if (!isEnabled) return; // se tiver desligado volto ao começo do codigo
        if (canJump) // se pular 
        {
            canJump = false; // jogo minha variavel para 0
            jumping.Play(); // toca a musica de pulo

            if (transform.position.x < 0) // se a posição de x > 0
            {
                forwardForce = forwardFactor; // troco  forwardForce = forwardFactor
            }

            else
            {
                forwardForce = 0f; // jogo a posição da força para 0
            }

            plyRB.velocity = new Vector2 (forwardForce, jumpFactor); // defino a velociadde do dinossauro 
            animator.Play("player_jumping"); //animação do meu jogador pulando
        }

    }

    /// <summary>
    /// Responsavel por setar a colisão do player
    /// </summary>
    private void OnCollisionEnter2D(Collision2D collision) //defino a classe de colisao 
    {
        if (!isEnabled) return;  // se tiver desligado volto ao começo do codigo
        if (collision.gameObject.tag == "Obstacle") // se meu dinossauro tiver um obstaculo
        {
            plyRB.constraints = RigidbodyConstraints2D.FreezeAll; // introduzo a fisica de colisao 
            onPlayerHitted.Invoke(); // se meu dinossauro tiver contato com o objeto invoco a classe  onPlayerHitted
            animator.Play("player_hurt"); // animação do meu dinossauro com colisão
            isEnabled = false; // jogo isEnabled para falso  
            onPlayerHitted.Invoke();// se meu dinossauro tiver contato com o objeto invoco a classe  onPlayerHitted
        }

        else
        {
            animator.Play("player_running"); // animação do meu dinossauro correndo
        }

        canJump = true; // canjump para verdadeiro 
        
    }
}
