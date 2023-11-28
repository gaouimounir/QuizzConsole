namespace Quizz
{
    class Program
    {
        static void Main()
        {

            AccueilJoueur();

            ParcourirQuestions();

            MessageBye();


        }

        private static void MessageBye()
        {
            throw new NotImplementedException();
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

        private static void AfficherScore()
        {
            throw new NotImplementedException();
        }

        private static void VerifierReponse()
        {
            throw new NotImplementedException();
        }

        private static void PoserQuestion()
        {
            var question1 = "What is your favorite color?";
            Console.WriteLine(question1);
            var reponse1 = "Blue";
            var reponse2 = "Red";
            var reponse3 = "Green";
            var reponse4 = "Yellow";
            Console.WriteLine(reponse1);
            Console.WriteLine(reponse2);
            Console.WriteLine(reponse3);
            Console.WriteLine(reponse4);

        }

        private static void DonnerReponse()
        {


            var reponseJoueur = Console.ReadLine();

            if (reponseJoueur == "Blue")
            {

            }
            else if (reponseJoueur == "Red")
            {

            }

            else if (reponseJoueur == "Green")
            {
            }
            else if (reponseJoueur == "Yellow")
            {
            }



        }
    }
}