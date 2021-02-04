using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    [SerializeField] private GameObject humanPrefab = null;
    private List<Human_AI> humans = new List<Human_AI>();
    [Space]
    [SerializeField] private TextMeshProUGUI scoreText = null;

    private float timer;
    [SerializeField] float roundTimeLimit = 15;
    private int score = 0;
    private int currentWave = 0;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Debug.LogWarning("Had to destroy an GameManager");
            Destroy(this);
        }
    }

    public void Start()
    {
        scoreText.text = score.ToString("0000");

        TriggerNextWave();
        timer = roundTimeLimit;
    }

    public void TriggerNextWave()
    {
        currentWave++;
        for (int i = 0; i < Mathf.RoundToInt(Mathf.Clamp((Mathf.Sin(currentWave) + 1) / 2, 0.1f, 1f) * currentWave * 10f); i++)
        {
            humans.Add(Instantiate(humanPrefab, Vector3.zero, Quaternion.identity).GetComponent<Human_AI>());
            timer = roundTimeLimit;
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            // Game Over 
        }
    }

    public void DeleteHuman(Human_AI deadHumanAI)
    {
        score += 10;
        scoreText.text = score.ToString("0000");

        humans.Remove(deadHumanAI);

        if (humans.Count == 0)
        {
            Invoke("TriggerNextWave", 2f);
        }
    }

    public void SmashAt(float position)
    {
        for (int i = 0; i < humans.Count; i++)
        {
            humans[i].HammerSmash(position);
        }
    }
}
