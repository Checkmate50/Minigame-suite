using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

public class TankManager : GameManager {

    [SerializeField]
    private TankInterface tankInterfacePrefab;
    [SerializeField]
    private GameObject wallPrefab;
    [SerializeField]
    private Tank[] playerPrefabs;
    [SerializeField]
    private TextAsset[] levels;
    [SerializeField]
    private int seconds;

    private TankInterface tankInterface;

    private int playerCount;
    private List<GameObject> wallInstances;
    private Tank[] playerInstances;
    private Space[,] board;
    private int[] scores;

    public void changeScore(int amount, int player) {
        scores[player - 1] += amount;
        tankInterface.updateScores();
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
        playerCount = playerPrefabs.Length; //TODO: Remove
        resetScores();
        wallInstances = new List<GameObject>();
        playerInstances = new Tank[playerCount];
        createBoard(0);
        setUpInterface();
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
        float wallWidth = wallPrefab.GetComponent<BoxCollider2D>().size.x;
        float wallHeight = wallPrefab.GetComponent<BoxCollider2D>().size.y;
        float xBound = 10f;
        float yBound = 4f;
        board = new Space[(int)(xBound * 2 / wallWidth), (int)(yBound * 2 / wallHeight)];
        for (int i = 0; i < board.GetLength(0); i++)
            for (int j = 0; j < board.GetLength(1); j++)
                board[i, j] = new Space(i * wallWidth - xBound + wallWidth / 2, yBound - j * wallHeight - wallHeight / 2);

        //Add borders
        Vector3 pos;
        pos = new Vector3(xBound + wallWidth / 2, yBound + wallHeight / 2);
        addWall(pos);
        pos = new Vector3(-xBound - wallWidth / 2, yBound + wallHeight / 2);
        addWall(pos);
        pos = new Vector3(xBound + wallWidth / 2, -yBound - wallHeight / 2);
        addWall(pos);
        pos = new Vector3(-xBound - wallWidth / 2, -yBound - wallHeight / 2);
        addWall(pos);
        for (int i = 0; i < board.GetLength(0); i++) {
            pos = new Vector3(board[i, 0].x, yBound + wallHeight / 2);
            addWall(pos);
            pos = new Vector3(board[i, 0].x,  -yBound - wallHeight / 2);
            addWall(pos);
        }
        for (int i = 0; i < board.GetLength(1); i++) {
            pos = new Vector3(xBound + wallWidth / 2, board[0, i].y);
            addWall(pos);
            pos = new Vector3(-xBound - wallWidth / 2, board[0, i].y);
            addWall(pos);
        }

        //Read level
        string[] lines = levels[level].text.Split('\n');
        for (int i = 0; i < lines.Length; i++) {
            for (int j = 0; j < lines[i].Length; j++)
                if (lines[i][j] == '1')
                    addWall(j, i);
        }

        //Load players
        Space space = board[0, 0];
        pos = new Vector3(space.x, space.y);
        Quaternion rot = Quaternion.Euler(0f, 0f, -135f);
        playerInstances[0] = (Tank) Instantiate(playerPrefabs[0], pos, rot);
        playerInstances[0].Initialize(this);

        space = board[board.GetLength(0) - 1, board.GetLength(1) - 1];
        pos = new Vector3(space.x, space.y);
        rot = Quaternion.Euler(0f, 0f, 45f);
        playerInstances[1] = (Tank) Instantiate(playerPrefabs[1], pos, rot);
        playerInstances[1].Initialize(this);
    }

    private void addWall(int row, int col) {
        Space space = board[row, col];
        Vector3 pos = new Vector3(space.x, space.y);
        addWall(pos);
        space.reserved = true;
    }

    private void addWall(Vector3 pos) {
        wallInstances.Add((GameObject)Instantiate(wallPrefab, pos, Quaternion.identity));
    }

    private void setUpInterface() {
        tankInterface = Instantiate(tankInterfacePrefab);
        tankInterface.TimeUp += tankInterface_TimeUp;
        tankInterface.setUpTimer(new Vector3(0f, -190f), DateTime.Now.AddSeconds(seconds));
        Color[] colors = new Color[2];
        colors[0] = Color.red;
        colors[1] = Color.blue;
        Vector3[] positions = new Vector3[2];
        positions[0] = new Vector3(-150f, -190f);
        positions[1] = new Vector3(150f, -190f);
        tankInterface.setUpScoreDisplay(positions, colors, scores);
    }

    private void tankInterface_TimeUp(object sender, EventArgs e) {
        endGame();
    }

    protected void Update() {
        if (Input.GetKeyDown("escape"))
            if (SceneManager.GetActiveScene().name == game)
                endGame();
    }
}
