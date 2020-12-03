using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public List<Enemy> enemies = new List<Enemy>();

    public GameObject enemy;

    public Enemy enemyScript;

    public Text nextWaveInText;
    public Text currentWaveText;

    #region Private Wave Variables

    [Header("Wave Variables")]
    [SerializeField, Tooltip("Amount of enemies spawning per second(some time value)")]
    private float enemySpawnRate = 1f;
    [SerializeField, Tooltip("Amount of time before next wave")]
    private float endWaveWaitTime = 20f;
    [SerializeField, Tooltip("What wave it is")]
    private int waveIndex = 1;
    [SerializeField, Tooltip("Amount of enemies in the first wave")]
    private int wave1 = 5;
    
    public float xPos; // the boundaries for where enemies can spawn
    public float zPos;
    public int enemyCount; // how many enemies will spawn
    
    private float waveStartWaitTime = 10f;

    #endregion

    #region Wave Variables Properties

    public float EnemySpawnRate { get { return enemySpawnRate; } }
    /// <summary>
    /// amount of enemies for each wave (needs to be modified to make sure
    /// the growth of waves is not so drastic(then delete thes brackets( maybe somthing
    /// like every wave add 5 enemies or something )))
    /// </summary>
    public float EnemySpawnAmount { get { return wave1 * waveIndex; } }
    public float EndWaveWaitTime { get { return endWaveWaitTime; } }
    public int WaveIndex { get { return waveIndex; } }
    public int Wave1 { get { return wave1; } }

    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    private void Update()
    {
        currentWaveText.text = "Current Wave: " + waveIndex.ToString();
    }

    public Enemy[] EnemiesInRange(Transform target, float maxRange, float minRange = 0)
    {
        List<Enemy> enemiesInRange = new List<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, target.position);
            if (distance < maxRange && distance > minRange)
            {
                enemiesInRange.Add(enemy);
            }
        }

        return enemiesInRange.ToArray();
    }

    IEnumerator EnemyDrop() // spawns enemies up to ten in a random area
    {
        do
        {
            if (enemyCount == 0)
            {
                yield return StartCoroutine(WaitAndPrintSeconds());
            }

            //xPos = Random.Range(-21, 14);
            //zPos = Random.Range(-11, 13);

            Vector3 newSpawnLocation = SpawnLocation(xPos, zPos);

            //GameObject newEnemy = Instantiate(enemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            GameObject newEnemy = Instantiate(enemy, newSpawnLocation, Quaternion.identity, transform);
            // add if enough time a look at player base logic------------------------------------------------------------------

            Enemy enemyRef = newEnemy.GetComponent<Enemy>();
            // add health to enemy every round / wave
            enemyRef.enemyHealth += 1 * waveIndex;
            enemies.Add(enemyRef);
            enemyCount++;

            if (enemyCount == EnemySpawnAmount)
            {
                while (enemyCount == EnemySpawnAmount)
                {
                    if (enemies.Count == 0)
                    {
                        enemyCount = 0;
                        waveIndex++;
                    }
                    yield return null;
                }
            }
            else
            {
                yield return new WaitForSeconds(1f);
            }

        } while (enemyCount < EnemySpawnAmount);

        //// first wait 10 sec
        //// start spawning enemies 
        //// once EnemySpawnAmount > enemyCount then 
        // increment the wave index 
        // and restart 10 second timer
        // start spawning enemies, this time with more enemies

    }

    IEnumerator WaitAndPrintSeconds()
    {
        nextWaveInText.gameObject.SetActive(true);
        while (waveStartWaitTime > 0)
        {
            waveStartWaitTime -= Time.deltaTime;
            nextWaveInText.text = "Next Wave in: " + waveStartWaitTime.ToString("F0");

            yield return null;
        }
        waveStartWaitTime = 10f;
        nextWaveInText.gameObject.SetActive(false);
    }

    /// <summary>
    /// finds random x, random z, changes signs if nessesary, and returns Vector3
    /// </summary>
    /// <returns></returns>
    private Vector3 SpawnLocation(float _xPosition, float _zPosition)
    {
        // which value to set first
        int firstValueSet = Random.Range(0, 2);
        if (firstValueSet == 0)
        {
            // set x position first
            _xPosition = Random.Range(18, 21);
            _zPosition = Random.Range(-11, 13);
        }
        else
        {
            // set x position first
            _zPosition = Random.Range(10, 13);
            _xPosition = Random.Range(-21, 14);
        }

        // which values to change
        int signConfig = Random.Range(0, 3);
        if (signConfig == 0)
        {
            // change both sign values
            _xPosition = -_xPosition;
            _zPosition = -_zPosition;
        }
        else if (signConfig == 1)
        {
            // change x sign value
            _xPosition = -_xPosition;
        }
        else if (signConfig == 2)
        {
            // change x sign value
            _zPosition = -_zPosition;
        }
        // else keep signs

        return new Vector3(_xPosition, 0, _zPosition);
    }

}

