using Danfoss;

var testString =
    "2023-11-01T08:03:15Z [CRITICAL] - Unhandled exception during payment processing for order 789123.\n  Traceback (most recent call last):\n    File \"/app/services/payment_processor.py\", line 45, in process\n      external_api.charge(amount, card_token)\n    File \"/app/lib/external_api.py\", line 112, in charge\n      raise APITimeoutError(\"API did not respond in 30s\")";

var logEntry = new LogEntry();
Console.WriteLine(LogParser.GetTimestamp(testString));
Console.WriteLine(LogParser.GetErrorCode(testString));
Console.WriteLine(LogParser.GetMessage(testString, true));
