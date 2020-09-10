using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Utils
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		public bool dontDestroy = false;

		static T m_instance;

		public static T INSTANCE
		{
			get
			{
				if(m_instance == null)
				{
					m_instance = GameObject.FindObjectOfType<T>();

					if(m_instance == null)
					{
						GameObject singleton = new GameObject(typeof(T).Name);
						m_instance = singleton.AddComponent<T>();
					}
				}

				return m_instance;
			}
		}

		public virtual void Awake()
		{
			if(m_instance == null)
			{
				m_instance = this as T;

				if (dontDestroy)
				{
					transform.parent = null;
					DontDestroyOnLoad(this.gameObject);
				}
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}
