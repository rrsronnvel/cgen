using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public string unitName;
	public int unitLevel;

	public float damage;

	public float maxHP;
	public float currentHP;

	public bool TakeDamage(float dmg)
	{
		currentHP -= dmg;

		// Update game data with new health
		if (DataPersistenceManager.instance != null)
		{
			GameData gameData = DataPersistenceManager.instance.GetGameData();
			if (gameData != null)
			{
				gameData.currentHealth = currentHP;
			}
		}

		if (currentHP <= 0)
			return true;
		else
			return false;
	}


	

	public void LoadHealth()
	{
		if (DataPersistenceManager.instance != null)
		{
			GameData gameData = DataPersistenceManager.instance.GetGameData();
			if (gameData != null)
			{
				currentHP = gameData.currentHealth;
			}
		}
	}

}
