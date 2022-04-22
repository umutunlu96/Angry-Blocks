using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [Header("Min - Max")]
    public int minBoxNumber;
    public int maxBoxNumber;
    [Header("Blocks")]
    public GameObject objects;
    public Vector2 spawnObjPos;

    [HideInInspector]public Text ballsCountText;
    private ShotCountText shotCountText;

    private GameObject[] block;


    public int shotCount;
    public int score;
    public int ballsCount;

    private GameObject ballsContainer;
    public GameObject gameOver;

    private bool firstShot;

    void Awake()
    {
        PlayerPrefs.DeleteAll();
        shotCountText = GameObject.Find("ShotCountText").GetComponent<ShotCountText>();
        ballsCountText = GameObject.Find("BallCountText").GetComponent<Text>();
        ballsContainer = GameObject.Find("BallsContainer");
    }

    void Start()
    {
        SetBlocksCount(minBoxNumber, maxBoxNumber);
        ballsCount = PlayerPrefs.GetInt("BallsCount", 5);
        ballsCountText.text = ballsCount.ToString();

        Physics2D.gravity = new Vector2(0, -17);

        //SpawnNewLevel(minBoxNumber, maxBoxNumber);
        GameObject.Find("Cannon").GetComponent<Animator>().SetBool("MoveIn", true);

    }

    void Update() {
        if(ballsContainer.transform.childCount == 0 && shotCount == 4)
        {
            gameOver.SetActive(true);
            GameObject.Find("Cannon").GetComponent<Animator>().SetBool("MoveIn", false);
        }

        if (shotCount > 2)
            firstShot = false;
        else
            firstShot = true;

        CheckBlocks();
    }

    void SpawnNewLevel(int min, int max)
    {
        if(shotCount > 1)
            Camera.main.GetComponent<CameraTransitions>().RotateCameraToFront();

        shotCount = 1;

        Instantiate(objects, spawnObjPos, Quaternion.identity);

        SetBlocksCount(min, max);

    }

    void SetBlocksCount(int min, int max)
    {
        block = GameObject.FindGameObjectsWithTag("Block");

        for (int i = 0; i < block.Length; i++)
        {
            int count = Random.Range(min, max);
            block[i].GetComponent<Block>().SetStartingCount(count);
        }
    }

    public void CheckBlocks()
    {
        block = GameObject.FindGameObjectsWithTag("Block");

        if (block.Length < 1) {
            RemoveBalls();
            PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
            
            //Next LEVEL
            if (ballsCount >= PlayerPrefs.GetInt("BallsCount", 5))
                PlayerPrefs.SetInt("BallsCount", ballsCount);

            if (firstShot)
                score += 5;
            else
                score += 3;
        }
    }

    void RemoveBalls()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        for (int i = 0; i < balls.Length; i++)
        {
            Destroy(balls[i]);
        }

    }

    public void CheckShotCount()
    {
        if(shotCount == 1)
        {
            shotCountText.SetTopText("SHOT");
            shotCountText.SetBottomText("1/3");
            shotCountText.Flash();
        }
        if (shotCount == 2)
        {
            shotCountText.SetTopText("SHOT");
            shotCountText.SetBottomText("2/3");
            shotCountText.Flash();
        }
        if (shotCount == 3)
        {
            shotCountText.SetTopText("FINAL");
            shotCountText.SetBottomText("SHOT");
            shotCountText.Flash();
        }
    }

}
