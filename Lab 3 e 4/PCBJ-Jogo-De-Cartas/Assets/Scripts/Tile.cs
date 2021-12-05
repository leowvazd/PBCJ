using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile : MonoBehaviour
{
    private bool tileRevelada = false;      // indicador da carta virada ou não
    public Sprite originalCarta;            // sprite da carta desejada
    public Sprite backCarta;                // sprite do avesso da carta
    public Sprite novaCarta;                // update da carta

    // Start is called before the first frame update
    void Start()
    {
        EscondeCarta();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        print("Você pressinou num Tile");
        /* if (tileRevelada)
            EscondeCarta();
        else
            RevelaCarta(); */       // aqui não se guardava o número de cartas
        GameObject.Find("gameManager").GetComponent<ManageCartas>().CartaSelecionada(gameObject);
    }

    public void EscondeCarta()
    {
        GetComponent<SpriteRenderer>().sprite = backCarta;
        tileRevelada = false;
    }

    public void RevelaCarta()
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        tileRevelada = true;
    }

    public void setCartaOriginal(Sprite novaCarta)
    {
        originalCarta = novaCarta;
    }
}
