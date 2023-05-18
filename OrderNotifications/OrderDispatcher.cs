using System.Collections.Concurrent;

namespace OrderNotifications
{
	/// <summary>
	/// Order dispatcher engine.
	/// </summary>
	public class OrderDispatcher : IStateNotifier
	{
		/// <summary>
		/// State change event.
		/// </summary>
		public event EventHandler<OrderInfo>? StateChange;

		/// <summary>
		/// Order states collection.
		/// </summary>
		private readonly IDictionary<int, State> OrderStates = new ConcurrentDictionary<int, State>();

		/// <summary>
		/// Order dispatcher engine initializer.
		/// </summary>
		public OrderDispatcher()
		{
			StateChange += new(UpdateState);
		}

		/// <summary>
		/// Proocesses new orders.
		/// </summary>
		/// <param name="orderId">The order ID.</param>
		/// <returns>The order processing task.</returns>
		public async Task ProcessOrder(int orderId)
		{
			OrderStates[orderId] = new State();
			await SimulateOrderProcessing(orderId);
		}

		/// <summary>
		/// State update handler.
		/// </summary>
		/// <param name="sender">Ignored parameter.</param>
		/// <param name="orderInfo">The order information.</param>
		private void UpdateState(object? sender, OrderInfo orderInfo)
		{
			OrderStates[orderInfo.OrderId] = orderInfo.State;
		}

		/// <summary>
		/// Retrieves the current order state.
		/// </summary>
		/// <param name="orderId">The order ID.</param>
		/// <returns>The order state.</returns>
		public State? GetOrderState(int orderId)
		{
			OrderStates.TryGetValue(orderId, out State state);

			return state;
		}

		/// <summary>
		/// Order processing simulator.
		/// </summary>
		/// <param name="orderId">The order ID.</param>
		/// <returns>The order processing task.</returns>
		private async Task SimulateOrderProcessing(int orderId)
		{
			var random = new Random();

			var orderInfo = new OrderInfo() { OrderId = orderId };

			// loop the order through the processing states
			for (int i = 0; i < Enum.GetValues<State>().Length; i++)
			{
				orderInfo.State = (State)i;
				StateChange?.Invoke(this, orderInfo);
				await Task.Delay(random.Next(500, 5000));
			}
		}
	}
}
