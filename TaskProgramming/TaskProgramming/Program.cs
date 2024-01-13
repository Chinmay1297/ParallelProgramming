using System.ComponentModel;
using TaskProgramming;

internal class Program
{
    public static int TextLength(object text)
    {
        Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {text}...");
        return text.ToString().Length;
    }

    private static void Main(string[] args)
    {
        //1.Creating Tasks and running it
        //MyTask1.Run();

        //2.Different ways of creating a task and passing it function/action and value for action
        //MyTask2.Run();

        //3.How to return a value from a task
        #region "how to return a value from a task"
        var txt1 = "mytext1";
        var txt2 = "hello chinmay";

        var task1 = new Task<int>(TextLength, txt1);
        task1.Start();
        Task<int> task2 = Task.Factory.StartNew<int>(TextLength, txt2);

        //you cant get result of a task until its finished, so you gotta wait for it to finish
        Console.WriteLine($"\nLength of text 1: {task1.Result} \nLength of text2: {task2.Result}");
        #endregion

        //4.How to cancle tasks
        //CancellingTasks.Run();

        //5.How to execute  wait handle after cancellation
        //CancellingTasks2.Run();

        //6.How to link multiple cancellation tokens
        //CompositeCancellationToken.Run();

        //7. Wait for time to pass
        WaitForTimeToPass2.Run();

        Console.WriteLine("\nMain Program finished\n");
        Console.ReadKey();
    }
}