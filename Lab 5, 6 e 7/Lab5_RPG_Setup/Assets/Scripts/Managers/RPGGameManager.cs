using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    public static RPGGameManager instanciaCompartilhada = null;                         // instancia da câmera compartilhada
    public RPGCameraManager cameraManager;                                              // armazena o objeto câmera

    public PontoSpawn playerPontoSpawn;                                                 // armazena o ponto de spawn do player                      

    private void /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary> 
    Awake()
    {
        if (instanciaCompartilhada != null && instanciaCompartilhada != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instanciaCompartilhada = this;
        }
    }

    void /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    Start()
    {
        SetupScene();
    }

    public void /// <summary>
    /// Classe que chama a função de SpawnPlayer
    /// </summary>
    SetupScene()
    {
        SpawnPlayer();
    }

    public void /// <summary>
    /// Classe que inicializa o objeto do Player no ponto Spawn
    /// </summary>
    SpawnPlayer() 
    {
        if (playerPontoSpawn != null)
        {
            GameObject player = playerPontoSpawn.SpawnO();
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }
    void /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    Update()
    {
        
    }
}
