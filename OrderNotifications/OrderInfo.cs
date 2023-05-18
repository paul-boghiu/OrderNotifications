namespace OrderNotifications
{
	/// <summary>
	/// Order info wrapper.
	/// </summary>
	[Serializable]
	public class OrderInfo
	{
		/// <summary>
		/// The order ID.
		/// </summary>
		public int OrderId { get; set; }

		/// <summary>
		/// The order state.
		/// </summary>
		public State State { get; set; }
	}
}