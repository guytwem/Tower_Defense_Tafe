using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    public EnemyManager instance;

    public List<Enemy> enemies = new List<Enemy>();

    public GameObject enemy;

    public Enemy enemyScript;

   

    #region Private Wave Variables

    [Header("Wave Variables")]
    [SerializeField, Tooltip("Amount of enemies spawning per second(some time value)")]
    private float enemySpawnRate = 1f;
    [SerializeField, Tooltip("Amount of time before next wave")]
    private float endWaveWaitTime = 20f;
    [SerializeField, Tooltip("What wave it is")]
    private int waveIndex = 0;
    [SerializeField, Tooltip("Amount of enemies in the first wave")]
    private int wave1 = 5;
    

    public float xPos; // the boundaries for where enemies can spawn
    public float zPos;
    public int enemyCount; // how many enemies will spawn
    

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
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(EnemyDrop());
        

    }

    private void Update()
    {
        //foreach (GameObject enemy in enemies)
        //{
         //   enemyScript.NextState();
        //}
        /*if (enemy == null)
        {
            enemy = GameObject.FindWithTag("Enemy");
        }*/
    }

    IEnumerator EnemyDrop() // spawns enemies up to ten in a random area
    {
        while(enemyCount < 10)
        {
            //xPos = Random.Range(-21, 14);
            //zPos = Random.Range(-11, 13);

            Vector3 newSpawnLocation = SpawnLocation(xPos, zPos);

            //GameObject newEnemy = Instantiate(enemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            GameObject newEnemy = Instantiate(enemy, newSpawnLocation, Quaternion.identity);
            Enemy enemyRef = newEnemy.GetComponent<Enemy>();
            //newEnemy.GetComponent<Enemy>().enemy = enemy;
            enemies.Add(enemyRef);
            //Debug.Log(enemies.Count);
            yield return new WaitForSeconds(5f);
            enemyCount += 1;
        }
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

