namespace TaskCoordination
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Run the Continuation class to execute the tasks in sequence
            //Continuation.Run();

            //1.1 Run the Continuation2 class to execute multiple tasks and then a continuation task when all tasks are completed
            //Continuation2.Run();

            //1.2 Run the Continuation3 class to execute multiple tasks and then a continuation task when any of the tasks is completed
            //Continuation3.Run();

            //2. Child Task - detached from parent
            //ChildTask.Run();

            //2.1 Child Task attached to parent
            //ChildTask2.Run();

            //2.3 Child Task with Continuation tasks (based of success or failure of the child task)
            ChildTask3.Run();

            Console.WriteLine("\nMain Function finished successfully\n");
        }
    }
}
