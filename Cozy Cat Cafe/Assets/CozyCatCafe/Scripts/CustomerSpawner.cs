using Plugins.CloudCanards.Inspector;
using UnityEngine;

namespace CozyCatCafe.Scripts
{
	public class CustomerSpawner : MonoBehaviour
	{
		[Required]
		public Food[] OrderList;
		
		[Required]
		public Seat[] Seats;
		public Customers CustomerPrefab;

		public float MinDuration = 1f;
		public float MaxDuration = 5f;

		private float _remainingDuration = 2f;

		private void Update()
		{
			_remainingDuration -= Time.deltaTime;
			if (_remainingDuration < 0f)
			{
				_remainingDuration = Random.Range(MinDuration, MaxDuration);

				for (var i = 0; i < Seats.Length; i++)
				{
					if (Seats[i].Customer == null)
					{
						var obj = Instantiate(CustomerPrefab, transform.position, Quaternion.identity);
						obj.setSeat(Seats[i]);
						obj.orderDish = OrderList[Random.Range(0, OrderList.Length)];
						return;
					}
				}
			}
		}
	}
}