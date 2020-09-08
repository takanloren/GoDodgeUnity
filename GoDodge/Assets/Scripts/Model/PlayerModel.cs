using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerModel
{
    public int coins;
    public int shields;
    public int speed_potion;

    public PlayerModel(int coins, int shields, int speed_potion)
    {
        this.coins = coins;
        this.shields = shields;
        this.speed_potion = speed_potion;
    }
}
