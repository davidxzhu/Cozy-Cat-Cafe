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
				FoodDisplay.sprite = value == null ? null : value.Sprite;
		}
	}

	public int PlayerIndex;

	void OnMouseDown()
	{
		if (player.holding != null)
		{
			if (onPlate.Contains(player.holding))
			{
				SoundMaster.Play(SoundMaster.Type.Invalid);
			}
			else
			{
				PlayerVisibility.Instance.Select(PlayerIndex);
				SoundMaster.Play(SoundMaster.Type.Item);
				putFood();
			}
		}
		else if (dishToDisplay != null)
		{
			PlayerVisibility.Instance.Select(PlayerIndex);
			SoundMaster.Play(SoundMaster.Type.Item);
			player.holding = dishToDisplay;
			onPlate.Clear();
			dishToDisplay = null;
		}
		else
		{
			SoundMaster.Play(SoundMaster.Type.Invalid);
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