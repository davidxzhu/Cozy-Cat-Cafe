using System;
using System.Collections.Generic;
using CozyCatCafe.Scripts;
using UnityEngine;

public class Plate : MonoBehaviour
{
	public PlayerStats player;
	public PlateRecipe RecipeList;
	public readonly HashSet<Food> onPlate = new HashSet<Food>();
	public Food dishToDisplay;

	void OnMouseDown()
	{
		if (player.holding != null)
		{
			putFood();
		}
		else if (dishToDisplay != null)
		{
			player.holding = dishToDisplay;
		}
	}

	void putFood()
	{
		onPlate.Add(player.holding);
		player.holding = null;
		
		var (bestFood, matchPercentage) = RecipeList.GetBestFood(onPlate);
		dishToDisplay = bestFood;
	}
}