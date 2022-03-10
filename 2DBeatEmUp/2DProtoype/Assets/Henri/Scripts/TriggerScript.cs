using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{

    // IN GAME VARIABELS FOR ENEMY SPAWN

    public GameObject[] spawnPoints;

    public GameObject[] enemies;


    //THIS SCRIPT IS ATTACHED TO PLAYER

    // CAMERA TARGETS WHICH IS CHANGED BASED ON WHICH TRIGGER PLAYER ACTIVATES
    public Transform cam1Target;
    public Transform cam2Target;
    public Transform cam3Target;
    public Transform player;

    // VIARIABLES FOR COLLIDERS WHICH IS ACTIVATED TO LIMIT PLAYER MOVEMENT
    public GameObject waweCollider1;
    public GameObject waweCollider2;
    public GameObject bossCollider1;
    public GameObject bossCollider2;
    public GameObject followCamTrigger2;

    public GameObject sanityValueText;
    public GameObject sanityValueText2;
    public GameObject sanityValueText3;
    public GameObject sanityValueText4;
    public GameObject sanityValueText5;



    // ACCESS TO CAMERA SCRIPT
    public CameraController cameraController;

    // ACCESS TO ENEMYSPAWNER SCRIPT
    public EnemySpawnerScript enemySpawner;
    public FirstBossScript firstBoss;
    public PlayerMovement playerMovement;
    public GameManager gameManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        // PLAYER ACTIVATES STARTCAMERA TARGET
        if (collision.gameObject.tag == "CameraOne")
        {
            cameraController.target = cam1Target;

        }

        // PLAYER ACTIVATES WAWE CAMERA TARGET
        if (collision.gameObject.tag == "CameraTwo")
        {
            cameraController.target = cam2Target;
            waweCollider1.SetActive(true);
            waweCollider2.SetActive(true);
            enemySpawner.enabled = true;

        }

        // PLAYER ACTIVATES BOSSCAMERA TARGET
        if (collision.gameObject.tag == "CameraThree")
        {
            cameraController.target = cam3Target;
            bossCollider1.SetActive(true);
            bossCollider2.SetActive(true);
            gameManager.bossHealthImage.gameObject.SetActive(true);
            gameManager.bossNameText.SetActive(true);
        }

        // PLAYER ACTIVATES CAMERATARGER WHICH FOLLOWS PLAYER
        if (collision.gameObject.tag == "FollowCam")
        {
            cameraController.target = player;
            enemySpawner.waweCounterText.SetActive(false);
            enemySpawner.completeText.SetActive(false);
        }

        // ACTIVATES THE SECOND FOLLOW CAM
        if (collision.gameObject.tag == "FollowCam2")
        {
            cameraController.target = player;
            enemySpawner.waweCounterText.SetActive(false);
            enemySpawner.completeText.SetActive(false);
            playerMovement.sanityLoss = false;

            if (playerMovement.playerSanity >= 9000)
            {
                sanityValueText.SetActive(true);
            }

            if (playerMovement.playerSanity < 9000 && playerMovement.playerSanity >= 8000)
            {
                sanityValueText2.SetActive(true);
            }

            if (playerMovement.playerSanity < 8000 && playerMovement.playerSanity >= 6000)
            {
                sanityValueText3.SetActive(true);
            }

            if (playerMovement.playerSanity < 6000 && playerMovement.playerSanity >= 4000)
            {
                sanityValueText4.SetActive(true);
            }

            if (playerMovement.playerSanity < 4000)
            {
                sanityValueText5.SetActive(true);
            }
        }

        if (collision.gameObject.tag == "EnemySpawn")
        {
            Instantiate(enemies[0], spawnPoints[0].transform.position, spawnPoints[0].transform.rotation);
            Instantiate(enemies[1], spawnPoints[1].transform.position, spawnPoints[1].transform.rotation);
            Instantiate(enemies[0], spawnPoints[2].transform.position, spawnPoints[2].transform.rotation);
            Destroy(spawnPoints[0]);
            Destroy(spawnPoints[1]);
            Destroy(spawnPoints[2]);
        }

        if (collision.gameObject.tag == "EnemySpawn2")
        {
            Instantiate(enemies[0], spawnPoints[3].transform.position, spawnPoints[3].transform.rotation);
            Instantiate(enemies[1], spawnPoints[4].transform.position, spawnPoints[4].transform.rotation);
            Instantiate(enemies[0], spawnPoints[5].transform.position, spawnPoints[5].transform.rotation);
            Destroy(spawnPoints[3]);
            Destroy(spawnPoints[4]);
            Destroy(spawnPoints[5]);
        }

    }
        // Start is called before the first frame update
        void Start()
    {
        

        // TELL SCRIPT VARIABLES WHERE TO FIND THE SPECIFIC SCRIPT
        cameraController = GameObject.Find("CameraTarget").GetComponent<CameraController>();
        enemySpawner = GameObject.Find("EnemySpawnerObject").GetComponent<EnemySpawnerScript>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
