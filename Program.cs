using System;

namespace Quizz
{
    class Program
    {
        static void Main()
        {
            AccueilJoueur();

            int score = ParcourirQuestions();

            AfficherScore(score);

            MessageBye();
        }

        private static void MessageBye()
        {
            Console.WriteLine("Thank you for playing the quiz. Goodbye!");
        }

        static void AccueilJoueur()
        {
            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();

            Console.WriteLine($"{Environment.NewLine}Hello, {name}!");
            Console.WriteLine("Welcome to the quiz. Good luck!");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        private static int ParcourirQuestions()
        {
            int score = 0;

            // Question 1
            if (PoserQuestion("Combien font 2+2?", "4"))
            {
                score++;
            }

            // Question 2
            if (PoserQuestion("Combien font 22+2?", "24"))
            {
                score++;
            }

            // Question 3
            if (PoserQuestion("Combien font 4+2?", "6"))
            {
                score++;
            }

            // Question 4
            if (PoserQuestion("Combien font 6+2?", "8"))
            {
                score++;
            }

            // Question 5
            if (PoserQuestion("Combien font 8+2?", "10"))
            {
                score++;
            }

            return score;
        }

        private static void AfficherScore(int score)
        {
            Console.WriteLine($"{Environment.NewLine}Your score: {score}");
        }

        private static bool PoserQuestion(string question, string reponseCorrecte)
        {
            Console.WriteLine(question);
            DonnerReponse();

            var reponseJoueur = Console.ReadLine();

            return VerifierReponse(reponseJoueur, reponseCorrecte);
        }

        private static bool VerifierReponse(string reponseJoueur, string reponseCorrecte)
        {
            if (reponseJoueur == reponseCorrecte)
            {
                Console.WriteLine("Correct answer!");
                Console.WriteLine("Press any key for the next question.");
                Console.ReadKey();
                return true;
            }
            else
            {
                Console.WriteLine("Wrong answer!");
                Console.WriteLine("Press any key for the next question.");
                Console.ReadKey();
                return false;

            }

        }

        private static void DonnerReponse()
        {
            Console.WriteLine("1) 2");
            Console.WriteLine("2) 22");
            Console.WriteLine("3) 4");
            Console.WriteLine("4) 6");
        }
    }
}
