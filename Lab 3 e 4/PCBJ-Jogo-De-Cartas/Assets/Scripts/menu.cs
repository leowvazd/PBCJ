using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class menu : MonoBehaviour
{
    public string cena;

    public GameObject optionsPanel;

    public void StartGame()             // função responsável por inicialiar o cena lab3 do jogo só com as pretas
    {
        PlayerPrefs.SetInt("gameMode", 0);
        SceneManager.LoadScene("Lab3");
    }

    public void RestartGame()             // função responsável por reinicializar o jogo
    {
        int gm = PlayerPrefs.GetInt("gameMode");
        if(gm != 3)
            SceneManager.LoadScene("Lab3");
        else
            SceneManager.LoadScene("Jogo Sobrevivencia");
    }

    public void StartMenu()             // função responsável por inicialiar o menu
    {
        
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()             // função responsável por fechar o jogo
    {
        Debug.Log("Fechando o jogo, obrigado por jogar! :D ");

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();              
    }

    public void StartRedGame()              // função responsável por inicialiar o jogo com o modo de game só com as vermelhas
    {
        PlayerPrefs.SetInt("gameMode", 1);
        SceneManager.LoadScene("Lab3");
    }

    public void StartBigGame()              // função responsável por inicialiar o jogo com o modo de game com todas as cartas (pretas e vermelhas)
    {
        PlayerPrefs.SetInt("gameMode", 2);
        SceneManager.LoadScene("Lab3");
    }

    public void StartSurvivorGame()              // função responsável por inicialiar o jogo Sobrevivência!
    {
        PlayerPrefs.SetInt("gameMode", 3);
        SceneManager.LoadScene("Jogo Sobrevivencia");
    }

    public void StartDiffNaipes()
    {
        PlayerPrefs.SetInt("gameMode", 4);
        SceneManager.LoadScene("Lab3");
    }

    // Update is called once per frame
    void Update()        // função responsável por atualizar frame a frame por jogo  
    {
        
    }

    public void ShowOptions()       // função que puxa o menu de opções 
    {
       optionsPanel.SetActive(true);       
    }

    public void BackToMenu()        // função que volta para o menu de opções 
    {
        optionsPanel.SetActive(false); 
    }

    public void Creditos()          // função que carrega a cena de creditos
    {
        SceneManager.LoadScene("end"); 
    }

}
 