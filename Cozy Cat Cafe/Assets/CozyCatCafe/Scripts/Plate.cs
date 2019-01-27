using System.Collections.Generic;
using CozyCatCafe.Scripts;
using UnityEngine;

public class Plate : MonoBehaviour
{
	public PlayerStats player;
	public PlateRecipe RecipeList;
	public readonly HashSet<Food> onPlate = new HashSet<Food>();

	public SpriteRenderer FoodDisplay;

	private Food _dish;

	public Food dishToDisplay
	{
		get => _dish;
		private set
		{
			_dish = value;
			if (FoodDisplay != null)
				FoodDisplay.sprite = value.Sprite;
		}
	}

	void OnMouseDown()
	{
		if (player.holding != null)
		{
			putFood();
		}
		else if (dishToDisplay != null)
		{
			player.holding = dishToDisplay;
			onPlate.Clear();
			dishToDisplay = null;
		}
	}

	void putFood()
	{
		var toAdd = player.holding;
		onPlate.Add(toAdd);
		player.holding = null;

		if (onPlate.Count <= 0)
			dishToDisplay = null;
		else if (onPlate.Count <= 1)
			dishToDisplay = toAdd;
		else
		{
			var (bestFood, matchPercentage) = RecipeList.GetBestFood(onPlate);
			dishToDisplay = bestFood;
		}
	}
}