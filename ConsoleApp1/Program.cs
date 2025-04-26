
RunHeavyProcess();


Console.WriteLine("Program started" + Thread.CurrentThread.ManagedThreadId);
await RunVeryHeavyProcess();
Console.WriteLine("Program ended" + Thread.CurrentThread.ManagedThreadId);


Console.ReadKey();
static async Task RunHeavyProcess()
{
    Console.WriteLine("FIRST PROCEDURE STARTED at thread" + Thread.CurrentThread.ManagedThreadId);
    await Task.Delay(3000);
    Console.WriteLine("FIRST PROCEDURE ENDED" + Thread.CurrentThread.ManagedThreadId);

}

static async Task RunVeryHeavyProcess()
{
    Console.WriteLine("SECOND PROCEDURE STARTED" + Thread.CurrentThread.ManagedThreadId);
    var result = await Task.Run(() =>
    {
        Thread.Sleep(4000);
        Console.WriteLine("SECOND PROCEDURE COMPLETED" + Thread.CurrentThread.ManagedThreadId);
        return "Heavy process completed!";
    });
}