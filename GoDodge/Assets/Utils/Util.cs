﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Utils
{
	class Util
	{
		public static void ShowAndroidToastMessage(string message)
		{
			//AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			//AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

			//if (unityActivity != null)
			//{
			//	AndroidJavaClass toastClass = new AndroidJavaClass("android.widget.Toast");
			//	unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
			//	{
			//		AndroidJavaObject toastObject = toastClass.CallStatic<AndroidJavaObject>("makeText", unityActivity, message, 0);
			//		toastObject.Call("show");
			//	}));
			//}

			Debug.Log(message);
		}
	}
}
