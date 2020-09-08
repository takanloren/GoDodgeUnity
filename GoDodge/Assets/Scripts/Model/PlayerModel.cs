using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerModel
{
    public int id;
    public int coins;
    public int shields;
    public int speed_potion;

    public PlayerModel(int id, int coins, int shields, int speed_potion)
    {
        this.id = id;
        this.coins = coins;
        this.shields = shields;
        this.speed_potion = speed_potion;
    }
}
