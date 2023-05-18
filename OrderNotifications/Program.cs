// See https://aka.ms/new-console-template for more information
using OrderNotifications;

// initialize the order dispatcher
var dispatcher = new OrderDispatcher();

// initialize the status monitor
_ = new OrderStatusMonitor(dispatcher);

// keep a list of order processing tasks
var runningOrders = new List<Task>();
int lastOrderId = 0;

// add some unpredictibility
var random = new Random();

// feed the beast
for (int i = 0; i < 10; i++)
{
	var orderId = ++lastOrderId;

	Console.WriteLine($"Submitting order: {orderId}");
	// submit order
	var orderTask = dispatcher.ProcessOrder(orderId);
	// keep the tasks
	runningOrders.Add(orderTask);

	Task.Delay(random.Next(500, 5000));
}

// wait for all the orders to be processed
Task.WaitAll(runningOrders.ToArray());

Console.WriteLine($"{System.Environment.NewLine}That's all folks!..");
Console.ReadLine();
