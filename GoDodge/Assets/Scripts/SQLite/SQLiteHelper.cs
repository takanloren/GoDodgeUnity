using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SQLiteHelper
{
    private IDbConnection dbConn;
    private static SQLiteHelper _instance;
    public static SQLiteHelper INSTANCE
    {
        get
        {
            if(_instance == null)
            {
                _instance = new SQLiteHelper();
            }

            return _instance;
        }
    }

    public void OpenDatabase()
    {
        // Create database
        string connection = "URI=file:" + Application.persistentDataPath + "/" + "GoDodgeDB";

        // Open connection
        dbConn = new SqliteConnection(connection);
        dbConn.Open();
    }

    public void InitAllTables()
    {
        CreateMapTable();
        CreatePlayerMapRecordTable();
        CreatePlayerTable();
    }

    private void CreateMapTable()
    {
        Debug.Log("Creating table Map");
        IDbCommand dbcmd = dbConn.CreateCommand();
        string qCreateMapTable = $"CREATE TABLE IF NOT EXISTS {Constants.MAP_TABLE} ({Constants.SQLITE_MAP_ID} INTEGER PRIMARY KEY, {Constants.SQLITE_MAP_NAME} TEXT, {Constants.SQLITE_MAP_MAX_LEVEL} INTEGER, {Constants.SQLITE_MAP_UNLOCKED} INTEGER DEFAULT 0)";
        //Create map table
        dbcmd.CommandText = qCreateMapTable;
        dbcmd.ExecuteReader();
    }
    private void CreatePlayerMapRecordTable()
    {
        Debug.Log("Creating table PlayerMapRecord");
        IDbCommand dbcmd = dbConn.CreateCommand();
        string qCreatePlayerMapTable = $"CREATE TABLE IF NOT EXISTS {Constants.PLAYER_MAP_DATA_TABLE} ({Constants.SQLITE_MAP_ID} INTEGER PRIMARY KEY, {Constants.SQLITE_MAP_FINISHED_LEVEL} INTEGER DEFAULT 1, {Constants.SQLITE_MAP_BEST_TIME} INTEGER DEFAULT 0)";
        //Create player - map data table
        dbcmd.CommandText = qCreatePlayerMapTable;
        dbcmd.ExecuteReader();
    }
    private void CreatePlayerTable()
    {
        Debug.Log("Creating table Player");
        IDbCommand dbcmd = dbConn.CreateCommand();
        string qCreatePlayerTable = $"CREATE TABLE IF NOT EXISTS {Constants.PLAYER_TABLE} " +
			$"({Constants.SQLITE_PLAYER_ID} INTEGER ," +
			$" {Constants.SQLITE_PLAYER_COINS} INTEGER DEFAULT 0," +
			$" {Constants.SQLITE_PLAYER_SHIELDS} INTEGER DEFAULT 0," +
			$" {Constants.SQLITE_PLAYER_SPEED_POTION} INTEGER DEFAULT 0)";
        //Create player table
        dbcmd.CommandText = qCreatePlayerTable;
        dbcmd.ExecuteReader();
    }

    public void CloseDatabase()
    {
        dbConn.Close();
    }

    public void InsertDataToMapTable(MapModel mapModel)
    {
        // Insert values in table
        IDbCommand cmnd = dbConn.CreateCommand();

        cmnd.CommandText = "INSERT INTO " + Constants.MAP_TABLE
                        + " ( "
                        + Constants.SQLITE_MAP_ID + ", "
                        + Constants.SQLITE_MAP_NAME + ", "
                        + Constants.SQLITE_MAP_MAX_LEVEL + ", "
                        + Constants.SQLITE_MAP_UNLOCKED + " ) "

                        + "VALUES ( "

                        + mapModel.mapID + ", '"
                        + mapModel.mapName + "', "
                        + mapModel.mapMaxLevel + ", "
                        + BoolToInt(mapModel.unlocked) + " )";

        cmnd.ExecuteNonQuery();
    }

    public void InsertDataToPlayerMapTable(PlayerMapRecordedModel model)
    {
        // Insert values in table
        IDbCommand cmnd = dbConn.CreateCommand();

        cmnd.CommandText = "INSERT OR IGNORE INTO " + Constants.PLAYER_MAP_DATA_TABLE
                        + " ( "
                        + Constants.SQLITE_MAP_ID + ", "
                        + Constants.SQLITE_MAP_FINISHED_LEVEL + ", "
                        + Constants.SQLITE_MAP_BEST_TIME + " ) "

                        + "VALUES ( "

                        + model.mapID + ", "
                        + model.finishedLevel + ", "
                        + model.bestTime.TotalMilliseconds + " )";

        cmnd.ExecuteNonQuery();
    }

	public void UpdatePlayerMapRecord(PlayerMapRecordedModel model)
	{
		OpenDatabase();

		IDbCommand cmnd = dbConn.CreateCommand();

		cmnd.CommandText = "UPDATE " + Constants.PLAYER_MAP_DATA_TABLE + " SET "
						+ Constants.SQLITE_MAP_FINISHED_LEVEL + "=" + model.finishedLevel + ", "
						+ Constants.SQLITE_MAP_BEST_TIME + "=" + model.bestTime.TotalMilliseconds
						+ " WHERE " + Constants.SQLITE_MAP_ID + "=" + model.mapID;
		cmnd.ExecuteNonQuery();

		CloseDatabase();
	}


    public void InitMapDataTable()
    {
        InitMapDungeon();
    }

    public void UpdatePlayerEquipment(PlayerModel model)
    {
		OpenDatabase();

        IDbCommand cmnd = dbConn.CreateCommand();

        cmnd.CommandText = "UPDATE " + Constants.PLAYER_TABLE + " SET "
                        + Constants.SQLITE_PLAYER_COINS + "=" + model.coins + ", "
                        + Constants.SQLITE_PLAYER_SHIELDS + "=" + model.shields + ", "
                        + Constants.SQLITE_PLAYER_SPEED_POTION + "=" + model.speed_potion
                        + " WHERE " + Constants.SQLITE_PLAYER_ID + "=" + model.id;
        cmnd.ExecuteNonQuery();

		CloseDatabase();
    }

    public PlayerModel LoadPlayerEquipment()
    {
        IDbCommand cmnd_read = dbConn.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM " + Constants.PLAYER_TABLE;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        PlayerModel player = null;
        while (reader.Read())
        {
            player = new PlayerModel(
               int.Parse(reader[0].ToString()),
               int.Parse(reader[1].ToString()),
               int.Parse(reader[2].ToString()),
               int.Parse(reader[3].ToString())
               );
        }

        return player;
    }

    public void InsertDataToPlayerTable()
    {
        // Insert values in table
        IDbCommand cmnd = dbConn.CreateCommand();

        cmnd.CommandText = "INSERT INTO " + Constants.PLAYER_TABLE
                        + " ( " + Constants.SQLITE_PLAYER_ID + ", "
                        + Constants.SQLITE_PLAYER_COINS + ", "
                        + Constants.SQLITE_PLAYER_SHIELDS + ", "
                        + Constants.SQLITE_PLAYER_SPEED_POTION + " ) "

                        + "VALUES (1, 0, 0, 0) ";
        Debug.Log("Init player equipment: " + cmnd.CommandText);
        cmnd.ExecuteNonQuery();
    }

	public void InitDefaultPlayerRecord()
	{
		
	}

    private void InitMapDungeon()
    {
        Debug.Log("Initializing map DUNGEON");
        MapModel dungeon = new MapModel(Constants.MAP_DUNGEON_ID, GameManager.Map.DUNGEON.ToString(), 40, true);

        if (NeedToAddMapData(GameManager.Map.DUNGEON))
        {
            Debug.Log($"DUNGEON- Adding map data to db");
            InsertDataToMapTable(dungeon);
        }
        else
        {
            Debug.Log("DUNGEON- Already added data");
        }
    }

    private bool NeedToAddMapData(GameManager.Map map)
    {
        Debug.Log("DUNGEON- Checking if need to add");

        IDbCommand cmnd = dbConn.CreateCommand();
        cmnd.CommandText =
            "SELECT * FROM " + Constants.MAP_TABLE + " WHERE " + Constants.SQLITE_MAP_NAME + " ='" + map.ToString() +"'";
        IDataReader reader = cmnd.ExecuteReader();
        while (reader.Read())
        {
            return !reader[1].ToString().Equals(map.ToString());
        }

        return true;
    }

    public List<MapModel> GetAll_MapTable()
    {
        // Read and print all values in table
        IDbCommand cmnd_read = dbConn.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM " + Constants.MAP_TABLE;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<MapModel> map = new List<MapModel>();
        while (reader.Read())
        {
            map.Add(new MapModel(
               int.Parse(reader[0].ToString()),
               reader[1].ToString(),
               int.Parse(reader[2].ToString()),
               IntToBool(int.Parse(reader[0].ToString()))
               ));
        }

        return map;
    }

    public MapModel GetMapTableByMapID(int id)
    {
        // Read and print all values in table
        IDbCommand cmnd_read = dbConn.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM "+ Constants.MAP_TABLE + " WHERE " + Constants.SQLITE_MAP_ID + " =" + id;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        MapModel map = null;
        while (reader.Read())
        {
             map = new MapModel(
                int.Parse(reader[0].ToString()),
                reader[1].ToString(),
                int.Parse(reader[2].ToString()),
                IntToBool(int.Parse(reader[0].ToString()))
                );

            Debug.Log("Read map id " + id + " from DB: "+map.ToString());
        }

        return map;
    }

    public List<PlayerMapRecordedModel> GetAll_PlayerMapRecorded()
    {
        // Read and print all values in table
        IDbCommand cmnd_read = dbConn.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM " + Constants.PLAYER_MAP_DATA_TABLE;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<PlayerMapRecordedModel> data = new List<PlayerMapRecordedModel>();
        while (reader.Read())
        {
            data.Add(new PlayerMapRecordedModel(
               int.Parse(reader[0].ToString()), //ID
               int.Parse(reader[1].ToString()), //Finished level
               TimeSpan.FromMilliseconds(int.Parse(reader[2].ToString())) //Best time
               ));
        }

        return data;
    }

    public PlayerMapRecordedModel GetPlayerMapRecordedByMapID(int mapID)
    {
        // Read and print all values in table
        IDbCommand cmnd_read = dbConn.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM " + Constants.PLAYER_MAP_DATA_TABLE + " WHERE " + Constants.SQLITE_MAP_ID + " =" + mapID;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        PlayerMapRecordedModel data = null;
        while (reader.Read())
        {
            data = new PlayerMapRecordedModel(
               int.Parse(reader[0].ToString()), //ID
               int.Parse(reader[1].ToString()), //Finished level
               TimeSpan.FromMilliseconds(int.Parse(reader[2].ToString())) //Best time
               );
        }

        return data;
    }

    public void DeleteTable(string tableName)
    {
        Debug.Log("Deleting table " + tableName);
        IDbCommand dbcmd = dbConn.CreateCommand();
        dbcmd.CommandText = "DROP TABLE IF EXISTS " + tableName;
        dbcmd.ExecuteNonQuery();
    }

    private int BoolToInt(bool value)
    {
        if (value == true)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    private bool IntToBool(int value)
    {
        if (value == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
