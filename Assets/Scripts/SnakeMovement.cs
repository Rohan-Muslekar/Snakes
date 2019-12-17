using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour
{
int[,] grid = new int[20, 20];
 
    int snakeScore = 3;
    int snakeX = 0;
    int snakeY = 10;
 
    Transform snakeTransform;
    float lastMove;
    float timeInBetweenMoves = 0.25f;
    Vector3 direction;
 
    bool hasLost;

    public GameObject applePrefab;

    public Text scoreText;
    public float times = 2.0f;
 
    private void Start()
    {   scoreText.text = "Score: " + snakeScore.ToString();
        snakeTransform = transform;
        direction = Vector3.right;
        grid[snakeX, snakeY] = snakeScore;
        
        grid[10,10] = -1;
        GameObject go = Instantiate(applePrefab) as GameObject;
        go.transform.position = new Vector3(10*1.0f + 0.5f, 10*1.0f + 0.5f,5.0f);
        go.name = "Apple";
    }
    
    IEnumerator timer(){
        yield return new WaitForSeconds(times);
    }
    private void Update()
    {
        if (hasLost)
        {      
            StartCoroutine(timer());
            UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
            return;
        }
 
        if (Time.time - lastMove > timeInBetweenMoves)
        {
            //Iterate every array and reduce if not 0 or -1
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] > 0)
                    {
                        grid[i, j]--;
                        if (grid[i, j] == 0)
                        {
                            //Destroy Object
                            GameObject toDestroy = GameObject.Find(i.ToString() + j.ToString());
                            Debug.Log(i.ToString()+j.ToString());
                            if(toDestroy != null)
                            {
                                Destroy(toDestroy);
                            }
                        }
                    }
                }
            }
           
            lastMove = Time.time;
 
            //Create object
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.position = new Vector3(snakeX*1.0f + 0.5f, snakeY*1.0f + 0.5f, 5f);
            go.name = snakeX.ToString() + snakeY.ToString();
 
            //add direction
            if (direction.x == 1)
            {
                snakeX++;
            }
 
            if (direction.x == -1)
            {
                snakeX--;
            }
 
            if (direction.y == 1)
            {
                snakeY++;
            }
 
            if (direction.y == -1)
            {
                snakeY--;
            }
 
            //Out of bounds
            if (snakeX >= grid.GetLength(0) || snakeX < 0 || snakeY >= grid.GetLength(1) || snakeY < 0)
            {
                hasLost = true;
                Debug.Log("You lost!");
            }
            else
            {
                //Eat apple
                if (grid[snakeX, snakeY] == -1)
                {   Debug.Log("Ate an apple!");
                    GameObject toDestroy = GameObject.Find("Apple");
                    Destroy(toDestroy);
                    snakeScore++;
                    scoreText.text = "Score: " + snakeScore.ToString();

                    for (int i = 0; i < grid.GetLength(0); i++)
                    {
                        for (int j = 0; j < grid.GetLength(1); j++)
                        {
                            if(grid[i,j] > 0){
                                grid[i,j]++;
                            }
                        } 
                    }
                    bool appleCreated = false;
                    while(!appleCreated)
                    {
                        int x = UnityEngine.Random.Range(0,grid.GetLength(0));
                        int y = UnityEngine.Random.Range(0,grid.GetLength(1));

                        if(grid[x,y] == 0){
                            grid[x,y] = -1;
                            GameObject apple = Instantiate(applePrefab) as GameObject;
                            apple.transform.position = new Vector3(x*1.0f + 0.5f, y*1.0f + 0.5f,5.0f);
                            apple.name = "Apple";
                            appleCreated = true;
                        }
                    }
                    

                    

                }
                else if (grid[snakeX, snakeY] != 0)
                {
                    hasLost = true;
                    Debug.Log("You lose.");

                    return;
                }
 
                //Move
                snakeTransform.position += direction;
                grid[snakeX, snakeY] = snakeScore;
            }
        }
 
 
 
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Vector3.up;
            Debug.Log("move up");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Vector3.left;
            Debug.Log("move left");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector3.down;
            Debug.Log("move down");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector3.right;
            Debug.Log("move right");
        }
    }

        public void OnBackButtonClick()
    {
        Debug.Log("Clicked PLay Button");
        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuScene");
    }
}
