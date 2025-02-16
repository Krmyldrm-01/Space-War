using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class yonetici : MonoBehaviour
{
    
    private int score = 0;
    public TMP_Text score_text;
  
    public GameObject oyun_bitti_pnl;
    public GameObject win_win_pnl;
   

    void Start() 
    {
    }

    public void paneli_goster()
    {
    oyun_bitti_pnl.SetActive(true);
    Invoke("durdur", 1.0f);
    }
    public void win_paneli_goster()
    {
        win_win_pnl.SetActive(true);
        
        Invoke("durdur", 1.0f);
    }
    void durdur()
    {
    Time.timeScale = 0.0f;  
    }
    
    public void tekrar_oyna()
    {
    Time.timeScale = 1.0f;
    SceneManager.LoadScene("Scenes/oyun");
    }
     public void tekrar_oyna_win()
    {
    Time.timeScale = 1.0f;
    SceneManager.LoadScene("Scenes/oyun");
    }

      public int GetScore()
    {
        return score;
    }
     public void UpdateScore(int amount)
    {
        score += amount;
        score_text.text = score.ToString();
    }
    void Update()
    {
        
    }
}
