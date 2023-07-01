using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PickItem
{
    private int _collectedItems;

    public int collectedItems
    {
        get { return _collectedItems; }
        set { _collectedItems = value; }
    }

    [Header("GameObjects for this task")]
    public List<GameObject> items;
    [Header("What should happen after the condition is met")]
    public UnityEvent actionPicks;
    public bool pickComplete { get; set; }
}

[Serializable]
public class KillsEnemy
{
    [Header("Tag enemy for this task")]
    public string nameTag;
    [Header("Jow much kill needs for complete task")]
    public float enemyCount;
    [Header("What should happen after the condition is met")]
    public UnityEvent actionKills;

    private int _counts;

    public int counts
    {
        get { return _counts; }
        set { _counts = value; }
    }

    public bool killsComplete { get; set; }
}

[Serializable]
public struct WhatDo
{
    public bool activeWTFTask;
    [Header("Action after task complete")]
    public UnityEvent action2;
}
