using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public int killsNeeded;
    public int turretKillNeeded;

    public GameObject BlockerWall;
    public PlayerJump player;

    public GameObject[] blockers;

    public Transform nextRoomSpawn;

    public GameObject enemy;
    public float spawnDelay;

    public void AddKill()
    {
        killsNeeded -= 1;
    }

    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        EnemyHealth eHP = Instantiate(enemy, transform.position + new Vector3(-2,0,-3), transform.rotation).GetComponent<EnemyHealth>();

        eHP.level2Mode = true;
        eHP.level2Room = this;
    }

    public void AddTurretKill()
    {
        turretKillNeeded -= 1;
    }

    void Update()
    {
        if (killsNeeded <= 0)
        {
            for (int i = 0; i < blockers.Length; i++)
            {
                blockers[i].SetActive(false);
            }
        }

        if (turretKillNeeded <= 0)
        {
            BlockerWall.SetActive(false);
            player.level2Complete = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.gameObject.transform.position = nextRoomSpawn.position;
            turretKillNeeded = 10000;
            player.level2Complete = false;
        }
    }
}
