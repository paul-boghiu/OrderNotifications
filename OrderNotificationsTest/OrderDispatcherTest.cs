using System.Collections.Concurrent;

namespace OrderNotificationsTest
{
	[TestClass]
	public class OrderDispatcherTest
	{
		readonly OrderDispatcher OrderDispatcher = new();
		EventHandler<OrderInfo>? OnStateChanged;
		readonly IDictionary<int, List<State>> StatesHistory = new ConcurrentDictionary<int, List<State>>();
		int LastOrderId { get; set; }

		[TestInitialize]
		public void OrderDispatcherInitialize()
		{
			LastOrderId = 0;
			OnStateChanged = new(StateChangeHandler);
			OrderDispatcher.StateChange += OnStateChanged;
		}

		private void StateChangeHandler(object? sender, OrderInfo orderInfo)
		{
			if (!StatesHistory.TryAdd(orderInfo.OrderId, new List<State>(new[] { orderInfo.State })))
			{
				StatesHistory[orderInfo.OrderId].Add(orderInfo.State);
			}
		}

		[TestMethod]
		public void ProcessOrderTest()
		{
			LastOrderId++;
			OrderDispatcher.ProcessOrder(LastOrderId).Wait();

			Assert.IsTrue(OrderDispatcher.GetOrderState(LastOrderId) == State.Delivered);

			var states = StatesHistory[LastOrderId];

			Assert.IsNotNull(states);
			var statesCount = Enum.GetValues<State>().Length;
			Assert.AreEqual(states.Count, statesCount);
			for (int i = 0; i < statesCount; i++)
			{
				Assert.AreEqual(states[i], (State)i);
			}
		}

		[TestCleanup]
		public void OrderDispatcherCleanup()
		{
			OrderDispatcher.StateChange -= OnStateChanged;
		}
	}
}