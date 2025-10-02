using BinaryState;

internal class Program
{
    private static void Main(string[] args)
    {
        var states = new States(100);

        states.SetState(50, 1);
        states.SetState(10, 1);
        states.SetState(90, 1);

        Console.WriteLine(states.GetIntState(50));
        Console.WriteLine(states.GetIntState(10));
        Console.WriteLine(states.GetIntState(90));
        Console.WriteLine(states.GetIntState(110));
    }
}