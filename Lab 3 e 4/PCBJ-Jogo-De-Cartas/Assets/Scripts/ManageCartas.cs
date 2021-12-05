using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageCartas : MonoBehaviour
{
    public GameObject carta;        // a carta a ser descartada
    public GameObject azul;
    private bool primeiraCartaSelecionada, segundaCartaSelecionada;         // indicadores para cada escolhida em cada linha
    private GameObject carta1, carta2;      // gameObjects da 1ª 2 ª carta selecionada
    private string linhaCarta1, linhaCarta2;        // linha da carta selecionada
    bool timerPausado, timerAcionado;       // indicador de pause no Timer ou Start Timer
    float timer;        // variável de tempo
    int numTentativas = 0;      // numero de tentativas na rodada
    int numAcertos = 0;         // numero de match de pares acertados
    AudioSource somOK;      // som de acerto
    int ultimoJogo = 0;
    int score = 0;
    int gameMode;

    string [] naipesVermelhos = new string [] {"_of_hearts", "_of_diamonds"};       // aqui é um arrray dos naipes vermelhos
    string [] naipesPretos = new string [] {"_of_spades", "_of_clubs"};         // aqui é um array dos naipes pretos
    string [] todosNaipes = new string [] {"_of_diamonds","_of_spades", "_of_hearts", "_of_clubs"};     // aqui é um array de todas as cartas de todos os naipes
    string [] diffNaipes = new string [] {"_of_hearts", "_of_hearts"};           // aqui é um array de mesmo naipe de diferentes baralhos

    string [] survivor = new string [] {"PD", "P"};   // aqui é um array com todos os animais do Sobrevivencia 

    int indexSurvivor = 0;
    int indexNaipe = 0;

    // Start is called before the first frame update
    void Start() // função que inicializa o jogo
    {
        Debug.Log("Modo de jogo: "+ PlayerPrefs.GetInt("gameMode"));

        gameMode = PlayerPrefs.GetInt("gameMode");

        if (gameMode < 2 || gameMode == 3 || gameMode == 4)
            MostraCartas();
        else
            MostraTodasCartas();

        UpDateTentativas();
        upDateScore();
        somOK = GetComponent<AudioSource>();
        ultimoJogo = PlayerPrefs.GetInt("Jogadas", 0);   
        GameObject.Find("ultimaJogada").GetComponent<Text>().text = "Jogo Anterior = " + ultimoJogo;  
    }

    // Update is called once per frame
    void Update() // função que da update no jogo a cada frame
    {
        if (timerAcionado)
        {
            timer += Time.deltaTime;
            print(timer);
            if (timer>1)
            {
                timerPausado = true;
                timerAcionado = false;
                if (carta1.tag == carta2.tag && carta1 != carta2 && gameMode != 3) // Logica que verifica se o player selecionou duas cartas com a mesma tag
                {
                    Destroy(carta1);
                    Destroy(carta2);
                    numAcertos++;
                    score++;                 // Faltava aumentar o score qndo o player encontrar um par
                    upDateScore();           // Adcionado chama de func upDateScore para atualizar score qndo jogado encontra um par
                    Debug.Log("Encontrou um par!");
                    somOK.Play();
                    if (numAcertos == 13)
                    {
                        PlayerPrefs.SetInt("Jogadas", numTentativas);
                        SceneManager.LoadScene("end"); //aqui coloquei para quando chegar no numero de acertos igual a 13 ele abrir a tela de end
                    }
                        
                }
                else if(gameMode == 3 && carta1.tag != carta2.tag)  // Logicas para o game Sobrevivencia!
                {
                    if(carta1.tag != carta2.tag)   // ganha ponto por achar par predador/presa
                    {
                        Destroy(carta1);
                        Destroy(carta2);
                        numAcertos++;
                        score++;                 
                        upDateScore();           
                        Debug.Log("Encontrou um par Predador/Presa!");
                        somOK.Play();
                        if (numAcertos == 6)   // Finaliza o game se alcançar 6 pontos no Sobrevivencia!
                        {
                            PlayerPrefs.SetInt("Jogadas", numTentativas);
                            SceneManager.LoadScene("end"); 
                        }
                    }
                    else if (carta1.tag == "predador")
                    {
                        Destroy(carta1);
                        Destroy(carta2);
                        numAcertos--;
                        score--;                 
                        upDateScore();           
                        Debug.Log("Encontrou um par Predador/Predador!");
                        somOK.Play();
                        
                    }
                    else
                    {
                        carta1.GetComponent<Tile>().EscondeCarta(); // aqui caso o jogador achar duas presas as cartas sao viradas e não é alterado o score
                        carta2.GetComponent<Tile>().EscondeCarta();
                    }
                }
                else
                {
                    carta1.GetComponent<Tile>().EscondeCarta(); // aqui caso o jogador erre em achar a "carta-par" esconde as cartas
                    carta2.GetComponent<Tile>().EscondeCarta();
                }
                primeiraCartaSelecionada = false;
                segundaCartaSelecionada = false;
                carta1 = null;
                carta2 = null;
                linhaCarta1 = "";
                linhaCarta2 = "";
                timer = 0;
            }
            //if (scorenovo < scoremaximo)  A IDEIA É CRIAR UM SCORE SALVAR O MAXIMO DELE , E FAZER A LOGICA AQUI 
        {
                
          // SceneManager.LoadScene("menu"); AI AQUI SE CRIA UMA CENA COM A TELA DE CAMPEAO 
        }


        }
        
    }

    void MostraCartas()
    {
        if(gameMode == 4)
        {
            int[] ArrayEmbaralhado = criaArrayEmbaralhadoDiffNaipes(); // cria-se dois arrays para embaralhas as cartas
            int[] ArrayEmbaralhado2 = criaArrayEmbaralhadoDiffNaipes();

            for (int i = 0; i < 13; i++)
            {
                // AddUmaCarta(i);
                // AddUmaCarta(i, ArrayEmbaralhado[i]);
                AddUmaCarta(0, i, ArrayEmbaralhado[i]);
                AddUmaCarta(1, i, ArrayEmbaralhado2[i]);
            }
        }
        else if(gameMode != 3)
        {
            int[] ArrayEmbaralhado = criaArrayEmbaralhado(); // aqui cria-se dois arrays para auxiliar
            int[] ArrayEmbaralhado2 = criaArrayEmbaralhado(); // ao embaralhar as cartas

            for (int i = 0; i < 13; i++)
            {
                // AddUmaCarta(i);
                // AddUmaCarta(i, ArrayEmbaralhado[i]);
                AddUmaCarta(0, i, ArrayEmbaralhado[i]);
                AddUmaCarta(1, i, ArrayEmbaralhado2[i]);
            }

        }
        else
        {
            int[] ArrayEmbaralhado = criaArrayEmbaralhadoSobrevivencia(); // Mesma coisa que o de cima, mas para o jogo Sobrevivencia
            int[] ArrayEmbaralhado2 = criaArrayEmbaralhadoSobrevivencia();

            for (int i = 0; i < 9; i++)
            {
                // AddUmaCarta(i);
                // AddUmaCarta(i, ArrayEmbaralhado[i]);
                AddUmaCarta(0, i, ArrayEmbaralhado[i]);
                AddUmaCarta(1, i, ArrayEmbaralhado2[i]);
            }
        }
        
    }

    void MostraTodasCartas()
    {
        int[] ArrayEmbaralhado = criaArrayEmbaralhado();    // novamente, aqui cria-se dois arrays para auxiliar
        int[] ArrayEmbaralhado2 = criaArrayEmbaralhado();   // ao embaralhar as cartas
        int[] ArrayEmbaralhado3 = criaArrayEmbaralhado();
        int[] ArrayEmbaralhado4 = criaArrayEmbaralhado();

        // Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity);
        // AddUmaCarta();

        for (int i = 0; i < 13; i++)
        {
            // AddUmaCarta(i);
            // AddUmaCarta(i, ArrayEmbaralhado[i]);
            AddUmaCarta(2, i, ArrayEmbaralhado[i]);
            AddUmaCarta(1, i, ArrayEmbaralhado2[i]);
            AddUmaCarta(0, i, ArrayEmbaralhado3[i]);
            AddUmaCarta(-1, i, ArrayEmbaralhado4[i]);

        }
    }
    void AddUmaCarta(int linha, int rank, int valor)
    {
        GameObject centro = GameObject.Find("centroDaTela");
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (650*escalaCartaOriginal)/110.0f;
        float fatorEscalaY   = (945*escalaCartaOriginal)/110.0f;

        Vector3 novaPosicao;
       // Vector3 novaPosicaoo;
        
        
        //novaPosicaoo = new Vector3(centro.transform.position.x + ((rank-13/2) * fatorEscalaX), centro.transform.position.y, centro.transform.position.z);
        //novaPosicao = new Vector3(centro.transform.position.x, centro.transform.position.y+ ((rank-13/2) * fatorEscalaY), centro.transform.position.z);
                
        if(gameMode != 3)
        {
                
            novaPosicao = new Vector3(centro.transform.position.x + ((rank-13/2) * fatorEscalaX), centro.transform.position.y + ((linha-2/2) * fatorEscalaY), centro.transform.position.z);
            
        }
        else
        {
            novaPosicao = new Vector3(centro.transform.position.x + ((rank-9/2) * fatorEscalaX), centro.transform.position.y + ((linha-2/2) * fatorEscalaY), centro.transform.position.z);
        }
        
        // GameObject c = (GameObject)(Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity));
        // GameObject c = (GameObject)(Instantiate(carta, new Vector3(rank*1.5f, 0, 0), Quaternion.identity));
        
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));
        // GameObject a = (GameObject)(Instantiate(azul, novaPosicao, Quaternion.identity)); // cartas azuis

        if(gameMode != 3)
        {
            c.tag = "" + (valor+1);
            c.name = "" + linha + "_" + valor;
           
        }                                                                                                                                               
        else
        {   

            c.tag = survivor[indexSurvivor];
            // c.name = "" + valor;
            c.name = survivor[indexSurvivor] + "_" + linha + "_" + valor;

            Debug.Log("Nome do C: " + valor + " = " + c.name);
        }

        
                
        string nomeDaCarta = "";
        string numeroCarta = "";

        /* if (rank == 0)
            numeroCarta = "ace";
        else if (rank == 10)
            numeroCarta = "jack";
        else if (rank == 11)
            numeroCarta = "queen";
        else if (rank == 12)
            numeroCarta = "king";
        else
            numeroCarta = "" + (rank+1); */         // else if para array ordenado no deck
        
        if (valor == 0)
            numeroCarta = "ace";
        else if (valor == 10)
            numeroCarta = "jack";
        else if (valor == 11)
            numeroCarta = "queen";
        else if (valor == 12)
            numeroCarta = "king";
        else
            numeroCarta = "" + (valor+1); 
        
        if (gameMode == 0)              // nesta parte, funciona o menu de opções e seus respectivos modos de jogo
        {
            nomeDaCarta = numeroCarta + naipesPretos[indexNaipe];
            indexNaipe++;
            if(indexNaipe >= 2)
                indexNaipe = 0;
        }
        else if(gameMode == 1)
        {
            nomeDaCarta = numeroCarta + naipesVermelhos[indexNaipe];
            indexNaipe++;
            if(indexNaipe >= 2)
                indexNaipe = 0;
        }
        else if(gameMode == 2)
        {
            nomeDaCarta = numeroCarta + todosNaipes[indexNaipe];
            indexNaipe++;
            if(indexNaipe >= 4)
                indexNaipe = 0;
        }
        else if(gameMode == 4)
        {
            nomeDaCarta = numeroCarta + diffNaipes[indexNaipe];
            indexNaipe++;
            if(indexNaipe >= 2)
                indexNaipe = 0;
        }
        
        else
        {
            nomeDaCarta = survivor[indexSurvivor] + (valor+1);
            Debug.Log("Nome da carta: "+ nomeDaCarta);
            indexSurvivor++;
            if(indexSurvivor >= 2)
                indexSurvivor = 0;
        }

        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        print("S1: " + s1);
        // GameObject.Find("" + rank).GetComponent<Tile>().setCartaOriginal(s1);
        // GameObject.Find("" + valor).GetComponent<Tile>().setCartaOriginal(s1);
        if(gameMode != 3)
        {
            GameObject.Find("" + linha + "_" + valor).GetComponent<Tile>().setCartaOriginal(s1);
        }
        else
        {
            GameObject.Find(c.name).GetComponent<Tile>().setCartaOriginal(s1);
        }
        
    }

    public int[] criaArrayEmbaralhado()
    {
        int[] novoArray = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        int temp;

        for (int t = 0; t < 13; t++)
        {
            temp = novoArray[t];
            int r = Random.Range(t, 13);
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;
        }

        return novoArray;
    }

    public int[] criaArrayEmbaralhadoSobrevivencia()
    {
        int[] novoArray = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        int temp;

        for (int t = 0; t < 9; t++)
        {
            temp = novoArray[t];
            int r = Random.Range(t, 9);
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;
        }

        return novoArray;
    }

    public int[] criaArrayEmbaralhadoDiffNaipes()
    {
        int[] novoArray = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
        int temp;

        for (int t = 0; t < 13; t++)
        {
            temp = novoArray[t];
            int r = Random.Range(t, 13);
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;
        }

        return novoArray;
    }
    
    public void CartaSelecionada(GameObject carta)
    {
        if (!primeiraCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta1 = linha;
            primeiraCartaSelecionada = true;
            carta1 = carta;
            carta1.GetComponent<Tile>().RevelaCarta();
        }
        else if (primeiraCartaSelecionada && !segundaCartaSelecionada)
        {
            string linha = carta.name.Substring(0, 1);
            linhaCarta2 = linha;
            segundaCartaSelecionada = true;
            carta2 = carta;
            carta2.GetComponent<Tile>().RevelaCarta();
            VerificaCartas();
        }
    }

    public void VerificaCartas()
    {
        DisparaTimer();
        numTentativas++;
        //score++;             
        UpDateTentativas();
        upDateScore();

    }

    public void DisparaTimer()      // função booleana responsavel por controlar o timer
    {
        timerPausado = false;
        timerAcionado = true;
    }

    void UpDateTentativas()         // função que atualiza o frame das tentativas
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tentativas = " + numTentativas;
    }
      void upDateScore()            // função que atualiza o frame do score
    {
        GameObject.Find("score").GetComponent<Text>().text = "Score = " + score;
        PlayerPrefs.SetInt("score", 0); 
    }

    

}
