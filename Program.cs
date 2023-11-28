﻿using System;

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
            Console.WriteLine($"{Environment.NewLine}Welcome, {name}!");
        }

        private static int ParcourirQuestions()
        {
            int score = 0;

            // Question 1
            if (PoserQuestion("Combien font 2+2?", "4"))
            {
                score++;
            }

            // Ajoutez d'autres questions ici selon le même modèle

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
                return true;
            }
            else
            {
                Console.WriteLine("Wrong answer!");
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
