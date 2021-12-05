using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsavel pela animação do background
/// </summary>
public class BackgroundTextureAnimator : MonoBehaviour // defino a classe como public
{
    private Material mat; // defino mat como privado
    private Vector2 offset; // defino offset como privador 

    public float factor = 40f; // defino factor como public  e float 

    public GameConfiguration config; // defino config como public
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        mat = GetComponent<Renderer>().material; // defino mat como  um renderizador que tem a função de fazer um objeto aparecer na tela.
        offset = mat.GetTextureOffset("_MainTex"); // defino meu offset sendo mat.GetTextureOffset("_MainTex")
    }

    // Update is called once per frame
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        offset.x = offset.x + ((config.speed / factor) * Time.deltaTime);  //defino a velocidade e faço uma multiplicação com meu tempo
        mat.SetTextureOffset("_MainTex", offset); // executo meu mat
    }
}
