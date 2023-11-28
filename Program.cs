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
            Console.WriteLine("Merci de votre participation!");
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

            var reponseJoueur = Console.ReadLine();


            if (reponseJoueur == "2")
            {
                Console.WriteLine("Mauvaise reponse");
            }
            else if (reponseJoueur == "22")
            {
                Console.WriteLine("Mauvaise reponse");
            }

            else if (reponseJoueur == "4")
            {
                Console.WriteLine("Bonne reponse");
            }
            else if (reponseJoueur == "6")
            {
                Console.WriteLine("Mauvaise reponse");
            }



        }

        private static void PoserQuestion()
        {
            var question1 = "Combien font 2+2?";
            Console.WriteLine(question1);
        }

        private static void DonnerReponse()
        {
            var reponse1 = "2";
            var reponse2 = "22";
            var reponse3 = "4";
            var reponse4 = "6";
            Console.WriteLine(reponse1);
            Console.WriteLine(reponse2);
            Console.WriteLine(reponse3);
            Console.WriteLine(reponse4);

        }
    }
}