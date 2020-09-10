using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAttempt
{
    private GameManager.Map _map;
    private DateTime _startTime;
    private DateTime _endTime;
    private int _finishedLevel = 1;
    private int _totalTimeRevival;
	private bool _isGameOver;
    private GameManager.BuffEffects _activeBuffEffect = GameManager.BuffEffects.OnNormal;
	private int _gemEarned;

    public RunAttempt(GameManager.Map map, DateTime startTime)
    {
        _map = map;
        _startTime = startTime;
    }

    public GameManager.Map Map
    {
        get
        {
            return _map;
        }

        set
        {
            _map = value;
        }
    }

    public GameManager.BuffEffects ActiveBuffEffect
    {
        get
        {
            return _activeBuffEffect;
        }

        set
        {
            _activeBuffEffect = value;
        }
    }

    public DateTime StartTime
    {
        get
        {
            return _startTime;
        }

        set
        {
            _startTime = value;
        }
    }

    public DateTime EndTime
    {
        get
        {
            return _endTime;
        }

        set
        {
            _endTime = value;
        }
    }

    public TimeSpan TotalTimeCost
    {
        get
        {
            return _endTime - _startTime;
        }
    }

    public int FinishedLevel
    {
        get
        {
            return _finishedLevel;
        }

        set
        {
            _finishedLevel = value;
        }
    }

    public int TotalTimeRevival
    {
        get
        {
            return _totalTimeRevival;
        }

        set
        {
            _totalTimeRevival = value;
        }
    }

	public bool IsGameOver
	{
		get
		{
			return _isGameOver;
		}

		set
		{
			_isGameOver = value;
		}
	}

	public int GemEarned
	{
		get
		{
			return GameManager.Instance.CalculateGemEarned(_finishedLevel);
		}
	}
}
