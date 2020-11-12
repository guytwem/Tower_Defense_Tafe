using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class EnemyManager : MonoBehaviour
{
    public EnemyManager instance;

    public List<Enemy> enemies = new List<Enemy>();

    public GameObject enemy;

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



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop());

    }

   IEnumerator EnemyDrop() // spawns enemies up to ten in a random area
    {
        while(enemyCount < 10)
        {
            xPos = Random.Range(-21, 14);
            zPos = Random.Range(-11, 13);
            Instantiate(enemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(5f);
            enemyCount += 1;
        }
    }

   

}

