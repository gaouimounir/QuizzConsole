using System;
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

            // Charger les catégories uniques à partir d'un fichier CSV
            HashSet<string> categories = ChargerCategoriesDepuisCSV("questions.csv");

            // Choix de la catégorie
            string selectedCategory = ChoisirCategorie(categories);

            // Charger les questions de la catégorie choisie à partir d'un fichier CSV
            List<Question> questions = ChargerQuestionsDepuisCSV("questions.csv", selectedCategory);

            if (questions.Count == 0)
            {
                Console.WriteLine($"Aucune question n'a été trouvée pour la catégorie '{selectedCategory}'. Veuillez vérifier le fichier CSV.");
                return;
            }

            score = 0; // Réinitialiser le score pour chaque nouvelle partie
            ParcourirQuestions(questions);

            AfficherScore();

            MessageBye();
        }

        private static void AccueilJoueur()
        {
            Console.WriteLine("What is your name?");
            var name = Console.ReadLine();

            Console.WriteLine($"{Environment.NewLine}Hello, {name}!");
            Console.WriteLine("Welcome to the quiz. Good luck!");
            Console.WriteLine("Press any key to continue.");
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
            Console.WriteLine("Choose a category:");
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
                    Console.WriteLine("Invalid choice. Please enter a valid category number.");
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
            // Mélanger les questions
            List<Question> questionsMelangees = questions.OrderBy(q => Guid.NewGuid()).ToList();

            foreach (var question in questionsMelangees)
            {
                tempsDebutQuestion = DateTime.Now; // Enregistrez le temps de début
                if (PoserQuestion(question))
                {
                    // Si la réponse est correcte, ajoutez le bonus et le score de base
                    score += CalculerBonusScore(DateTime.Now - tempsDebutQuestion);
                }
            }
        }

        private static void AfficherScore()
        {
            Console.WriteLine($"{Environment.NewLine}Your final score: {score}");
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
            int bonusScore = CalculerBonusScore(tempsReponse);

            if (int.TryParse(reponseJoueur, out int choixUtilisateur) && choixUtilisateur - 1 == correctOptionIndex)
            {
                Console.WriteLine($"Correct answer! Bonus Score: +{bonusScore}");
                // Ajouter le bonus et le score de base
                score += bonusScore + 1;
            }
            else
            {
                Console.WriteLine("Wrong answer!");
            }

            if (Console.IsInputRedirected)
            {
                Console.ReadLine();
            }
            else
            {
                Console.ReadKey();
            }

            return choixUtilisateur - 1 == correctOptionIndex;
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

        private static int CalculerBonusScore(TimeSpan tempsReponse)
        {
            // Ajustez ces valeurs selon votre préférence
            const double seuilRapide = 5.0; // en secondes
            const int bonusRapide = 3;

            Console.WriteLine($"Response Time: {tempsReponse.TotalSeconds} seconds");
            Console.WriteLine($"Bonus Score: {(tempsReponse.TotalSeconds < seuilRapide ? bonusRapide : 0)}");

            return tempsReponse.TotalSeconds < seuilRapide ? bonusRapide : 0;
        }
    }
}
