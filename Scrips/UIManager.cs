using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   public static UIManager instance;

   [SerializeField] private Text canText , ballText;
   [SerializeField] private GameObject mainMenu, gameMenu, gameOverPanel, retryBtn, nextBtn;
   [SerializeField] private GameObject container , lvlBtnPrefab;
   public Text CanText{ get {return canText;}}
   public Text BallText{ get  {return ballText;}}

     private void Awake()
    {
        if(instance==null)
        {
            instance=this;
        }
        else{
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(GameManager.singleton.gameStatus == GameStatus.None)
        {
           CreatLevelButtons();
        }
        else if(GameManager.singleton.gameStatus == GameStatus.Failed || GameManager.singleton.gameStatus == GameStatus.Complete)
        {
            mainMenu.SetActive(false);
            gameMenu.SetActive(true);
            LevelManager.instance.SpawnLevel(GameManager.singleton.currentLevelIndex);
        }
  
    }

    void CreatLevelButtons()
    {
        for(int i=0 ; i<LevelManager.instance.levelDatas.Length ; i++)
        {
            GameObject buttonObj=Instantiate(lvlBtnPrefab , container.transform);
            buttonObj.transform.GetChild(0).GetComponent<Text>().text=""+(i+1);
            Button button=buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() =>onClick(button));
        }
    }

    void onClick(Button btn)
    {
            mainMenu.SetActive(false);
            gameMenu.SetActive(true);
            GameManager.singleton.currentLevelIndex = btn.transform.GetSiblingIndex();
            LevelManager.instance.SpawnLevel(GameManager.singleton.currentLevelIndex);
    }

     public void GameResult(GameStatus gameStatus)
    {
        switch (gameStatus)
        {
            case GameStatus.Complete:                       //if completed
                gameOverPanel.SetActive(true);                
                nextBtn.SetActive(true);    
                SoundManager.instance.PlayFx(FxTypes.GAMEOVER);               
                break;
            case GameStatus.Failed:                         //if failed
                gameOverPanel.SetActive(true);                
                retryBtn.SetActive(true);    
                SoundManager.instance.PlayFx(FxTypes.GAMEWIN);                     
                break;
        }
    }

     public void HomeBtn()                                   
    {
        GameManager.singleton.gameStatus = GameStatus.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextRetryBtn()                               
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
