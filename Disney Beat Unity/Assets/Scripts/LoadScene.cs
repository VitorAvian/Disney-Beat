using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//Script para controlar as trocas de cenas e pausas
public class LoadScene : MonoBehaviour
{
	//Variavel para controlar o botão de pausa
	private bool paused;

	void Start()
    {
        paused = false;
    }

    //Função para trocar as cenas no clique dos botões
    public void SceneLoader(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

    //Botão para sair
    public void GameQuit(){
    	Application.Quit();
    }

    //Função para pausar a cena
    public void PauseGame(){
    	paused = !paused;

    	//Ao pressionar uma vez, pausar a musica, se pressionado de novo, retomar a musica
        if(paused){
        	GameManager.instance.music.Pause();
        	GameManager.instance.pause = true;
        }
        else if(!paused){
        	GameManager.instance.music.UnPause();
        	GameManager.instance.pause = false;
        }
    }
}
