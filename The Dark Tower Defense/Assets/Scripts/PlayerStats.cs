using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money;
    public int startMoney = 400;

    public static int Lives;
    public int startLives = 20;

    public static int Rounds;

    public static int Wood;
    public int startWood = 5;
    public static int Stone;
    public int startStone = 5;
    public static int Iron;
    public int startIron = 0;

    public Worker worker;
    public GameObject HQ;

    // Start is called before the first frame update
    void Start()
    {
        Money = startMoney;
        Lives = startLives;

        Rounds = 0;

        Wood = 0;
        Stone = 0;
        Iron = 0;
    }

    public void SummonWorker(Resource targetResource, Transform targetLocation)
    {
        //Instantiate Worker, send them to the resource
        Worker temp = (Worker)Instantiate(worker, HQ.transform.position, Quaternion.identity);
        temp.SetResource(targetResource, targetLocation, HQ.transform);

        Lives--;

        Debug.Log("Summoned Worker");
    }


}
