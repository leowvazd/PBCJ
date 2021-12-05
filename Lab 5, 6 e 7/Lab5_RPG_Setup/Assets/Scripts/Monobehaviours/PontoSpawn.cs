using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontoSpawn : MonoBehaviour
{
    public GameObject prefabParaSpawn;                              // prefeb do Spawn
    
    public float intervaloRepeticao;                                // int do intervalo de spawn do inimigo
    
    public void /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    Start()
    {
        if (intervaloRepeticao > 0)
        {
            InvokeRepeating("SpawnO", 0.0f, intervaloRepeticao);
        }

    }

    public GameObject 
    /// <summary>
    /// Objeto do Spawn
    /// </summary>
    SpawnO()
    {
        if (prefabParaSpawn != null)
        {
            return Instantiate (prefabParaSpawn, transform.position, Quaternion.identity);
        }

        return null;
    }

    // Update is called once per frame
    void /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    Update()
    {
        
    }
}
