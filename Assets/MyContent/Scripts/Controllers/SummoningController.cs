using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummoningController : BaseController
{
    public Enemy minion;
    public Transform[] summonPoints;

    void Awake()
    {
        base.Awake();
    }

    public void Summon()
    {
        foreach (Transform summonPoint in summonPoints)
        {
            Instantiate(minion, summonPoint.position, summonPoint.rotation);
        }
    }
}
