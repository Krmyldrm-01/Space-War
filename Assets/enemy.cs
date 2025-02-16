using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class enemy : MonoBehaviour
{
    public Transform player;
    public yonetici yonet;
    public GameObject patlama_efekti;
    public GameObject dusman_kursunu;
    public Image enemry_bar;
    float can = 100.0f;
    float simdiki_can = 100.0f;
    float hareket_hizi = 1.0f;
    float kursun_hizi = 175.0f;//175yap
    float ates_etme_araligi = 0.2f;
    float ates_etme_zamani = 0.0f;
    public SpawnManager spawnManager;
    public int counter=0;
    void Start()
    {
           // Düşman türüne göre can, hareket hızı ve kurşun hızını ayarla
        if (gameObject.CompareTag("enemy2"))
        {
            can = 150.0f; // Enemy2 için 150 can
            hareket_hizi += 0.25f; // Enemy1'den 0.25 daha hızlı
            kursun_hizi += 10.0f; // Enemy1'den 10 daha hızlı
        }
        else if (gameObject.CompareTag("boss"))
        {
            can = 200.0f; // Boss için 200 can
            hareket_hizi += 0.5f; // Enemy1'den 0.5 daha hızlı (Enemy2'den 0.25 daha hızlı)
            kursun_hizi += 20.0f; // Enemy1'den 20 daha hızlı (Enemy2'den 10 daha hızlı)
        }

        simdiki_can = can;
        yonet = FindObjectOfType<yonetici>();
        

    }

    void can_azalt(float deger)
    {
        simdiki_can -= deger;
        enemry_bar.fillAmount = simdiki_can / can;

        if (simdiki_can <= 0)
        {
            if (gameObject.CompareTag("boss"))
            {
                Debug.Log("Boss yok edildi, win paneli gösteriliyor");
                yonet.win_paneli_goster();
            }
            yok_ol();
        }
        
        if (gameObject.CompareTag("enemy2")) // enemy2 için skor +3
        {
            yonet.UpdateScore(3);
        }
        else if (gameObject.CompareTag("boss")) // boss için skor +5
        {
            yonet.UpdateScore(5);
        }
        else
        {
            yonet.UpdateScore(1);
        }
    }

    void yok_ol()
    {
        counter++;
        GameObject yeni_patlama = Instantiate(patlama_efekti, transform.position, Quaternion.identity);
        Destroy(yeni_patlama, 1.0f);
        Destroy(gameObject);

        if (spawnManager != null)
        {
            spawnManager.OnEnemyDefeated();
        }
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player_ball")
        {
            Destroy(collision.gameObject);
            can_azalt(5.0f);
        }
    }

    public void ates_et()
    {
        GameObject yeni_kursun = Instantiate(dusman_kursunu, transform.position, Quaternion.identity);
        yeni_kursun.GetComponent<Rigidbody2D>().AddForce(Vector2.down * kursun_hizi);

        Destroy(yeni_kursun, 2.0f);
    }

    void Update()
    {
        if (player)
        {
            if (transform.position.x < player.position.x)
            {
                transform.Translate(hareket_hizi * Time.deltaTime, 0, 0);
            }
            if (transform.position.x > player.position.x)
            {
                transform.Translate(-hareket_hizi * Time.deltaTime, 0, 0);
            }

            if (player.position.x - transform.position.x <= 0.3f)
            {
                if (Time.time >= ates_etme_zamani)
                {
                    ates_et();
                    ates_etme_zamani = Time.time + ates_etme_araligi;
                }
            }
        }
    }

    public void IncreaseHealth(float amount)
    {
        simdiki_can += amount;
        enemry_bar.fillAmount = simdiki_can / can; // Can çubuğunu güncelle
    }
}
