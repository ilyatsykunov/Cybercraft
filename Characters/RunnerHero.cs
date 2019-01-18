using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerHero : Hero
{
    public GameObject missionTarget;

    protected override void Start()
    {
        initialStrength = 2;
        initialAiming = 4;
        initialSpeed = 5;
        base.Start();
        spottingDistance += 5f;
    }
    protected override void Update()
    {
        base.Update();

    }
    protected void SpotMissionTargets()
    {
        if(Vector3.Distance(gameObject.transform.position, missionTarget.transform.position) <= spottingDistance)
        {

        }
    }
}
