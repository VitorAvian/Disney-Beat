using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Script do Game Manager, controla a maioria das funcões do jogo
public class GameManager : MonoBehaviour
{
	//Variaveis referentes ao ambiente da cena
	public AudioSource music;
	[SerializeField] private bool startPlaying;
	[SerializeField] private Scroller scroll;

	public static GameManager instance;

	//Variaveis referentes a pontuação
	[SerializeField] private int score;
	[SerializeField] private int highscore;
	[SerializeField] private int noteNormalScore = 10;
	[SerializeField] private int noteBadScore = 5;
	[SerializeField] private int noteGoodScore = 12;
	[SerializeField] private int notePerfectScore = 15;

	[SerializeField] private int multiplier = 1;
	[SerializeField] private int multiplierTracker;
	[SerializeField] private int[] multiplierLevels;

	//Variaveis referentes ao resultado
	[SerializeField] private Text scoreText;
	[SerializeField] private Text multiText;

	[SerializeField] private float totalNotes;
	[SerializeField] private float badHits;
	[SerializeField] private float normalHits;
	[SerializeField] private float goodHits;
	[SerializeField] private float perfectHits;
	[SerializeField] private float missedHits;

	[SerializeField] private float perfectScore;

	[SerializeField] private GameObject resultScreen;
	[SerializeField] private GameObject pauseButton;
	[SerializeField] private GameObject boxScore;
	[SerializeField] private GameObject buttons;


	[SerializeField] private Text badText, normalText, goodText, perfectText, percentText, rankText, finalScoreText, highscoreText;
	[SerializeField] private float percent;
	public bool pause;

	[SerializeField] private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
    	//Pegar o nome da cena atual
    	Scene currentScene = SceneManager.GetActiveScene();
    	sceneName = currentScene.name;
    	
    	//Calculos iniciais
    	resultScreen.SetActive(false);
        instance = this;
        score = 0;
        multiplier = 1;
        totalNotes = FindObjectsOfType<NoteActivation>().Length;
        perfectScore = (totalNotes-30)*notePerfectScore*4 + 5*notePerfectScore + 10*notePerfectScore*2 + 15*notePerfectScore*3;
        
        //Calculo de highscore entre as diferentes musicas
        if(sceneName == "Let It Go"){
        	highscore = PlayerPrefs.GetInt ("highscore1", highscore);
        }
        else if(sceneName == "Healing Incantation"){
        	highscore = PlayerPrefs.GetInt ("highscore2", highscore);
        }
        else if(sceneName == "How Far ill Go"){
        	highscore = PlayerPrefs.GetInt ("highscore3", highscore);
        }
        highscoreText.text = highscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    	//Começar a musica e a cena
        if(!startPlaying){
        	startPlaying = true;
        	scroll.hasStarted = true;

        	music.Play();
        }
        else{
        	//Quando a musica acabar, e nao for o menu de pause, mostrar o resultado na tela
        	if(!music.isPlaying  && !pause && !resultScreen.activeInHierarchy){
        		pauseButton.SetActive(false);
        		boxScore.SetActive(false);
        		buttons.SetActive(false);

        		resultScreen.SetActive(true);

        		//Calculo do highscore
        		if (score > highscore){
         			highscore = score;
         			highscoreText.text = "" + score;
  
         			if(sceneName == "Let It Go"){
         				PlayerPrefs.SetInt ("highscore1", highscore);
     				}
     				else if(sceneName == "Healing Incantation"){
     					PlayerPrefs.SetInt ("highscore2", highscore);
     				}
     				else if(sceneName == "How Far ill Go"){
     					PlayerPrefs.SetInt ("highscore3", highscore);
     				}
     			}

     			//Mostrar as estatisticas
        		badText.text = badHits.ToString();
        		normalText.text = normalHits.ToString();
        		goodText.text = goodHits.ToString();
        		perfectText.text = perfectHits.ToString();

        		percent = ((totalNotes-missedHits)/totalNotes)*100f;
        		percentText.text = percent.ToString("F1") + "%";

        		finalScoreText.text = score.ToString();

        		//Calculo do rank
        		if(score >= perfectScore*(0.7)){
        			rankText.text = "S";
        			rankText.color = Color.magenta;
        			//rankText.color = new Color(212, 175, 55, 1);
        		}
        		else if(score >= perfectScore*(0.6)){
        			rankText.text = "A";
        			rankText.color = Color.cyan;
        		}
        		else if(score >= perfectScore*(0.5)){
        			rankText.text = "B";
        			rankText.color = Color.green;
        		}
        		else if(score >= perfectScore*(0.4)){
        			rankText.text = "C";
        			rankText.color = Color.yellow;
        		}
        		else if(score >= perfectScore*(0.3)){
        			rankText.text = "D";
        			rankText.color = Color.white;
        		}
        		else{
        			rankText.text = "F";
        			rankText.color = Color.red;
        		}
        	}
        }
    }

    //Função que faz a contagem de notas acertadas e mostra na pontuacao
    public void NoteHit(){
    	if(multiplier - 1 < multiplierLevels.Length){
			multiplierTracker ++;
			if(multiplierTracker >= multiplierLevels[multiplier-1]){
				multiplierTracker = 0;
				multiplier++;
			}
    	}
    	multiText.text = "" + multiplier;
    	scoreText.text = "" + score;
    }

    //Função para um hit normal
    public void NormalHit(){
    	score += noteNormalScore * multiplier;
    	NoteHit();
    	normalHits++;
    }

    //Função para um hit ruim
    public void BadHit(){
    	score += noteBadScore * multiplier;
    	NoteHit();
    	badHits++;
    }

    //Função para um hit bom
    public void GoodHit(){
    	score += noteGoodScore * multiplier;
    	NoteHit();
    	goodHits++;
    }	

    //Função para um hit perfeito
    public void PerfectHit(){
    	score += notePerfectScore * multiplier;
    	NoteHit();
    	perfectHits++;
    }

    //Função para quando a nota passa do ponto certo, resetar o multiplicador
    public void NoteMiss(){
    	multiplier = 1;
    	multiplierTracker = 0;
    	multiText.text = "" + multiplier;
    	missedHits++;
    }
}
