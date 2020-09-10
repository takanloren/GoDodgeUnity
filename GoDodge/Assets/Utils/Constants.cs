using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
	public static string DUNGEON_LEVEL_PREFIX = "Dungeon_Level_";

    public static int MAP_DUNGEON_ID = 1;

    public static string MAP_TABLE = "player_data_table";
    public static string PLAYER_MAP_DATA_TABLE = "player_map_data_table";
    public static string PLAYER_TABLE = "player_table";

    public static string SQLITE_MAP_ID = "map_id";
    public static string SQLITE_MAP_NAME = "map_name";
    public static string SQLITE_MAP_MAX_LEVEL = "max_level";
    public static string SQLITE_MAP_UNLOCKED = "unlocked";

    public static string SQLITE_MAP_FINISHED_LEVEL = "finished_level";
    public static string SQLITE_MAP_BEST_TIME = "best_time";

    public static int PLAYER_ID = 1;
    public static string SQLITE_PLAYER_ID = "player_id";
    public static string SQLITE_PLAYER_COINS = "dodge_coins";
    public static string SQLITE_PLAYER_SHIELDS = "shields";
    public static string SQLITE_PLAYER_SPEED_POTION = "speed_potion";

    public static float PLAYER_NORMAL_SPEED = 3f;
    public static float PLAYER_BOOSTED_SPEED = 5f;

    public static string PLAYER_EFFECT_SHIELD = "SHIELD";
    public static string PLAYER_EFFECT_SPEED = "SPEED";

	public static string GOOGLE_PLAY_GAME_ID = "3808815";
	public static string Shield_Reward_PlacementID = "shieldReward";
	public static string SpeedPotion_Reward_PlacementID = "speedPotionReward";
	public static string Gem_Reward_PlacementID = "gemReward";
	public static string Respawn_Reward_PlacementID = "respawnReward";

	public static int GEM_ADS_REWARD = 50;
	public static int SHIELD_PRICE = 100;
	public static int SPEED_POTION_PRICE = 80;
}
