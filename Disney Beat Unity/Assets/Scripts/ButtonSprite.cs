using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script para controlar o sprite dos botões apertaveis
public class ButtonSprite : MonoBehaviour
{
	private SpriteRenderer sr;
	[SerializeField] private Sprite defImage;
	[SerializeField] private Sprite pressImage;

	[SerializeField] private KeyCode pressKey;
	[SerializeField] private KeyCode pressKey2;

    // Start is called before the first frame update
    void Start()
    {
        //Pegar o componente do Sprite Render
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Quando as teclas das setas forem pressionadas ou as teclas WASD, mudar o Sprite para a seta preenchida
        if(Input.GetKeyDown(pressKey) || Input.GetKeyDown(pressKey2)){
        	sr.sprite = pressImage;
        }
        //Ao levantar a tecla voltar para o sprite default
        if(Input.GetKeyUp(pressKey) || Input.GetKeyUp(pressKey2)){
        	sr.sprite = defImage;
        }
    }
}
