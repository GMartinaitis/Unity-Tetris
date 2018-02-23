using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tetromino : MonoBehaviour {

    float fall = 0;
    public float fallfatcock;
    public bool allowRotation = true;
    public bool limitRotation = false;
    int score = 0;
	// Use this for initialization
	void Start () {
        fallfatcock = FindObjectOfType<Game>().FallIncrease();
    }
	
	// Update is called once per frame
	void Update () {
        CheckUserInput ();
        FallSpeed();
    }
    void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {

            transform.position += new Vector3(1, 0, 0);
 
            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().updateGrid(this);
            } else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            transform.position += new Vector3(-1, 0, 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().updateGrid(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (allowRotation)
            {
                if (limitRotation)
                {
                    if (transform.rotation.eulerAngles.z >= 90)
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }
                }

                else
                {
                    transform.Rotate(0, 0, 90);
                }
                if (CheckIsValidPosition())
                {
                    FindObjectOfType<Game>().updateGrid(this);
                }
                else
                {
                    if (limitRotation)
                    {
                        if (transform.rotation.eulerAngles.z >= 90)
                        {
                            transform.Rotate(0, 0, -90);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }
                    }else
                    {
                        transform.Rotate(0, 0, -90);
                    }
                    
                }
                
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -1, 0); 
            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().updateGrid(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                FindObjectOfType<Game>().DeleteRow();
                if (FindObjectOfType<Game>().AboveGrid(this))
                {
                    FindObjectOfType<Game>().GameOver();
                }
                enabled = false;

                FindObjectOfType<Game>().spawnNextTetromino();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
          
            transform.position += new Vector3(0, FindObjectOfType<Game>().Slam(transform.position), 0);

            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().updateGrid(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                FindObjectOfType<Game>().DeleteRow();
                if (FindObjectOfType<Game>().AboveGrid(this))
                {
                    FindObjectOfType<Game>().GameOver();
                }
                enabled = false;

                FindObjectOfType<Game>().spawnNextTetromino();
            }
        }
    }
    void FallSpeed()
    {
        if (Time.time - fall >= fallfatcock)
        { 
            transform.position += new Vector3(0, -1, 0);
            fall = Time.time;
            if (CheckIsValidPosition())
            {
                FindObjectOfType<Game>().updateGrid(this);
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                FindObjectOfType<Game>().DeleteRow();
                if (FindObjectOfType<Game>().AboveGrid(this))
                {
                    FindObjectOfType<Game>().GameOver();
                }
                enabled = false;
                FindObjectOfType<Game>().spawnNextTetromino();
            }
        }
    }
    bool CheckIsValidPosition()
    {
        foreach(Transform mino in transform)
        {

            Vector2 pos = FindObjectOfType<Game>().Round(mino.position);
            if (FindObjectOfType<Game>().Bounds (pos) == false)
            {
                return false;
            }
            if(FindObjectOfType<Game>().GetTransform(pos) != null && FindObjectOfType<Game>().GetTransform(pos).parent != transform)
            {
                return false;
            }
            
        }
        return true;
    }
}
