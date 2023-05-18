namespace OrderNotifications
{
	/// <summary>
	/// Order staus monitor.
	/// </summary>
	public class OrderStatusMonitor
	{
		/// <summary>
		/// The order state notifications provider instance.
		/// </summary>
		private readonly IStateNotifier OrderStateNotifier;

		/// <summary>
		/// The order state change event handler.
		/// </summary>
		private readonly EventHandler<OrderInfo> NotificationsHandler = new(OrderStateChangeHandler);

		/// <summary>
		/// Order staus monitor initializer.
		/// </summary>
		/// <param name="stateNotifier">The order state notifications provider.</param>
		public OrderStatusMonitor(IStateNotifier stateNotifier)
		{
			OrderStateNotifier = stateNotifier;

			// subscribe to events
			OrderStateNotifier.StateChange += NotificationsHandler;
		}

		/// <summary>
		/// The handler of order state change events.
		/// </summary>
		/// <param name="sender">Ignored parameter.</param>
		/// <param name="orderInfo">The order information.</param>
		private static void OrderStateChangeHandler(object? sender, OrderInfo orderInfo)
		{
			Console.WriteLine($"{orderInfo.State} order: {orderInfo.OrderId}");
		}

		/// <summary>
		/// Unsubscribes from events when going down.
		/// </summary>
		~OrderStatusMonitor() => OrderStateNotifier.StateChange -= NotificationsHandler;
	}
}
