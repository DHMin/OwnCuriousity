using System;
using System.Threading;
using System.Threading.Tasks;

public class AsyncAwaitTestClass
{

    public async Task AsyncMethod()
    {
        Print("AsyncMethod : before await call");
        await _AwaitMethod();
        // if just return Task that is not running,
        // below code will never be called.
        Print("AsyncMethod : after await call");
    }

    private Task _AwaitMethod()
    {
        Print("AwaitMethod");

        Action action = delegate
        {
            Print("AsyncJob : start");
            Thread.Sleep(1000);
            Print("AsyncJob : end");
        };

        //return new Task(action);
                 
        return Task.Factory.StartNew(action);
    }

	public static void Main(string[] args)
	{
        AsyncAwaitTestClass t = new AsyncAwaitTestClass();
        Print("Main: before async method call");
        Task task = t.AsyncMethod();
        Console.WriteLine("async task is compleated : " + task.IsCompleted);
        Print("Main: after async method call");

        Thread.Sleep(3000);
	}

    public static void Print(string msg)
    {
        Console.WriteLine("=========================");
        Console.WriteLine(msg);
        Console.WriteLine("Thread : " + Thread.CurrentThread.ManagedThreadId);
        Console.WriteLine("Stack : " + Environment.StackTrace);   
    }
}