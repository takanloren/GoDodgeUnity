using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardEntry
{
    public string name;
    public string map;
    public int level;
    public double totalTime;

    public LeaderboardEntry()
    {

    }

    public LeaderboardEntry(string name, string map, int level, double time)
    {
        this.name = name;
        this.map = map;
        this.level = level;
        this.totalTime = time;
    }

    public override string ToString()
    {
        return $"Name: {name} - Level: {level} - TotalTime: {totalTime} - Map: {map}";
    }
}
