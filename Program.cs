using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Quizz
{
    // Définition d'une classe Question pour représenter une question du quiz
    public class Question
    {
        public string Category { get; set; } = ""; // Catégorie de la question
        public string Text { get; set; } = "";     // Texte de la question
        public List<string> Options { get; set; } = new List<string>(); // Liste des options de réponse
        public int CorrectOptionIndex { get; set; } // Index de l'option correcte dans la liste des options
    }

    class Program
    {
        // Déclaration de variables globales
        private static int score;               // Variable pour stocker le score du joueur
        private static DateTime tempsDebutQuestion;  // Variable pour enregistrer le moment où la question a été posée
        private static TimeSpan tempsReponseQuestion; // Variable pour stocker le temps de réponse à une question

        // Méthode principale
        static void Main()
        {
            // Appel de la méthode d'accueil
            AccueilJoueur();

            // Charger les catégories uniques à partir d'un fichier CSV
            HashSet<string> categories = ChargerCategoriesDepuisCSV("questions.csv");

            // Choix de la catégorie
            string selectedCategory = ChoisirCategorie(categories);

            // Charger les questions de la catégorie choisie à partir d'un fichier CSV
            List<Question> questions = ChargerQuestionsDepuisCSV("questions.csv", selectedCategory);

            // Vérifier s'il y a des questions dans la catégorie sélectionnée
            if (questions.Count == 0)
            {
                Console.WriteLine($"Aucune question n'a été trouvée pour la catégorie '{selectedCategory}'. Veuillez vérifier le fichier CSV.");
                return;
            }

            // Initialisation du score
            score = 0;

            // Parcourir et poser des questions
            ParcourirQuestions(questions);

            // Afficher le score final
            AfficherScore();

            // Message de fin
            MessageBye();
        }

        // Méthode d'accueil du joueur
        private static void AccueilJoueur()
        {
            Console.WriteLine("Bienvenue au Quiz interactif!");
            Console.WriteLine("Comment vous appelez-vous?");
            var name = Console.ReadLine();

            Console.WriteLine($"{Environment.NewLine}Bonjour, {name}!");
            Console.WriteLine("Prêt à tester vos connaissances? Appuyez sur n'importe quelle touche pour commencer.");
            Console.ReadLine();
        }

        // Méthode pour charger les catégories depuis un fichier CSV
        private static HashSet<string> ChargerCategoriesDepuisCSV(string cheminFichier)
        {
            HashSet<string> categories = new HashSet<string>();

            try
            {
                // Utilisation du bloc using pour garantir la fermeture du StreamReader
                using StreamReader sr = new StreamReader(cheminFichier);

                // Parcourir le fichier CSV
                while (!sr.EndOfStream)
                {
                    string[]? valeurs = sr.ReadLine()?.Split(';');

                    if (valeurs != null && valeurs.Length >= 1)
                    {
                        string category = valeurs[0];
                        categories.Add(category);
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

        // Méthode pour choisir une catégorie
        private static string ChoisirCategorie(HashSet<string> categories)
        {
            Console.WriteLine("Choisissez une catégorie :");
            int index = 1;

            // Afficher les catégories disponibles avec des indices
            foreach (var category in categories)
            {
                Console.WriteLine($"{index}. {category}");
                index++;
            }

            // Attendre une entrée utilisateur valide
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int choixCategorie) && choixCategorie >= 1 && choixCategorie <= categories.Count)
                {
                    // Retourner la catégorie sélectionnée
                    return categories.ElementAt(choixCategorie - 1);
                }
                else
                {
                    Console.WriteLine("Choix invalide. Veuillez entrer un numéro de catégorie valide.");
                }
            }
        }

        // Méthode pour charger les questions depuis un fichier CSV
        private static List<Question> ChargerQuestionsDepuisCSV(string cheminFichier, string selectedCategory)
        {
            List<Question> questions = new List<Question>();

            try
            {
                // Utilisation du bloc using pour garantir la fermeture du StreamReader
                using StreamReader sr = new StreamReader(cheminFichier);

                // Parcourir le fichier CSV
                while (!sr.EndOfStream)
                {
                    string[]? valeurs = sr.ReadLine()?.Split(';');

                    if (valeurs != null && valeurs.Length >= 5)
                    {
                        string category = valeurs[0];
                        string questionText = valeurs[1];
                        string reponseCorrecte = valeurs[2];

                        // Vérifier si la question appartient à la catégorie sélectionnée
                        if (category == selectedCategory)
                        {
                            // Récupérer les options de réponse à partir des valeurs restantes
                            List<string> options = new List<string>(valeurs[3..]);

                            // Créer un objet Question et l'ajouter à la liste
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

        // Méthode principale pour parcourir les questions et gérer les réponses
        private static void ParcourirQuestions(List<Question> questions)
        {
            // Mélanger les questions pour un ordre aléatoire
            List<Question> questionsMelangees = questions.OrderBy(q => Guid.NewGuid()).ToList();

            // Pour chaque question, poser la question et vérifier la réponse
            foreach (var question in questionsMelangees)
            {
                tempsDebutQuestion = DateTime.Now; // Enregistrement du début du temps de réponse
                if (PoserQuestion(question))
                {
                    score += 1; // Ajoute 1 point pour une bonne réponse
                }
                AfficherScore(); // Afficher le score après chaque question
            }
        }

        // Méthode pour afficher le score actuel
        private static void AfficherScore()
        {
            Console.WriteLine($"{Environment.NewLine}Votre score est de : {score} points. Temps de réponse : {tempsReponseQuestion.TotalSeconds} secondes.");
        }

        // Méthode pour poser une question
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

        // Méthode pour vérifier la réponse
        private static bool VerifierReponse(string reponseJoueur, int correctOptionIndex)
        {
            tempsReponseQuestion = DateTime.Now - tempsDebutQuestion; // Calcul du temps de réponse

            // Vérification de la réponse en comparant l'index de l'option choisie avec l'index de la réponse correcte
            if (int.TryParse(reponseJoueur, out int choixUtilisateur) && choixUtilisateur - 1 == correctOptionIndex)
            {
                Console.WriteLine("Bonne réponse ! +1");
                int bonusScore = CalculerBonusScore(tempsReponseQuestion);

                // Si un bonus est obtenu, l'ajouter au score et l'afficher
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

        // Méthode pour afficher les options de réponse
        private static void DonnerReponse(List<string> options)
        {
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {options[i]}");
            }
        }

        // Méthode pour afficher le message de fin
        private static void MessageBye()
        {
            Console.WriteLine("Merci d'avoir joué au quiz. Au revoir !");
        }

        // Méthode pour calculer le bonus de score en fonction du temps de réponse
        private static int CalculerBonusScore(TimeSpan tempsReponse)
        {
            const double seuilRapide = 5.0; // Seuil de temps en secondes pour le bonus
            const int bonusRapide = 3;      // Montant du bonus pour une réponse rapide

            // Si le temps de réponse est inférieur au seuil, retourner le montant du bonus, sinon retourner 0
            return tempsReponse.TotalSeconds < seuilRapide ? bonusRapide : 0;
        }
    }
}
