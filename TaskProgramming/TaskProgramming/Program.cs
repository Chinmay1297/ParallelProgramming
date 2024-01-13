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
        //Creating Tasks and running it
        //MyTask1.Run();

        //Different ways of creating a task and passing it function/action and value for action
        //MyTask2.Run();

        #region "how to return a value from a task"
        var txt1 = "mytext1";
        var txt2 = "hello chinmay";

        var task1 = new Task<int>(TextLength, txt1);
        task1.Start();
        Task<int> task2 = Task.Factory.StartNew<int>(TextLength, txt2);

        //you cant get result of a task until its finished, so you gotta wait for it to finish
        Console.WriteLine($"\nLength of text 1: {task1.Result} \nLength of text2: {task2.Result}");
        #endregion

        //How to cancle tasks
        //CancellingTasks.Run();

        //How to execute  wait handle after cancellation
        CancellingTasks2.Run();

        Console.WriteLine("\nMain Program finished\n");

        Console.ReadKey();
    }
}