using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsavel pelo gerador dos objetos (inimigos)
/// </summary>
public class ObstacleGenerator : MonoBehaviour
{
    public GameObject[] obstacles;  // defino a classe obstacles como public 

    public List<Obstacle> obstaclesToSpawn; // definindo a classe obstaclestospawn como public 

    public GameConfiguration config; // definindo a classe config como public 

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        InitObstacles(); // ligo obstaculos

        float camWidth = Camera.main.orthographicSize * Camera.main.aspect; // defino camwidth como uma variavel float e faço multiplicação da camera orthographicSize com a aspect
        gameObject.transform.localPosition = new Vector3 ( camWidth, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);    // defino posição da minha camera 
    }

    
    /// <summary>
    /// Responsavel por iniciar a rotina do gerador
    /// </summary>
    public void GenerateObstacles() // aqui eu spwano obstaculos de forma aleatoria
    {
        StartCoroutine(SpawnRandomObstacles()); //  spwano os obstaculos
    }

    /// <summary>
    /// Responsavel por parar a rotina do gerador
    /// </summary>
    public void StopGenerator() // para obstaculos
    {
        StopAllCoroutines(); // paro tudo
    }

    /// <summary>
    /// Responsavel por reiniciar o gerador
    /// </summary>
    public void ResetGenerator() // reseto  obstaculos
    {
        foreach (Obstacle obstacle in obstaclesToSpawn) // para cada obstaculo dentro do obstaculos spwanados 
        {
            obstacle.gameObject.SetActive(false); // meu obstaculo vira falso
        }
    }

    /// <summary>
    /// Responsavel pela inicialização dos objetos
    /// </summary>
    void InitObstacles()
    {
        int index = 0; // jogo meu index para 0
        for (int i =0; i < obstacles.Length * 3; i++) // para cada (i=0 , adiciono meu tamanho de obstaculo x 3)
        {
            GameObject obj = Instantiate(obstacles[index], transform.position, Quaternion.identity); // muda a posição dos obstaculos
            obj.SetActive(false); // jogo para falso
            obstaclesToSpawn.Add(obj.GetComponent<Obstacle>()); // spawn os obstaculos

            index++;// jogo +1 para o contador 

            if (index == obstacles.Length) // se meu index for igual ao meu tamanho do obstaculo
            {
                index = 0; // defino como 0
            }
        }
    }

    /// <summary>
    /// Responsavel por randomizar o spawn dos inimigos
    /// </summary>
    IEnumerator SpawnRandomObstacles() 
    {
        yield return new WaitForSeconds(Random.Range(config.minRangeObstacleGenerator, config.maxRangeObstacleGenerator)); // Suspende a execução da co-rotina por um determinado período de segundos usando o tempo escalado.


        int index = Random.Range(0, obstaclesToSpawn.Count); // jogo meu indexiador para um tempo random 

        while (true) // se for verade 
        {
            Obstacle obstacle = obstaclesToSpawn[index]; // meu obstaculo passa a valer obstaclesToSpawn[index]

            if (!obstacle.gameObject.activeInHierarchy) // se obstacle.gameObject.activeInHierarchy
            {
                obstacle.gameObject.SetActive(true); // transformo para verdadeiro
                obstacle.transform.position = transform.position; // mudo a posição
                break; // para tudo
            }

            else
            {
                index = Random.Range(0, obstaclesToSpawn.Count); // jogo meu index para um tempo random
            }
            
        }
        
        StartCoroutine(SpawnRandomObstacles());// executo a função
    }
}
