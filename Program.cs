namespace Quizz
{
    class Program
    {
        static void Main(string[] args)
        {

            AccueilJoueur();

            ParcourirQuestions();


        }

        static void AccueilJoueur()
        {
            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();
            Console.WriteLine($"{Environment.NewLine}Welcome, {name}!");
        }

        private static void ParcourirQuestions()
        {

            PoserQuestion();
            DonnerReponse();
            VerifierReponse();
            AfficherScore();

        }


        private static void PoserQuestion()
        {

        }

        private static void DonnerReponse()
        {
            throw new NotImplementedException();
        }

        private static void VerifierReponse()
        {
            throw new NotImplementedException();
        }

        private static void AfficherScore()
        {
            throw new NotImplementedException();
        }


    }
}