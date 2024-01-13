internal class Program
{

    public static void Write(char c)
    {
        int i = 1000;
        while(i -- > 0)
        {
            Console.Write(c);
        }
    }

    private static void Main(string[] args)
    {
        Task.Factory.StartNew(() => Write('.'));  //creating a task and starting it simultaneously

        var t = new Task(()=> Write('?'));        //creating a task variable which is yet to start

        t.Start();

        Write('-');

        Console.Write("Main Program done");

        Console.Write("Hello, World!");
    }
}