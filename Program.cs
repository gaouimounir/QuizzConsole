﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quizz
{
    public class Question
    {
        public string Category { get; set; } = "";
        public string Text { get; set; } = "";
        public List<string> Options { get; set; } = new List<string>();
        public int CorrectOptionIndex { get; set; }
    }

    class Program
    {
        private static int score;
        private static DateTime tempsDebutQuestion;

        static void Main()
        {
            AccueilJoueur();

            HashSet<string> categories = ChargerCategoriesDepuisCSV("questions.csv");
            string selectedCategory = ChoisirCategorie(categories);

            List<Question> questions = ChargerQuestionsDepuisCSV("questions.csv", selectedCategory);

            if (questions.Count == 0)
            {
                Console.WriteLine($"Aucune question n'a été trouvée pour la catégorie '{selectedCategory}'. Veuillez vérifier le fichier CSV.");
                return;
            }

            score = 0;
            ParcourirQuestions(questions);

            AfficherScore();

            MessageBye();
        }

        private static void AccueilJoueur()
        {
            Console.WriteLine("Bienvenue au Quiz interactif!");
            Console.WriteLine("Comment vous appelez-vous?");
            var name = Console.ReadLine();

            Console.WriteLine($"{Environment.NewLine}Bonjour, {name}!");
            Console.WriteLine("Prêt à tester vos connaissances? Appuyez sur n'importe quelle touche pour commencer.");
            Console.ReadLine();
        }

        private static HashSet<string> ChargerCategoriesDepuisCSV(string cheminFichier)
        {
            HashSet<string> categories = new HashSet<string>();

            try
            {
                using (StreamReader sr = new StreamReader(cheminFichier))
                {
                    while (!sr.EndOfStream)
                    {
                        var valeurs = sr.ReadLine()?.Split(';');

                        if (valeurs != null && valeurs.Length >= 1)
                        {
                            string category = valeurs[0];
                            categories.Add(category);
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
                Console.WriteLine($"Une erreur s'est produite lors du chargement des catégories : {ex.Message}");
            }

            return categories;
        }

        private static string ChoisirCategorie(HashSet<string> categories)
        {
            Console.WriteLine("Choisissez une catégorie :");
            int index = 1;

            foreach (var category in categories)
            {
                Console.WriteLine($"{index}. {category}");
                index++;
            }

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choixCategorie) && choixCategorie >= 1 && choixCategorie <= categories.Count)
                {
                    return categories.ElementAt(choixCategorie - 1);
                }
                else
                {
                    Console.WriteLine("Choix invalide. Veuillez entrer un numéro de catégorie valide.");
                }
            }
        }

        private static List<Question> ChargerQuestionsDepuisCSV(string cheminFichier, string selectedCategory)
        {
            List<Question> questions = new List<Question>();

            try
            {
                using (StreamReader sr = new StreamReader(cheminFichier))
                {
                    while (!sr.EndOfStream)
                    {
                        var valeurs = sr.ReadLine()?.Split(';');

                        if (valeurs != null && valeurs.Length >= 5)
                        {
                            string category = valeurs[0];
                            string questionText = valeurs[1];
                            string reponseCorrecte = valeurs[2];

                            if (category == selectedCategory)
                            {
                                List<string> options = new List<string>(valeurs[3..]);

                                Question nouvelleQuestion = new Question
                                {
                                    Category = category,
                                    Text = questionText,
                                    Options = options,
                                    CorrectOptionIndex = int.Parse(reponseCorrecte) - 1
                                };

                                questions.Add(nouvelleQuestion);
                            }
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

        private static void ParcourirQuestions(List<Question> questions)
        {
            List<Question> questionsMelangees = questions.OrderBy(q => Guid.NewGuid()).ToList();

            foreach (var question in questionsMelangees)
            {
                tempsDebutQuestion = DateTime.Now;
                if (PoserQuestion(question))
                {
                    score += 1; // Ajoute 1 point pour une bonne réponse
                }
                AfficherScore();
            }
        }

        private static void AfficherScore()
        {
            Console.WriteLine($"{Environment.NewLine}Votre score actuel : {score}");
        }

        private static bool PoserQuestion(Question question)
        {
            Console.WriteLine(question.Text);
            DonnerReponse(question.Options);

            var reponseJoueur = Console.ReadLine();

            if (string.IsNullOrEmpty(reponseJoueur))
            {
                return false;
            }

            return VerifierReponse(reponseJoueur, question.CorrectOptionIndex);
        }

        private static bool VerifierReponse(string reponseJoueur, int correctOptionIndex)
        {
            TimeSpan tempsReponse = DateTime.Now - tempsDebutQuestion;

            if (int.TryParse(reponseJoueur, out int choixUtilisateur) && choixUtilisateur - 1 == correctOptionIndex)
            {
                Console.WriteLine("Bonne réponse ! +1");
                int bonusScore = CalculerBonusScore(tempsReponse);
                if (bonusScore > 0)
                {
                    score += bonusScore;
                    Console.WriteLine($"Bonus Score : +{bonusScore}");
                }
                return true;
            }
            else
            {
                Console.WriteLine("Mauvaise réponse !");
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
            Console.WriteLine("Merci d'avoir joué au quiz. Au revoir !");
        }

        private static int CalculerBonusScore(TimeSpan tempsReponse)
        {
            const double seuilRapide = 5.0; // en secondes
            const int bonusRapide = 3;

            return tempsReponse.TotalSeconds < seuilRapide ? bonusRapide : 0;
        }
    }
}
