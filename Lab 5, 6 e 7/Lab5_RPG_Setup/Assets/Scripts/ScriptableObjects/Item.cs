using UnityEngine;

[CreateAssetMenu(menuName = "Item")]

public class Item : ScriptableObject
{
    public string NomeObjeto;                   // armazena o nome do objeto-item
    public Sprite sprite;                       // sprite do objeto-item
    public int quantidade;                      // int para quantidade do objeto-item
    public bool empilhavel;                     // bool para determinar se o objeto-item é empilhavél ou não

    public enum 
    /// <summary>
    /// Tipos diferentes de itens in-game
    /// </summary>
    TipoItem
    {
        MOEDA,
        HEALTH,
        CRISTAL_AZUL,
        CRISTAL_PRATA,
        CRISTAL_VERDE,
        CRISTAL_VERMELHO
    }

    public TipoItem tipoItem;                   // armazena o diferente tipo de item
}
