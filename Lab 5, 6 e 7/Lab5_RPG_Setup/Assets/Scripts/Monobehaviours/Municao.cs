using UnityEngine;

public class Municao : MonoBehaviour
{
    public int danoCausado;                                         // poder de dano da munição

    private void /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
    OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            Inimigo inimigo = collision.gameObject.GetComponent<Inimigo>();
            StartCoroutine(inimigo.DanoCaractere(danoCausado, 0.0f));
            gameObject.SetActive(false);
        }
    }

    void /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    Start()
    {
        
    }

    // Update is called once per frame
    void /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    Update()
    {
        
    }
}
