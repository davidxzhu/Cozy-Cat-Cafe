using System;
using System.Collections.Generic;
using Plugins.CloudCanards.Inspector;
using UnityEngine;

namespace CozyCatCafe.Scripts
{
	public class PlateRecipe : ScriptableObject
	{
		[SerializeField]
		private Food _failedFood;
		
		[SerializeField]
		private List<Recipe> _recipes;

		public (Food bestFood, float matchPercentage) GetBestFood(HashSet<Food> plateFoods)
		{
			if (_recipes == null || _recipes.Count <= 0)
				return (null, 0f);

			Recipe bestRecipe = null;
			var match = 0f;
			
			foreach (var recipe in _recipes)
			{
				var delta = 1f / recipe.Foods.Count;
				var percentage = 0f;
				var missedCount = 0;
				foreach (var food in plateFoods)
				{
					if (recipe.Foods.Contains(food))
						percentage += delta;
					else
						missedCount++;
				}

				percentage *= (plateFoods.Count - missedCount) * 1f / (plateFoods.Count - 1);

				if (match < percentage)
				{
					bestRecipe = recipe;
					match = percentage;
				}

				if (match >= 1f)
					break;
			}

			return (match < 1f ? _failedFood : bestRecipe?.Result, match);
		}

		[Serializable]
		public class Recipe : ISerializationCallbackReceiver
		{
			[SerializeField]
			private Food[] _foods;

			public readonly HashSet<Food> Foods = new HashSet<Food>();
			[Required]
			public Food Result;

			public void OnBeforeSerialize()
			{
				_foods = new Food[Foods.Count];
				var i = 0;
				foreach (var food in Foods)
				{
					_foods[i] = food;
					i++;
				}
			}

			public void OnAfterDeserialize()
			{
				Foods.Clear();

				foreach (var food in _foods)
				{
					Foods.Add(food);
				}

				_foods = null;
			}
		}
	}
}