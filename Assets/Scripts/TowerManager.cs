using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefence.Towers;

namespace TowerDefence.Managers
{
    public class TowerManager : MonoBehaviour
    {
        public static TowerManager instance;
        Player player;

        public GameObject towerPrefab;
        [SerializeField] private Transform worldSpacePointer;

        private int cost = 1;
        private int groundLayerMask;

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
            FindNewTowerSpawn();
        }

        private void FindNewTowerSpawn()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                groundLayerMask = 1 << 8;

                RaycastHit hit;

                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, groundLayerMask))
                {
                    //Vector3 newTowerPosition = hit.point;
                    //towerPlacement = newTowerPosition;

                    worldSpacePointer.position = hit.point;
                    towerPlacement = worldSpacePointer.position;
                    worldSpacePointer.gameObject.SetActive(true);
                }
            }
        }

        public void PurchaseTower()
        {
            if (player.money >= cost)
            {
                GameObject newTower = Instantiate(towerPrefab, towerPlacement, Quaternion.identity, transform);
                Tower newTow = newTower.GetComponent<Tower>();
                towerList.Add(newTow);
                worldSpacePointer.gameObject.SetActive(false);
            }
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 200, 40), towerPlacement.ToString()))
            {
                PurchaseTower();
            }
        }
    }
}
