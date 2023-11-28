namespace Quizz
{
    class Program
    {
        static void Main(string[] args)
        {

            AccueilJoueur();

            ParcourirQuestions();


        }

        private static void ParcourirQuestions()
        {

        }

        static void AccueilJoueur()
        {
            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();
            Console.WriteLine($"{Environment.NewLine}Welcome, {name}!");
        }
    }
}