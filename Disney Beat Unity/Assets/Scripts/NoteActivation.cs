using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script para detectar quando as notas forem pressionadas no momento certo
public class NoteActivation : MonoBehaviour
{
	[SerializeField] private bool canBePressed;
	[SerializeField] private KeyCode pressKey;
	[SerializeField] private KeyCode pressKey2;
    private bool hit;

    // Update is called once per frame
    void Update()
    {
        //Verficar se as teclas estão sendo pressionadas no momento certo
        if(Input.GetKeyDown(pressKey) || Input.GetKeyDown(pressKey2)){
        	if(canBePressed){
                hit = true;
        		gameObject.SetActive(false);

                //Calcular a distancia entre o acerto, e classifica-lo em ruim, normal, bom e perfeito
                if(Mathf.Abs(transform.position.y + 4) > 0.4){
                    GameManager.instance.BadHit();
                }
                else if(Mathf.Abs(transform.position.y + 4) > 0.2){
                    GameManager.instance.NormalHit();
                }
                else if(Mathf.Abs(transform.position.y + 4) > 0.10){
                    GameManager.instance.GoodHit();
                }
                else {
                    GameManager.instance.PerfectHit();
                }
                
        	}
        }

        //Apagar as notas que forem erradas
        if(transform.position.y < -7){
            gameObject.SetActive(false);
        }
    }

    //Dizer quando as notas entrarem na area em que podem ser "acertadas"
    private void OnTriggerEnter2D(Collider2D other){
    	if(other.tag == "Activated"){
    		canBePressed = true;
    	}
    }

    //Dizer quando as notas sairem da area em que podem ser "acertadas"
    private void OnTriggerExit2D(Collider2D other){
        if(!hit)
            if(other.tag == "Activated"){
                canBePressed = false;                
                GameManager.instance.NoteMiss();
            }
    }
}
