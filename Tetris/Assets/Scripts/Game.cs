using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    public static int gridWidth = 10;
    public static int gridHeight = 20;

    public static Transform[,] grid = new Transform[gridWidth, gridHeight];

    public int score1 = 50;
    public int score2 = 100;
    public int score3 = 300;
    public int score4 = 1000;
    private int CurNumRow = 0;
    public Text hud_Score;
    public int curScore = 0;
    int goalscore = 100;
    float speed = 1;
	// Use this for initialization
	void Start () {
        spawnNextTetromino();
	}
    void Update()
    {
        UpdateScore();
        UpdateHud();
        FallIncrease();
    }
    public void UpdateHud()
    {
        hud_Score.text = curScore.ToString();
    }
    public void UpdateScore()
    {
        if(CurNumRow > 0)
        {
            if (CurNumRow == 1)
            {
                curScore += score1;
            }
            else if (CurNumRow == 2)
            {
                curScore += score2;
            }
            else if (CurNumRow == 3)
            {
                curScore += score3;
            }
            else if (CurNumRow == 4)
            {
                curScore += score4;
            }
            CurNumRow = 0;
        }
    }
    public bool isFullRow (int y)
    {
        for(int x = 0; x < gridWidth; x++)
        {
            if(grid[x,y] == null)
            {
                return false;
            }
        }
        CurNumRow++;
        return true;
    }
    public void DeleteMeno(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }
    public void MoveRowDown(int y)
    {
        for (int x = 0; x < gridWidth; x++)
        {
            if(grid[x,y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    public bool AboveGrid(Tetromino tet)
    {
        for(int x = 0; x < gridWidth; x++)
        {
            foreach(Transform mino in tet.transform)
            {
                Vector2 pos = Round(mino.position);
                if(pos.y > gridHeight - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void MoveAllDown(int y)
    {
        for(int i = y; i < gridHeight; i++)
        {
            MoveRowDown(i);
        }
    }
    public void DeleteRow()
    {
        for(int y = 0; y < gridHeight; y++)
        {
            if (isFullRow(y))
            {
                DeleteMeno(y);
                MoveAllDown(y + 1);
                --y;            
            }
        }
    }
    public void updateGrid(Tetromino tet)
    {
        for(int i = 0; i < gridHeight; i++)
        {
            for(int j = 0; j <gridWidth; j++)
            {
                if(grid[j , i] != null)
                {
                    if(grid[j,i].parent == tet.transform)
                    {
                        grid[j, i] = null;
                    }
                }
            }
        }
        foreach(Transform mino in tet.transform)
        {
            Vector2 pos = Round(mino.position);
            if(pos.y < gridHeight)
            {
                grid[(int)pos.x, (int)pos.y] = mino;
            }
        }
    }
    public Transform GetTransform (Vector2 pos)
    {
        if(pos.y > gridHeight - 1)
        {
            return null;
        }
        else
        {
            return grid[(int)pos.x, (int)pos.y];
        }
    }
    public void spawnNextTetromino()
    {
        GameObject nextTetromino = (GameObject)Instantiate(Resources.Load(GetRandomTetromino(), typeof(GameObject)), new Vector2(5.0f, 20.0f), Quaternion.identity);
    
    }
    public bool Bounds (Vector2 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < gridWidth && (int)pos.y >= 0);
    }
    public Vector2 Round (Vector2 pos)
    {
        return new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y));
    }
    string GetRandomTetromino()
    {
        int randomTetromino = Random.Range(1, 8);
        string randomTetName = "Prefabs/T";

        switch (randomTetromino)
        {
            case 1:
                randomTetName = "Prefabs/T";
                break;
            case 2:
                randomTetName = "Prefabs/L";
                break;
            case 3:
                randomTetName = "Prefabs/S";
                break;
            case 4:
                randomTetName = "Prefabs/GameObject";
                break;
            case 5:
                randomTetName = "Prefabs/Long";
                break;
            case 6:
                randomTetName = "Prefabs/Square";
                break;
            case 7:
                randomTetName = "Prefabs/Z";
                break;
            
        }
        return randomTetName;
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public int Slam(Vector3 pos)
    {
        for (int i = 0; i < gridHeight; i++)
        {

        }
        return 0;
    }
    public float FallIncrease()
    {
        if (curScore > goalscore)
        { 
            goalscore += 100;
            speed *= .1f;
        }
            return speed;
      
    }
}
