using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script para controlar o decaimento das notas
public class Scroller : MonoBehaviour
{
	[SerializeField] private float beatTempo;
	public bool hasStarted;

    void Start()
    {
        //Calcular a velocidade de decaimento das notas atraves do tempo da musica
        beatTempo = beatTempo/60f;
    }

    void Update()
    {
        //Se a cena começar e a musica estiver tocando, notas devem cair
    	if(hasStarted && GameManager.instance.music.isPlaying){
    		transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    	
    }
}
