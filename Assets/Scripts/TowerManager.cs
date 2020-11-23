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
                int UILayerMask = 1 << 5;
                int groundAndUILayerMask = groundLayerMask | UILayerMask;

                RaycastHit hit;

                if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, groundAndUILayerMask))
                {
                    //if hit UIlayer(layer 5) dont set variables below
                    if (hit.collider.gameObject.layer == 5)
                    {
                        return;
                    }
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

                Vector3 towerRotation = newTower.transform.localEulerAngles;
                towerRotation.x = -90;
                newTower.transform.localEulerAngles = towerRotation;

                newTower.transform.localScale = new Vector3(40, 40, 40);

                Tower newTow = newTower.GetComponent<Tower>();
                towerList.Add(newTow);
                worldSpacePointer.gameObject.SetActive(false);
            }
        }

        private void OnGUI()
        {
            //if (GUI.Button(new Rect(0, 0, 200, 40), towerPlacement.ToString()))
            //{
            //    PurchaseTower();
            //}
        }
    }
}
