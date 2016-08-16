using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class TankManager : GameManager {

    [SerializeField]
    private GameObject wall;
    [SerializeField]
    private Tank[] players;
    [SerializeField]
    private TextAsset[] levels;
    [SerializeField]
    private Scene num;

    private int playerCount;
    private List<GameObject> wallInstances;
    private GameObject[] playerInstances;
    private Space[,] board;
    private int[] scores;

    public void changeScore(int player, int amount) {
        scores[player - 1] += amount;
    }

    public int getScore(int player) {
        return scores[player - 1];
    }

    public void resetScores() {
        scores = new int[playerCount];
        for (int i = 0; i < scores.Length; i++)
            scores[i] = 0;
    }

    protected override void enterGame() {
        playerCount = players.Length; //TODO: Remove
        resetScores();
        wallInstances = new List<GameObject>();
        playerInstances = new GameObject[playerCount];
        Debug.Log("Creating board");
        createBoard(0);
    }

    protected override void enterMenu() {}

    private struct Space
    {
        public float x;
        public float y;
        public bool reserved;
        public Space(float x, float y) {
            this.x = x;
            this.y = y;
            reserved = false;
        }
    }

    private void createBoard(int level) {
        //Setup board
        float wallWidth = wall.GetComponent<BoxCollider2D>().size.x;
        float wallHeight = wall.GetComponent<BoxCollider2D>().size.y;
        float xBound = 10f;
        float yBound = 4f;
        board = new Space[(int)(xBound * 2 / wallWidth), (int)(yBound * 2 / wallHeight)];
        Debug.Log(board.GetLength(0));
        Debug.Log(board.GetLength(1));
        for (int i = 0; i < board.GetLength(0); i++)
            for (int j = 0; j < board.GetLength(1); j++)
                board[i, j] = new Space(i * wallWidth - xBound, j * wallHeight - yBound);

        //Add borders
        for (int i = 0; i < board.GetLength(0); i++) {
            Vector3 pos = new Vector3(board[i, 0].x, yBound + wallHeight);
            addWall(pos);
            pos = new Vector3(board[i, 0].x,  -yBound - wallHeight);
            addWall(pos);
        }
        for (int i = 0; i < board.GetLength(1); i++) {
            Vector3 pos = new Vector3(xBound + wallHeight, board[0, i].y);
            addWall(pos);
            pos = new Vector3(-xBound - wallHeight, board[0, i].y);
            addWall(pos);
        }

        //Read level
        string[] lines = levels[level].text.Split('\n');
        for (int i = 0; i < lines.Length; i++) {
            Debug.Log(lines[i]);
            for (int j = 0; j < lines[i].Length; j++)
                if (lines[i][j] == '1')
                    addWall(j, i);
        }
    }

    private void addWall(int row, int col) {
        Debug.Log(row + " " + col);
        Space space = board[row, col];
        Vector3 pos = new Vector3(space.x, space.y);
        addWall(pos);
        space.reserved = true;
    }

    private void addWall(Vector3 pos) {
        wallInstances.Add((GameObject)Instantiate(wall, pos, Quaternion.identity));
    }

    protected void Update() {
        if (Input.GetKeyDown("escape"))
            if (SceneManager.GetActiveScene().name == game)
                endGame();
    }
}
