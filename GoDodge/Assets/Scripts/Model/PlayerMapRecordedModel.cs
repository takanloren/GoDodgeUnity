using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerMapRecordedModel
{
    public int mapID;
    public int finishedLevel;
    public TimeSpan bestTime;

    public PlayerMapRecordedModel(int id, int finishedLevel, TimeSpan bestTime)
    {
        mapID = id;
        this.finishedLevel = finishedLevel;
        this.bestTime = bestTime;
    }
}
