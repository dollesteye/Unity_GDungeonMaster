using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Game : MonoBehaviour {

	protected static Game			m_instance 	  = null;
	Dictionary<string, IManager>	m_dicManager  = null;

	public static Game Instance
	{
		get
		{
			if(m_instance == null)
			{
				GameObject obj = GameObject.Find("Game");
				m_instance	   = obj.GetComponent<Game>();
				GameObject.DontDestroyOnLoad(obj);
			}

			return m_instance;
		}
	}

	public T GetManager<T>() where T : IManager
	{
		if(m_dicManager == null)
		{
			m_dicManager = new Dictionary<string, IManager>();
		}

		Type type = typeof(T); 
		if(m_dicManager.ContainsKey(type.Name) == false)
		{
			IManager manager = Activator.CreateInstance(type) as IManager;
			manager.Init();
			m_dicManager.Add(type.Name, manager);
		}

		return (T)m_dicManager[type.Name];
	}

	public void Clear()
	{
		if(m_dicManager != null)
		{
			foreach(KeyValuePair<string, IManager> pair in m_dicManager)
			{
				if(pair.Value != null)
				{
					pair.Value.Clear();
				}
			}

			m_dicManager.Clear();
		}
		m_dicManager = null;
	}
}
