using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    [SerializeField] private GameObject humanPrefab = null;
    private List<Human_AI> humans = new List<Human_AI>();

    private int score = 0;
    private int currentWave = 0;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);

        TriggerNextWave(1);
        currentWave++;
    }

    public void TriggerNextWave(int waveNumber)
    {
        for (int i = 0; i < Mathf.RoundToInt(Mathf.Clamp((Mathf.Sin(waveNumber) + 1) / 2, 0.1f, 1f) * waveNumber * 10f); i++)
        {
            humans.Add(Instantiate(humanPrefab, Vector3.zero, Quaternion.identity).GetComponent<Human_AI>());
        }
    }

    public void DeleteHuman(Human_AI deadHumanAI)
    {
        score++;
        humans.Remove(deadHumanAI);
        Destroy(deadHumanAI.gameObject);

        if (humans.Count == 0)
        {
            TriggerNextWave(currentWave);
            currentWave++;
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
