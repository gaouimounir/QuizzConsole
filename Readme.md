
# Quiz interactif en C#

Bienvenue dans le Quiz interactif en C#! Ce programme vous permet de tester vos connaissances à travers une série de questions réparties dans différentes catégories. Les questions sont chargées à partir d'un fichier CSV, et vous devrez choisir la catégorie qui vous intéresse avant de répondre aux questions.


## Comment exécuter le programme

1) Assurez-vous d'avoir un fichier CSV contenant les questions au format approprié (voir ci-dessous).

2) Compilez et exécutez le programme C#.


## Comment jouer

1) Le programme commence par vous accueillir et vous demande votre nom.
2) Ensuite, vous pouvez choisir une catégorie parmi celles disponibles.
3) Le quiz commencera avec des questions aléatoires de la catégorie choisie.
4) Pour chaque question, lisez attentivement et choisissez la réponse correcte en entrant le numéro correspondant.
5) Vous recevrez des points pour chaque réponse correcte, avec un bonus de score si vous répondez rapidement.
6) À la fin du quiz, votre score total sera affiché.
## Format du fichier CSV

Le fichier CSV doit être structuré comme suit :

Catégorie;Question;Numéro de la bonne option;Option 1;Option 2;Option 3;Option 4

- Chaque ligne représente une question avec sa catégorie, le texte de la question, Numéro de la bonne option, et les 4 autres options possibles.
- La bonne option doit être numérotée de 1 à 4 et correspondre à l'option correcte.

Exemple : 

Maths;Combien font 2+2?;3;2;5;4;22

Science;Quelle est la plus grande planète du système solaire?;1;Jupiter;Vénus;Mars;Saturne

## Remarques

- Si aucune question n'est trouvée pour la catégorie sélectionnée, veuillez vérifier le fichier CSV.
- Assurez-vous que le fichier CSV est correctement formaté pour éviter des erreurs lors du chargement des questions.


Amusez-vous bien en testant vos connaissances avec ce Quiz interactif en C#!