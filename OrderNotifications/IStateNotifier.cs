namespace OrderNotifications
{
	/// <summary>
	/// State change notifier interface.
	/// </summary>
	public interface IStateNotifier
	{
		/// <summary>
		/// State change event.
		/// </summary>
		event EventHandler<OrderInfo> StateChange;
	}
}