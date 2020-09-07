using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MapModel
{
    public int mapID;
    public string mapName;
    public int mapMaxLevel;
    public bool unlocked;

    public MapModel(int id, string mapName, int maxLevel, bool unlocked)
    {
        mapID = id;
        this.mapName = mapName;
        mapMaxLevel = maxLevel;
        this.unlocked = unlocked;
    }

    public override string ToString()
    {
        return $"Map ID: {mapID} - Map Name: {mapName} - Max Level: {mapMaxLevel} - Unlocked: {unlocked}";
    }
}
