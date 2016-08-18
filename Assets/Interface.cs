using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public abstract class Interface : MonoBehaviour {

    [SerializeField]
    protected Text interfaceText;

    protected Transform canvasTransform;

    protected Text clock;
    protected DateTime refTime;

    protected int[] scores;
    protected Text[] scoreText;

    public delegate void TimeUpHandler(object sender, EventArgs e);
    public event TimeUpHandler TimeUp;

    public void setUpTimer(Vector3 pos, DateTime refTime) {
        setUpTimer(pos, Color.white, refTime);
    }

    public void setUpTimer(Vector3 pos, Color color, DateTime refTime) {
        this.refTime = refTime;

        clock = Instantiate(interfaceText);
        clock.color = color;
        RectTransform rect = clock.GetComponent<RectTransform>();
        rect.SetParent(canvasTransform, false);
        rect.localPosition = pos;
    }

    public void setUpScoreDisplay(Vector3[] positions, int[] scores) {
        Color[] colors = new Color[positions.Length];
        for (int i = 0; i < positions.Length; i++)
            colors[i] = Color.white;
        setUpScoreDisplay(positions, scores);
    }

    public void setUpScoreDisplay(Vector3[] positions, Color[] colors, int[] scores) {
        this.scores = scores;
        scoreText = new Text[positions.Length];
        for (int i = 0; i < scoreText.Length; i++) {
            scoreText[i] = Instantiate(interfaceText);
            scoreText[i].color = colors[i];
            RectTransform rect = scoreText[i].GetComponent<RectTransform>();
            rect.SetParent(canvasTransform, false);
            rect.localPosition = positions[i];
        }
        updateScores();
    }

    public void updateScores() {
        for(int i = 0; i < scores.Length; i++)
            scoreText[i].text = scores[i].ToString();
    }

    protected virtual void Awake () {
        canvasTransform = GameObject.Find("Canvas").transform;
    }

    protected virtual void Update () {
        updateTime();
    }

    protected void updateTime() {
        TimeSpan ts = DateTime.Now.Subtract(refTime).Duration();
        clock.text = new DateTime(ts.Ticks).ToString("mm:ss");
        if (Math.Abs(ts.Ticks) < 100000L) {
            TimeUp(this, null);
        }
    }
}