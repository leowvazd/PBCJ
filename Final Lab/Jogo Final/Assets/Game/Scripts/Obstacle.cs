using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsavel pela configuração dos objetos
/// </summary>
public class Obstacle : MonoBehaviour
{
    private Rigidbody2D objRB; // defino minha classe objRB para privado

    public GameConfiguration config; // defino meu config para public

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        objRB = gameObject.GetComponent<Rigidbody2D>(); // meu objrb passa a ser um Rigidbody2D
    }
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        objRB.velocity = new Vector2(-config.speed, 0f); // defino a velocidade do objRB
    }
}
