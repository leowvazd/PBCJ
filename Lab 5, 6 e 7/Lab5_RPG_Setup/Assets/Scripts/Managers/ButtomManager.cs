using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtomManager : MonoBehaviour
{

    public void ReiniciaJogo(){

        SceneManager.LoadScene("Tela 1");

    }

    public void TelaCreditos(){

        SceneManager.LoadScene("Tela Creditos");

    }

    public void CloseGame(){

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();

    }

}
