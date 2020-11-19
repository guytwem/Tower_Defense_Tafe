using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Towers;

namespace TowerDefence.Managers
{
    public class TowerManager : MonoBehaviour
    {
        public static TowerManager instance;

        public GameObject towerPrefab;

        Player player;

        private int groundLayerMask;

        private int cost = 1;

        Tower tower = null;

        public List<Tower> towerList = new List<Tower>();

        private Vector3 towerPlacement = new Vector3(0, 0, 0);



        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            player = Player.instance;
        }

        private void Update()
        {
            FindTowerSpawn();
            Debug.Log(towerPlacement);
        }


        private void FindTowerSpawn()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                groundLayerMask = 1 << 8;

                RaycastHit hit;


                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, groundLayerMask))
                {
                    Vector3 newTowerPosition = hit.point;
                    towerPlacement = newTowerPosition;
                }
                
            }
            
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), towerPlacement.ToString()))
            {
                if(player.money >= cost)
                {

                    GameObject newTower = Instantiate(towerPrefab, towerPlacement, Quaternion.identity, transform);
                    Tower newTow = newTower.GetComponent<Tower>(); 
                    towerList.Add(newTow);
                }
            }
            
        }
    }
}
