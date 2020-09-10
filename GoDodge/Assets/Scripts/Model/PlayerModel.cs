using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PlayerModel
{
	public int id;
	private int coins;
	private int shields;
	private int speed_potion;

	public PlayerModel(int id, int coins, int shields, int speed_potion)
	{
		this.id = id;
		this.coins = coins;
		this.shields = shields;
		this.speed_potion = speed_potion;
	}

	public int GemAmount
	{
		get
		{
			return coins;
		}

		set
		{
			coins = value;

			SavePlayerEquipment();
		}

	}

	public int ShieldAmount
	{
		get
		{
			return shields;
		}

		set
		{
			shields = value;

			SavePlayerEquipment();
		}
	}

	public int SpeedPotionAmount
	{
		get
		{
			return speed_potion;
		}

		set
		{
			speed_potion = value;

			SavePlayerEquipment();
		}
	}

	private void SavePlayerEquipment()
	{
		SQLiteHelper.INSTANCE.UpdatePlayerEquipment(this);
	}
}
