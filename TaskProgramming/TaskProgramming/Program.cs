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
        //MyTask1.Run();
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

        Console.WriteLine("\nMain Program finished\n");
    }
}