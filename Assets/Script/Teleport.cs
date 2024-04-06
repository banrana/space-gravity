using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerOj;
    public Player pl;
    private Collider gate;
    public TextMeshProUGUI gateStatus;

    public List<GameObject> cubeObjects;
    public List<Transform> cubeDestinations;
    public List<GameObject> Enemy;
    public List<Transform> EnemyDestinations;
    public List<GameObject> HealingBar;
    public List<Transform> HealDestinations;
    public GameObject Key;
    public List<Transform> KeyDestinations;
    public void Start()
    {
        gate = GetComponent<Collider>();    
        gate.isTrigger = false;
        UpdateGateStatus();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            gate.isTrigger = false;

            playerOj.SetActive(false);
            player.position = destination.position;
            playerOj.SetActive(true);

            if (pl.DeathEffect != null && pl.HitEffect != null)
            {
                pl.DeathEffect.Stop();
                pl.HitEffect.Stop();
            }
            //wall
            int numCubes = Random.Range(1, cubeObjects.Count);
            List<Transform> randomDestinations = GetRandomDestinations(numCubes);

            for (int i = 0; i < numCubes; i++)
            {
                GameObject cubeObject = cubeObjects[i];
                cubeObject.SetActive(false);

                Transform randomDestination = randomDestinations[i];
                cubeObject.transform.position = randomDestination.position;
                cubeObject.SetActive(true);
            }
            //enemy
            int numEnemies = Random.Range(1, Enemy.Count);
            List<Transform> enemyRandomDestinations = GetEnemyRandomDestinations(numEnemies);

            for (int i = 0; i < numEnemies; i++)
            {
                GameObject enemyObject = Enemy[i];
                enemyObject.SetActive(false);

                Transform randomDestination = enemyRandomDestinations[i];
                enemyObject.transform.position = randomDestination.position;
                enemyObject.SetActive(true);
            }
            //healbar
            int numHealBars = Random.Range(1, HealingBar.Count);
            List<Transform> HealRandomDestinations = GetHealingRandomDestinations(numHealBars);

            for (int i = 0; i < numHealBars; i++)
            {
                GameObject HealObject = HealingBar[i];
                HealObject.SetActive(false);

                Transform randomDestination = HealRandomDestinations[i];
                HealObject.transform.position = randomDestination.position;
                HealObject.SetActive(true);
            }
            //key
            GetKeyToRandomDestination();

        }
    }

    private List<Transform> GetRandomDestinations(int count)
    {
        List<Transform> randomDestinations = new List<Transform>();
        List<Transform> availableDestinations = new List<Transform>(cubeDestinations);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, availableDestinations.Count);
            randomDestinations.Add(availableDestinations[randomIndex]);
            availableDestinations.RemoveAt(randomIndex);
        }

        return randomDestinations;
    }
    private List<Transform> GetEnemyRandomDestinations(int count)
    {
        List<Transform> randomDestinations = new List<Transform>();
        List<Transform> availableDestinations = new List<Transform>(EnemyDestinations);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, availableDestinations.Count);
            randomDestinations.Add(availableDestinations[randomIndex]);
            availableDestinations.RemoveAt(randomIndex);
        }

        return randomDestinations;
    }
    private List<Transform> GetHealingRandomDestinations(int count)
    {
        List<Transform> randomDestinations = new List<Transform>();
        List<Transform> availableDestinations = new List<Transform>(HealDestinations);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, availableDestinations.Count);
            randomDestinations.Add(availableDestinations[randomIndex]);
            availableDestinations.RemoveAt(randomIndex);
        }

        return randomDestinations;
    }
    private void GetKeyToRandomDestination()
    {
        int randomIndex = Random.Range(0, KeyDestinations.Count);
        Transform randomDestination = KeyDestinations[randomIndex];

        Key.SetActive(false);
        Key.transform.position = randomDestination.position;
        Key.SetActive(true);
    }
    private void Update()
    {
        UpdateGateStatus();
    }
    private void UpdateGateStatus()
    {
        if (gate.isTrigger)
        {
            gateStatus.text = "OPEN";
        }
        else
        {
            gateStatus.text = "CLOSED";
        }
    }
}
