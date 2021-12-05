using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Caractere
{
    public Inventario inventarioPrefab;             // referencia ao objeto prefab criado do inventario
    Inventario inventario;                          // declaração do inventário
    public HealthBar healthBarPrefab;               // referencia ao objeto prefab criado da HealthBar
    HealthBar healthBar;
    public PontosDano pontosDano;                   // tem o valor da "saúde" do objeto
    public AudioSource weapon;                      // som do click da arma

    bool playerWin=false;

    int qtdItensInventário=0;
    
    private void /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    Start()
    {
        inventario = Instantiate(inventarioPrefab);
        
        pontosDano.valor = inicioPontosDano;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;

        weapon = GetComponent<AudioSource>();
    }

    public override IEnumerator 
    /// <summary>
    /// IEnumerator responsável pelo dano do player
    /// </summary>
    DanoCaractere(int dano, float intervalo)
    {
        while (true)
        {
            StartCoroutine(FlickerCaractere());
            pontosDano.valor = pontosDano.valor - dano;
            if (pontosDano.valor <= float.Epsilon)
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
    /// Reinicia o objeto player e seu inventario e vida
    /// </summary>
    ResetCaractere()
    {
        inventario = Instantiate(inventarioPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        pontosDano.valor = inicioPontosDano;
    }

    public override void 
    /// <summary>
    /// Responsável por "matar" o objeto player após morrer
    /// </summary>
    KillCaractere()
    {
        base.KillCaractere();
        Destroy(healthBar.gameObject);
        Destroy(inventario.gameObject);

        SceneManager.LoadScene("Tela Dead");
    }

    private void /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
    OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coletavel"))
        {
            Item DanoObjeto = collision.gameObject.GetComponent<Consumable>().item;
            if (DanoObjeto != null)
            {
                bool DeveDesaparecer = false; 
                // print ("Acertou" + DanoObjeto.NomeObjeto);

                switch (DanoObjeto.tipoItem)
                {
                    case Item.TipoItem.MOEDA:
                        // DeveDesaparecer = true;
                        DeveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;
                    
                    case Item.TipoItem.HEALTH:
                        DeveDesaparecer = AjustePontosDano(DanoObjeto.quantidade);
                        break;
                    
                    case Item.TipoItem.CRISTAL_AZUL:
                        // DeveDesaparecer = true;
                        DeveDesaparecer = inventario.AddItem(DanoObjeto); 
                        break;

                    case Item.TipoItem.CRISTAL_PRATA:
                        // DeveDesaparecer = true;
                        DeveDesaparecer = inventario.AddItem(DanoObjeto); 
                        break;
                    
                    case Item.TipoItem.CRISTAL_VERDE:
                        // DeveDesaparecer = true;
                        DeveDesaparecer = inventario.AddItem(DanoObjeto); 
                        break;
                    
                    case Item.TipoItem.CRISTAL_VERMELHO:
                        // DeveDesaparecer = true;
                        DeveDesaparecer = inventario.AddItem(DanoObjeto); 
                        break;

                    default: 
                        break; 
                }
                if (DeveDesaparecer)
                {

                    if(DanoObjeto.tipoItem != Item.TipoItem.HEALTH){

                        playerWin = IncrementaQtdInventario(1);

                    }
                    
                    if(playerWin){

                        ProximaFase();

                    }

                    collision.gameObject.SetActive(false);
                }
            }            
        }
    }

    public bool 
    /// <summary>
    /// Responsável pelo ajuste do PontosDano
    /// </summary>
    AjustePontosDano (int quantidade)
    {
        if (pontosDano.valor < MaxPontosDano)
        {
            pontosDano.valor = pontosDano.valor + quantidade;
            print ("Ajuste PD por: " + quantidade + ". Novo Valor = " + pontosDano.valor);
            return true;
        }

        else return false;     
    }

    bool IncrementaQtdInventario(int quantidade=1){

        qtdItensInventário += quantidade;

        if (qtdItensInventário >= 25) return true;   // Parametro correto = 25

        return false;

    }

    void ProximaFase(){


        string sceneAtual = SceneManager.GetActiveScene().name;

        if(sceneAtual == "Tela 1"){    // Se o jogador estiver na Tela 1, carrega a Tela 2
            
            // Resetar o inventario!
            SceneManager.LoadScene("Tela 2");

        }
        else{

            SceneManager.LoadScene("Tela Win");

        }

    }

}
