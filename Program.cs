﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Quizz
{
    public class Question
    {
        // Le texte de la question
        public string Text { get; set; }

        // Les options de réponse
        public List<string> Options { get; set; }

        // L'index de l'option correcte dans la liste des options
        public int CorrectOptionIndex { get; set; }
    }

    class Program
    {
        static void Main()
        {
            AccueilJoueur();

            // Charger les questions à partir d'un fichier CSV
            List<Question> questions = ChargerQuestionsDepuisCSV("questions.csv");

            // Vérifier si des questions ont été chargées
            if (questions.Count == 0)
            {
                Console.WriteLine("Aucune question n'a été chargée. Veuillez vérifier le fichier CSV.");
                return;
            }

            // Parcourir les questions
            int score = ParcourirQuestions(questions);

            // Afficher le score final
            AfficherScore(score);

            // Afficher un message de fin
            MessageBye();
        }

        private static List<Question> ChargerQuestionsDepuisCSV(string cheminFichier)
        {
            List<Question> questions = new List<Question>();

            try
            {
                using (StreamReader sr = new StreamReader(cheminFichier))
                {
                    while (!sr.EndOfStream)
                    {
                        var ligne = sr.ReadLine();
                        var valeurs = ligne.Split(';');


                        if (valeurs.Length >= 3)
                        {
                            string question = valeurs[0];
                            string reponseCorrecte = valeurs[1];
                            string choix = valeurs[2];

                            List<string> options = new List<string>();
                            for (int i = 2; i < valeurs.Length; i++)
                            {
                                options.Add(valeurs[i]);
                            }

                            Question nouvelleQuestion = new Question
                            {
                                Text = question,
                                Options = options,
                                CorrectOptionIndex = int.Parse(reponseCorrecte) - 1
                            };

                            questions.Add(nouvelleQuestion);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Le fichier de questions n'a pas été trouvé.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite lors du chargement des questions : {ex.Message}");
            }

            return questions;
        }

        static void AccueilJoueur()
        {
            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();

            Console.WriteLine($"{Environment.NewLine}Hello, {name}!");
            Console.WriteLine("Welcome to the quiz. Good luck!");
            Console.WriteLine("Press any key to continue.");
            Console.ReadLine();
        }

        private static int ParcourirQuestions(List<Question> questions)
        {
            int score = 0;

            foreach (var question in questions)
            {
                if (PoserQuestion(question.Text, question.Options, question.CorrectOptionIndex))
                {
                    score++;
                }
            }

            return score;
        }

        private static void AfficherScore(int score)
        {
            Console.WriteLine($"{Environment.NewLine}Your score: {score}");
        }

        private static bool PoserQuestion(string question, List<string> options, int correctOptionIndex)
        {
            Console.WriteLine(question);
            DonnerReponse(options);

            var reponseJoueur = Console.ReadLine();

            return VerifierReponse(reponseJoueur, correctOptionIndex);
        }

        private static bool VerifierReponse(string reponseJoueur, int correctOptionIndex)
        {
            if (int.TryParse(reponseJoueur, out int choixUtilisateur) && choixUtilisateur - 1 == correctOptionIndex)
            {
                Console.WriteLine("Correct answer!");
                Console.WriteLine("Press any key for the next question.");
                Console.ReadLine();
                return true;
            }
            else
            {
                Console.WriteLine("Wrong answer!");
                Console.WriteLine("Press any key for the next question.");
                Console.ReadLine();
                return false;
            }
        }

        private static void DonnerReponse(List<string> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {options[i]}");
            }
        }

        private static void MessageBye()
        {
            Console.WriteLine("Thank you for playing the quiz. Goodbye!");
        }
    }
}
