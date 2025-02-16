using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
public class player : MonoBehaviour
{
    public yonetici yonet;
    public GameObject patlama_efekti;
    public GameObject oyuncu_kursunu;
    public Image player_bar;
    float can = 100.0f;
    float simdiki_can = 100.0f;
    float hareket_hizi = 5.0f;
    float kursun_hizi = 500.0f;

    
    public void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy_ball")
        {
             Destroy(collision.gameObject);
             can_azalt(10.0f);
        }
    {
    }

     void can_azalt(float deger)
    {
    if (gameObject.CompareTag("enemy2")) // enemy2 için
    {
        simdiki_can -= 15; // 15 can azalt
       
    }
    else if (gameObject.CompareTag("boss")) // boss için
    {
        simdiki_can -= 20; // 20 can azalt
       
    }
    else
    {
        simdiki_can -= deger; // Diğer durumlarda belirtilen miktarda can azalt
    }

   player_bar.fillAmount = simdiki_can / can;
        if (simdiki_can <= 0)
        {
            yok_ol();
        }
    } 
    


     void yok_ol()
    {
         Destroy(gameObject);  
        GameObject yeni_patlama = Instantiate (patlama_efekti, transform.position, Quaternion. identity);
        Destroy(yeni_patlama, 1.0f);  
        yonet.paneli_goster();
    }

    }

    void ates_et()
    {
        GameObject yeni_kursun = Instantiate(oyuncu_kursunu, transform.position, Quaternion.identity);
        yeni_kursun.GetComponent<Rigidbody2D>().AddForce(Vector2.up * kursun_hizi);

        Destroy(yeni_kursun, 2.0f);
    }

   public void Update()
{
    float tus_tespiti = -Input.GetAxis("Horizontal"); // - işareti eklenmiş
    transform.Translate(tus_tespiti * Time.deltaTime * hareket_hizi, 0, 0);
    if (Input.GetKeyDown(KeyCode.Space))
    {
        ates_et();
    }
}


    
}