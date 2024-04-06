using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Teleport_MainGate : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerOj;
    public Player pl;
    private Collider gate;
    public TextMeshProUGUI gateStatus;

    public List<GameObject> cubeObjects;
    public List<Transform> cubeDestinations;
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
