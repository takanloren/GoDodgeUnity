using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
	#region Map Level Prefix
	public static string DUNGEON_LEVEL_PREFIX = "Dungeon_Level_";
	#endregion

	#region SQLITE TABLE
	public static string SQLITE_MAP_TABLE = "player_data_table";
	public static string SQLITE_PLAYER_MAP_DATA_TABLE = "player_map_data_table";
	public static string SQLITE_PLAYER_TABLE = "player_table";
	#endregion

	#region SQLITE COLUMNS
	public static string SQLITE_MAP_ID = "map_id";
	public static string SQLITE_MAP_NAME = "map_name";
	public static string SQLITE_MAP_MAX_LEVEL = "max_level";
	public static string SQLITE_MAP_UNLOCKED = "unlocked";

	public static string SQLITE_MAP_FINISHED_LEVEL = "finished_level";
	public static string SQLITE_MAP_BEST_TIME = "best_time";

	public static string SQLITE_PLAYER_ID = "player_id";
	public static string SQLITE_PLAYER_COINS = "dodge_coins";
	public static string SQLITE_PLAYER_SHIELDS = "shields";
	public static string SQLITE_PLAYER_SPEED_POTION = "speed_potion";
	#endregion

	#region MAP ID
	public static int MAP_DUNGEON_ID = 1;

	#endregion

	#region ADS
	public static string GOOGLE_PLAY_GAME_ID = "3808815";
	public static string Shield_Reward_PlacementID = "shieldReward";
	public static string SpeedPotion_Reward_PlacementID = "speedPotionReward";
	public static string Gem_Reward_PlacementID = "gemReward";
	public static string Respawn_Reward_PlacementID = "respawnReward";
	#endregion

	#region PLAYER STATS
	public static int CONST_PLAYER_ID = 1;
	public static float PLAYER_NORMAL_SPEED = 3f;
	public static float PLAYER_BOOSTED_SPEED = 5f;

	public static string PLAYER_EFFECT_SHIELD = "SHIELD";
	public static string PLAYER_EFFECT_SPEED = "SPEED";

	public static float SHIELD_TIME = 3;
	public static float SPEED_POTION_TIME = 3;


	#endregion

	#region IAP - SHOP
	public static int GEM_PURCHASE_099_EARN = 500;
	public static int GEM_PURCHASE_499_EARN = 3000;
	public static int GEM_PURCHASE_999_EARN = 8000;

	public static int GEM_ADS_REWARD = 50;
	public static int SHIELD_PRICE = 100;
	public static int SPEED_POTION_PRICE = 80;
	public static int BASE_GEM_EARN_PER_LEVEL = 4;
	#endregion





}
