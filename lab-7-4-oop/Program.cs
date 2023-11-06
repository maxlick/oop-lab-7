using lab_7_4_oop;

class Program
{
    static void Main(string[] args)
    {
        //1
        Calculator<int> intCalc = new Calculator<int>();
        intCalc.Add = (a, b) => a + b;
        intCalc.Subtract = (a, b) => a - b;
        intCalc.Multiply = (a, b) => a * b;
        intCalc.Divide = (a, b) => b != 0 ? a / b : throw new ArgumentException("Cannot divide by zero.");

        int intResultAdd = intCalc.PerformOperation(intCalc.Add, 3, 5);
        int intResultSubtract = intCalc.PerformOperation(intCalc.Subtract, 5, 3);
        int intResultMultiply = intCalc.PerformOperation(intCalc.Multiply, 3, 5);
        int intResultDivide = intCalc.PerformOperation(intCalc.Divide, 10, 2);

        Console.WriteLine("Integer calculations:");
        Console.WriteLine($"Addition: {intResultAdd}");
        Console.WriteLine($"Subtraction: {intResultSubtract}");
        Console.WriteLine($"Multiplication: {intResultMultiply}");
        Console.WriteLine($"Division: {intResultDivide}");

        Calculator<double> doubleCalc = new Calculator<double>();
        doubleCalc.Add = (a, b) => a + b;
        doubleCalc.Subtract = (a, b) => a - b;
        doubleCalc.Multiply = (a, b) => a * b;
        doubleCalc.Divide = (a, b) => b != 0 ? a / b : throw new ArgumentException("Cannot divide by zero.");

        double doubleResultAdd = doubleCalc.PerformOperation(doubleCalc.Add, 3.5, 4.8);
        double doubleResultSubtract = doubleCalc.PerformOperation(doubleCalc.Subtract, 5.9, 3.8);
        double doubleResultMultiply = doubleCalc.PerformOperation(doubleCalc.Multiply, 3.4, 4.8);
        double doubleResultDivide = doubleCalc.PerformOperation(doubleCalc.Divide, 12.8, 4);

        Console.WriteLine("\nDouble calculations:");
        Console.WriteLine($"Addition: {doubleResultAdd}");
        Console.WriteLine($"Subtraction: {doubleResultSubtract}");
        Console.WriteLine($"Multiplication: {doubleResultMultiply}");
        Console.WriteLine($"Division: {doubleResultDivide}");

        //2
        Repository<int> repository = new Repository<int>();
        repository.Add(1);
        repository.Add(2);
        repository.Add(3);
        repository.Add(4);
        repository.Add(5);

        List<int> evenNumbers = repository.Find(item => item % 2 == 0);
        Console.WriteLine("\nEven numbers:");
        foreach (var evenNumber in evenNumbers)
        {
            Console.WriteLine(evenNumber);
        }

        //3
        FunctionCache<int, string> cache = new FunctionCache<int, string>(TimeSpan.FromMinutes(1));

        Func<int, string> myFunction = (key) => "Value for key " + key;

        string result1 = cache.ExecuteFunction(myFunction, 5);
        Console.WriteLine(result1);

        System.Threading.Thread.Sleep(61000);

        string result2 = cache.ExecuteFunction(myFunction, 5);
        Console.WriteLine(result2);

        //4
        TaskScheduler<string, int> scheduler = new TaskScheduler<string, int>(
            task => task.Length,
            task => Console.WriteLine("Executing task: " + task));

        Console.WriteLine("Enter a task (or 'exit' to quit):");
        string input = Console.ReadLine();
        while (input != "exit")
        {
            scheduler.AddTask(input);
            Console.WriteLine("Enter a task (or 'exit' to quit):");
            input = Console.ReadLine();
        }

        scheduler.ExecuteNext();
    }
}