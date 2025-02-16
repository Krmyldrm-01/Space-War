using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject bossPrefab;

    private int enemiesDefeated = 0;
    private Vector3 spawnPosition = new Vector3(0.7f, 3.48f, 0f); // Başlangıç spawn noktası

    void Start()
    {
        Debug.Log("SpawnManager başlatıldı");
        SpawnEnemy1();
    }

    void SpawnEnemy1()
    {
        Debug.Log("Enemy 1 spawn ediliyor");
        GameObject enemy1 = Instantiate(enemy1Prefab, spawnPosition, Quaternion.identity);
        enemy enemyScript = enemy1.GetComponent<enemy>();
        if (enemyScript != null)
        {
            enemyScript.spawnManager = this;
        }
    }

    void SpawnEnemy2()
    {
        Debug.Log("Enemy 2 spawn ediliyor");
        GameObject enemy2 = Instantiate(enemy2Prefab, spawnPosition, Quaternion.identity);
        enemy enemyScript = enemy2.GetComponent<enemy>();
        if (enemyScript != null)
        {
            enemyScript.spawnManager = this;
            enemyScript.IncreaseHealth(50); // enemy2'nin canını 50 artır
        }
    }

    void SpawnBoss()
    {
        Debug.Log("Boss spawn ediliyor");
        Vector3 spawnPositionBoss = new Vector3(-1.5f, 3.48f, 0f); // Boss spawn noktası
        GameObject boss = Instantiate(bossPrefab, spawnPositionBoss, Quaternion.identity);
        enemy enemyScript = boss.GetComponent<enemy>();
        if (enemyScript != null)
        {
            enemyScript.spawnManager = this;
            enemyScript.IncreaseHealth(100); // Boss'un canını 100 artır
        }
    }

    public void OnEnemyDefeated()
    {
        Debug.Log("Bir düşman yenildi");
        enemiesDefeated++;
        Invoke("SpawnNextEnemy", 1f); // 1 saniye sonra bir sonraki düşmanı spawn et
    }

    void SpawnNextEnemy()
    {
        if (enemiesDefeated == 1)
        {
            SpawnEnemy2(); // Birinci düşman öldüğünde enemy2 spawn edilecek
        }
        else if (enemiesDefeated == 2)
        {
            SpawnBoss(); // İkinci düşman öldüğünde boss spawn edilecek
        }
        else if (enemiesDefeated >= 3)
        {
            Debug.Log("Tebrikler! Tüm düşmanları yendiniz.");
        }
    }
}
