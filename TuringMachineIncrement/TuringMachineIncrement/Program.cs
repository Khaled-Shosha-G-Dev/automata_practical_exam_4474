namespace TuringMachineIncrement
{
    internal class Program
    {
        static void Main()
        {
            Console.Write("Enter binary number: ");
            string input = Console.ReadLine();
            string result = SimulateTMI(input);
            Console.WriteLine($"Result : {result}");
        }

        static string SimulateTMI(string binary)
        {
            char[] tape = binary.ToCharArray();
            int head = tape.Length - 1;

            while (head >= 0)
            {
                if (tape[head] == '0')
                {
                    tape[head] = '1';
                    return new string(tape);
                }
                else
                {
                    tape[head] = '0';
                    head--;
                }
            }

            return "1" + new string(tape);
        }
    }
}