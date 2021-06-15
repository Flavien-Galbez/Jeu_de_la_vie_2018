using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuDeLaVie_Flavien_Galbez
{
    class Program
    {
        // Flavien GALBEZ Groupe C

        static void Main(string[] args)
        {
            int nbDePopulation; //Sert uniquement dans le cas du jeu à N population
            int nbLigne;
            int nbColonne;
            int numeroDuJeu;
            double tauxDeRemplissage;
            int[,] matriceOriginale;
            do
            {
                Console.WriteLine("Combien de ligne possède la grille ? (Saisir un nombre entier supérieur ou égal à 3)");
                nbLigne = Convert.ToInt32(Console.ReadLine());
            }
            while (nbLigne < 3);
            Console.Clear();
            do
            {
                Console.WriteLine("Combien de colonne possède la grille ? (Saisir un nombre entier supérieur ou égal à 3)");
                nbColonne = Convert.ToInt32(Console.ReadLine());
            }
            while (nbColonne < 3);
            Console.Clear();
            do
            {
                Console.WriteLine("Quel est le taux de remplissage de la grille (compris entre 0.1 et 0.9) ?");
                tauxDeRemplissage = Convert.ToDouble(Console.ReadLine());
            }
            while (tauxDeRemplissage < 0.1 || tauxDeRemplissage > 0.9);
            Console.Clear();
            numeroDuJeu = ChoixDuJeu();
            Console.Clear();
            switch (numeroDuJeu)
            {
                case 1:
                    matriceOriginale = CreationMatriceDeDepartUnePopulation(nbLigne, nbColonne, tauxDeRemplissage);
                    JeuClassique(matriceOriginale, false);
                    break;
                case 2:
                    matriceOriginale = CreationMatriceDeDepartUnePopulation(nbLigne, nbColonne, tauxDeRemplissage);
                    JeuClassique(matriceOriginale, true);
                    break;
                case 3:
                    matriceOriginale = CreationMatriceDeDepartDeuxPopulations(nbLigne, nbColonne, tauxDeRemplissage);
                    JeuVariante(matriceOriginale, false);
                    break;
                case 4:
                    matriceOriginale = CreationMatriceDeDepartDeuxPopulations(nbLigne, nbColonne, tauxDeRemplissage);
                    JeuVariante(matriceOriginale, true);
                    break;
                case 5:
                    do
                    {
                        Console.WriteLine("Combien de populations différentes possède la grille ? (Saisir un nombre entier supérieur ou égal à 1)");
                        nbDePopulation = Convert.ToInt32(Console.ReadLine());
                    }
                    while (nbDePopulation < 1);
                    Console.Clear();
                    matriceOriginale = CreationMatriceDeDepartNPopulations(nbDePopulation, nbLigne, nbColonne, tauxDeRemplissage);
                    JeuVarianteNPopulation(matriceOriginale, nbDePopulation, false);
                    break;
                case 6:
                    do
                    {
                        Console.WriteLine("Combien de populations différentes possède la grille ? (Saisir un nombre entier supérieur ou égal à 1)");
                        nbDePopulation = Convert.ToInt32(Console.ReadLine());
                    }
                    while (nbDePopulation < 1);
                    Console.Clear();
                    matriceOriginale = CreationMatriceDeDepartNPopulations(nbDePopulation, nbLigne, nbColonne, tauxDeRemplissage);
                    JeuVarianteNPopulation(matriceOriginale, nbDePopulation, true);
                    break;
            }
        }

        /// <summary>
        /// Permet de demander à l'utilisateur à quel jeu il veut jouer (renvoie un nombre entier entre 1 et 6)
        /// </summary>
        /// <returns></returns>
        static int ChoixDuJeu()
        {
            int choix;
            string lecture;
            Console.WriteLine("A quel jeu voulez vous jouer ? (Saisir la lettre correspondante)");
            Console.WriteLine("     (a) - Jeu DLV classique sans visualisation intermédiaire des états futurs");
            Console.WriteLine("     (b) - Jeu DLV classique avec visualisation intermédiaire des états futurs");
            Console.WriteLine("     (c) - Jeu DLV variante (2 populations) sans visualisation intermédiaire des états futurs");
            Console.WriteLine("     (d) - Jeu DLV variante (2 populations) avec visualisation intermédiaire des états futurs");
            Console.WriteLine("     (e) - Jeu DLV variante à N populations sans visualisation intermédiaire des états futurs");
            Console.WriteLine("     (f) - Jeu DLV variante à N populations avec visualisation intermédiaire des états futurs");
            do
            {
                lecture = Console.ReadLine();
                switch (lecture)
                {
                    case "a":
                        choix = 1;
                        break;
                    case "b":
                        choix = 2;
                        break;
                    case "c":
                        choix = 3;
                        break;
                    case "d":
                        choix = 4;
                        break;
                    case "e":
                        choix = 5;
                        break;
                    case "f":
                        choix = 6;
                        break;
                    default:            //Si l'utilisateur se trompe ou ne saisit rien, le jeu lui demanderas de resaisir son choix
                        choix = -1; 
                        Console.WriteLine("Veuillez saisir \"a\", \"b\", \"c\", \"d\", \"e\" ou \"f\" puis validez en pressant \"Entrer\" pour choisir un jeu");
                        break;
                }
            }
            while (choix < 0);
            return choix;
        }

        /// <summary>
        /// Correspond au jeu classique
        /// </summary>
        /// <param name="matriceOriginale"></param>
        static void JeuClassique(int[,] matriceOriginale, bool avecVisualisation)
        {
            int[,] matriceTemporaire;
            int numeroDeGeneration = 0;
            string entre;
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine("Presser \"Entrer\" pour passer à la génération suivante");
            Console.WriteLine("Ecrire \"fin\" puis presser \"Entrer\" pour mettre fin au jeu");
            do
            {
                matriceTemporaire = CreerMatriceTemporaire(matriceOriginale);
                ModificationMatriceTemporaireJeuClassique(matriceOriginale, matriceTemporaire);
                Console.WriteLine("=========================================================");
                Console.WriteLine();
                Console.WriteLine("Grille de l'état :");
                AfficherMatrice(matriceOriginale);
                if (avecVisualisation)
                {
                    Console.WriteLine("Grille de visualisation de l'état futur :");
                    AfficherMatrice(matriceTemporaire);
                }
                Console.WriteLine("Le numéro de génération est " + numeroDeGeneration);
                Console.WriteLine("La taille de la population est " + CompterValeur(1, matriceOriginale));
                EntrerLesNouvellesValeurs(matriceOriginale, matriceTemporaire);
                numeroDeGeneration++;
                entre = Console.ReadLine();
            }
            while (entre != "fin");

        }

        /// <summary>
        /// Correspond au jeu avec variante
        /// </summary>
        /// <param name="matriceOriginale"></param>
        /// <param name="avecVisualisation"></param>
        static void JeuVariante(int[,] matriceOriginale, bool avecVisualisation)
        {
            int[,] matriceTemporaire;
            int numeroDeGeneration = 0;
            string entre;
            Console.WriteLine();
            Console.WriteLine("=================================================================");
            Console.WriteLine("  Presser \"Entrer\" pour passer à la génération suivante");
            Console.WriteLine("  Ecrire \"fin\" puis presser \"Entrer\" pour mettre fin au jeu");
            do
            {
                matriceTemporaire = CreerMatriceTemporaire(matriceOriginale);
                ModificationMatriceTemporaireJeuVariante(matriceOriginale, matriceTemporaire);
                Console.WriteLine("=================================================================");
                Console.WriteLine();
                Console.WriteLine("Grille de l'état :");
                AfficherMatrice(matriceOriginale);
                if (avecVisualisation)
                {
                    Console.WriteLine("Grille de visualisation de l'état futur :");
                    AfficherMatrice(matriceTemporaire);
                }
                Console.WriteLine("Le numéro de génération est " + numeroDeGeneration);
                Console.WriteLine("La taille de la population 1 est " + CompterValeur(1, matriceOriginale));
                Console.WriteLine("La taille de la population 2 est " + CompterValeur(2, matriceOriginale));
                EntrerLesNouvellesValeurs(matriceOriginale, matriceTemporaire);
                numeroDeGeneration++;
                entre = Console.ReadLine();
            }
            while (entre != "fin");
        }

        /// <summary>
        /// Correspond au jeu avec variante à N population
        /// </summary>
        /// <param name="matriceOriginale"></param>
        /// <param name="avecVisualisation"></param>
        static void JeuVarianteNPopulation(int[,] matriceOriginale, int nbDePopulation, bool avecVisualisation)
        {
            int[,] matriceTemporaire;
            int numeroDeGeneration = 0;
            string entre;
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine("Presser \"Entrer\" pour passer à la génération suivante");
            Console.WriteLine("Ecrire \"fin\" puis presser \"Entrer\" pour mettre fin au jeu");
            do
            {
                matriceTemporaire = CreerMatriceTemporaire(matriceOriginale);
                ModificationMatriceTemporaireJeuVarianteNPopulations(matriceOriginale, matriceTemporaire, nbDePopulation);
                Console.WriteLine("=========================================================");
                Console.WriteLine();
                Console.WriteLine("Grille de l'état :");
                AfficherMatriceNPopulations(matriceOriginale);
                if (avecVisualisation)
                {
                    Console.WriteLine("Grille de visualisation de l'état futur :");
                    AfficherMatriceNPopulations(matriceTemporaire);
                }
                Console.WriteLine("Le numéro de génération est " + numeroDeGeneration);
                for (int population = 1; population <= nbDePopulation; population++)
                {
                    Console.WriteLine("La taille de la population " + population + " est " + CompterValeur(population, matriceOriginale));
                }
                EntrerLesNouvellesValeursNPopulations(matriceOriginale, matriceTemporaire);
                numeroDeGeneration++;
                entre = Console.ReadLine();
            }
            while (entre != "fin");
        }

        /// <summary>
        /// Creer une grille composée uniquement de cellule morte
        /// </summary>
        /// <param name="nbLigne"></param>
        /// <param name="nbColonne"></param>
        /// <returns></returns>
        static int[,] CreationMatriceMorte(int nbLigne, int nbColonne)
        {
            int[,] matrice = new int[nbLigne, nbColonne];
            for (int ligne = 0; ligne <= matrice.GetLength(0) - 1; ligne++)
            {
                for (int colonne = 0; colonne <= matrice.GetLength(1) - 1; colonne++)
                {
                    matrice[ligne, colonne] = 0;
                }
            }
            return matrice;
        }

        /// <summary>
        /// Permet de creer la première grille de façon aléatoire pour une population (et possédant le nombre de cellules vivantes correspondant au taux de remplissage)
        /// </summary>
        /// <param name="nbLigne"></param>
        /// <param name="nbColonne"></param>
        /// <param name="tauxDeRemplissage"></param>
        /// <returns></returns>
        static int[,] CreationMatriceDeDepartUnePopulation(int nbLigne, int nbColonne, double tauxDeRemplissage)
        {
            Random generateur = new Random();
            int ligne;
            int colonne;
            int[,] matrice = CreationMatriceMorte(nbLigne, nbColonne);
            int nbCases = CompterValeur(0, matrice);
            double nbCasesARemplir = nbCases * tauxDeRemplissage;
            for (int i = 0; i < nbCasesARemplir; i++)
            {
                do
                {
                    ligne = generateur.Next(0, matrice.GetLength(0));
                    colonne = generateur.Next(0, matrice.GetLength(1));
                }
                while (matrice[ligne, colonne] != 0); //Permet de ne pas écrire sur une cellule déjà vivante
                matrice[ligne, colonne] = 1;
            }
            return matrice;
        }

        /// <summary>
        /// Permet de creer la première grille de façon aléatoire pour deux populations (et possédant le nombre de cellules vivantes correspondant au taux de remplissage)
        /// </summary>
        /// <param name="nbLigne"></param>
        /// <param name="nbColonne"></param>
        /// <param name="tauxDeRemplissage"></param>
        /// <returns></returns>
        static int[,] CreationMatriceDeDepartDeuxPopulations(int nbLigne, int nbColonne, double tauxDeRemplissage)
        {
            Random generateur = new Random();
            int ligne;
            int colonne;
            int[,] matrice = CreationMatriceMorte(nbLigne, nbColonne);
            int nbCases = CompterValeur(0, matrice);
            double nbCasesARemplir = nbCases * tauxDeRemplissage / 2;
            for (int i = 0; i < nbCasesARemplir; i++)
            {
                do
                {
                    ligne = generateur.Next(0, matrice.GetLength(0));
                    colonne = generateur.Next(0, matrice.GetLength(1));
                }
                while (matrice[ligne, colonne] != 0); //Permet de ne pas écrire sur une cellule déjà vivante
                matrice[ligne, colonne] = 1;
                do
                {
                    ligne = generateur.Next(0, matrice.GetLength(0));
                    colonne = generateur.Next(0, matrice.GetLength(1));
                }
                while (matrice[ligne, colonne] != 0); //Permet de ne pas écrire sur une cellule déjà vivante
                matrice[ligne, colonne] = 2;
            }
            return matrice;
        }

        /// <summary>
        /// Permet de creer la première grille de façon aléatoire pour N populations
        /// </summary>
        /// <param name="nbDePopulation"></param>
        /// <param name="nbLigne"></param>
        /// <param name="nbColonne"></param>
        /// <param name="tauxDeRemplissage"></param>
        /// <returns></returns>
        static int[,] CreationMatriceDeDepartNPopulations(int nbDePopulation, int nbLigne, int nbColonne, double tauxDeRemplissage)
        {
            Random generateur = new Random();
            int ligne;
            int colonne;
            int famille = 1;
            int[,] matrice = CreationMatriceMorte(nbLigne, nbColonne);
            int nbCases = CompterValeur(0, matrice);
            double nbCasesARemplir = nbCases * tauxDeRemplissage ;
            for (int i = 0; i <= nbCasesARemplir; i++)
            {
                do
                {
                    ligne = generateur.Next(0, matrice.GetLength(0));
                    colonne = generateur.Next(0, matrice.GetLength(1));
                }
                while (matrice[ligne, colonne] != 0); //Permet de ne pas écrire sur une cellule déjà vivante
                matrice[ligne, colonne] = famille;
                famille++;
                if (famille > nbDePopulation)   //Redémarre le compteur à 1 si toutes les familles ont été parcouru une fois 
                {
                    famille = 1;
                }
            }
            return matrice;
        }

        /// <summary>
        /// Modifie la matrice temporaire en suivant les règles du jeu classique
        /// </summary>
        /// <param name="matriceOriginale"></param>
        /// <param name="matriceTemporaire"></param>
        static void ModificationMatriceTemporaireJeuClassique(int[,] matriceOriginale, int[,] matriceTemporaire)
        {
            for (int ligne = 0; ligne < matriceOriginale.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matriceOriginale.GetLength(1); colonne++)
                {
                    if (matriceOriginale[ligne, colonne] == 1)
                    {
                        if (CompterLesEtatsVoisins(1, ligne, colonne, 1, matriceOriginale) < 2 || CompterLesEtatsVoisins(1, ligne, colonne, 1, matriceOriginale) > 3)
                        {
                            matriceTemporaire[ligne, colonne] = -1;
                        }
                    }
                    else
                    {
                        if (CompterLesEtatsVoisins(1, ligne, colonne, 1, matriceOriginale) == 3)
                        {
                            matriceTemporaire[ligne, colonne] = -2;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Modifie la matrice temporaire en suivant les règles de la variante
        /// </summary>
        /// <param name="matriceOriginale"></param>
        /// <param name="matriceTemporaire"></param>
        static void ModificationMatriceTemporaireJeuVariante(int[,] matriceOriginale, int[,] matriceTemporaire)
        {
            for (int ligne = 0; ligne < matriceOriginale.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matriceOriginale.GetLength(1); colonne++)
                {
                    if (matriceOriginale[ligne, colonne] == 1)
                    {
                        if (CompterLesEtatsVoisins(1, ligne, colonne, 1, matriceOriginale) < 2 || CompterLesEtatsVoisins(1, ligne, colonne, 1, matriceOriginale) > 3)
                        {
                            matriceTemporaire[ligne, colonne] = -1;
                        }
                    }
                    if (matriceOriginale[ligne, colonne] == 2)
                    {
                        if (CompterLesEtatsVoisins(1, ligne, colonne, 2, matriceOriginale) < 2 || CompterLesEtatsVoisins(1, ligne, colonne, 2, matriceOriginale) > 3)
                        {
                            matriceTemporaire[ligne, colonne] = -1;
                        }
                    }
                    if (matriceOriginale[ligne, colonne] == 0)
                    {
                        if (CompterLesEtatsVoisins(1, ligne, colonne, 1, matriceOriginale) == 3 && CompterLesEtatsVoisins(1, ligne, colonne, 2, matriceOriginale) == 3)
                        {
                            if (CompterLesEtatsVoisins(2, ligne, colonne, 1, matriceOriginale) > CompterLesEtatsVoisins(2, ligne, colonne, 2, matriceOriginale))
                            {
                                matriceTemporaire[ligne, colonne] = -2;
                            }
                            if (CompterLesEtatsVoisins(2, ligne, colonne, 1, matriceOriginale) < CompterLesEtatsVoisins(2, ligne, colonne, 2, matriceOriginale))
                            {
                                matriceTemporaire[ligne, colonne] = -3;
                            }
                            if (CompterLesEtatsVoisins(2, ligne, colonne, 1, matriceOriginale) == CompterLesEtatsVoisins(2, ligne, colonne, 2, matriceOriginale))
                            {
                                if (CompterValeur(1, matriceOriginale) > CompterValeur(2, matriceOriginale))
                                {
                                    matriceTemporaire[ligne, colonne] = -2;
                                }
                                if (CompterValeur(1, matriceOriginale) < CompterValeur(2, matriceOriginale))
                                {
                                    matriceTemporaire[ligne, colonne] = -3;
                                }
                            }
                        }
                        else
                        {
                            if (CompterLesEtatsVoisins(1, ligne, colonne, 1, matriceOriginale) == 3)
                            {
                                matriceTemporaire[ligne, colonne] = -2;
                            }
                            if (CompterLesEtatsVoisins(1, ligne, colonne, 2, matriceOriginale) == 3)
                            {
                                matriceTemporaire[ligne, colonne] = -3;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Modifie la matrice temporaire en suivant les règles de la variante à N population
        /// </summary>
        /// <param name="matriceOriginale"></param>
        /// <param name="matriceTemporaire"></param>
        /// <param name="nbDePopulation"></param>
        static void ModificationMatriceTemporaireJeuVarianteNPopulations(int[,] matriceOriginale, int[,] matriceTemporaire, int nbDePopulation)
        {
            int numeroDePopulation;
            int nbDePopulationDonnantLaVie;
            int populationUn = 0;
            int populationDeux = 0;
            for (int ligne = 0; ligne < matriceOriginale.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matriceOriginale.GetLength(1); colonne++)
                {
                    nbDePopulationDonnantLaVie = 0;
                    populationUn = 0;
                    populationDeux = 0;
                    if (matriceOriginale[ligne, colonne] != 0)          // Cas où la cellule est vivante
                    {
                        if (CompterLesEtatsVoisins(1, ligne, colonne, matriceOriginale[ligne, colonne], matriceOriginale) < 2 || CompterLesEtatsVoisins(1, ligne, colonne, matriceOriginale[ligne, colonne], matriceOriginale) > 3)
                        {
                            matriceTemporaire[ligne, colonne] = -1;         //"-1" correspond à une cellule vivante qui va mourir
                        }                        
                    }
                    else   //Cas où la cellule est morte
                    {
                        for (numeroDePopulation = 1; numeroDePopulation <= nbDePopulation; numeroDePopulation++)
                        {
                            if (CompterLesEtatsVoisins(1, ligne, colonne, numeroDePopulation, matriceOriginale) == 3)
                            {
                                nbDePopulationDonnantLaVie++;
                                populationDeux = populationUn;     //Permet de mémoriser la ou les deux population(s) succeptible de donner la vie à la cellule morte
                                populationUn = numeroDePopulation;
                            }
                        }
                        if (nbDePopulationDonnantLaVie == 1) // Cas où il n'y a qu'une population pouvant donner la vie
                        {
                            matriceTemporaire[ligne, colonne] = -populationUn - 1; //"-X-1" correspond à une cellule morte qui va naître sous la population X
                        }
                        if (nbDePopulationDonnantLaVie == 2) // Cas où il y a deux populations pouvant donner la vie
                        {
                            if (CompterLesEtatsVoisins(2, ligne, colonne, populationUn, matriceOriginale) > CompterLesEtatsVoisins(2, ligne, colonne, populationDeux, matriceOriginale))
                            {
                                matriceTemporaire[ligne, colonne] = -populationUn - 1;
                            }
                            if (CompterLesEtatsVoisins(2, ligne, colonne, populationUn, matriceOriginale) < CompterLesEtatsVoisins(2, ligne, colonne, populationDeux, matriceOriginale))
                            {
                                matriceTemporaire[ligne, colonne] = -populationDeux - 1;
                            }
                            if (CompterLesEtatsVoisins(2, ligne, colonne, populationUn, matriceOriginale) == CompterLesEtatsVoisins(2, ligne, colonne, populationDeux, matriceOriginale))
                            {
                                if (CompterValeur(populationUn, matriceOriginale) > CompterValeur(populationDeux, matriceOriginale))
                                {
                                    matriceTemporaire[ligne, colonne] = -populationUn - 1;
                                }
                                if (CompterValeur(1, matriceOriginale) < CompterValeur(2, matriceOriginale))
                                {
                                    matriceTemporaire[ligne, colonne] = -populationDeux - 1;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Permet de creer une nouvelle matrice temporaire afin d'enregistrer les modifications
        /// </summary>
        /// <param name="matriceOriginale"></param>
        /// <returns></returns>
        static int[,] CreerMatriceTemporaire(int[,] matriceOriginale)
        {
            int[,] matriceTemporaire = new int[matriceOriginale.GetLength(0), matriceOriginale.GetLength(1)];
            for (int ligne = 0; ligne < matriceOriginale.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matriceOriginale.GetLength(1); colonne++)
                {
                    matriceTemporaire[ligne, colonne] = matriceOriginale[ligne, colonne];
                }
            }
            return matriceTemporaire;
        }

        /// <summary>
        /// Permet de renvoyer les données de la matrice temporaire vers la matrice principale pour 1 et 2 population.
        /// </summary>
        /// <param name="matriceOriginale"></param>
        /// <param name="matriceTemporaire"></param>
        static void EntrerLesNouvellesValeurs(int[,] matriceOriginale, int[,] matriceTemporaire)
        {
            for (int ligne = 0; ligne < matriceOriginale.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matriceOriginale.GetLength(1); colonne++)
                {
                    if (matriceTemporaire[ligne, colonne] == 1 || matriceTemporaire[ligne, colonne] == -2)
                    {
                        matriceOriginale[ligne, colonne] = 1;
                    }
                    else
                    {
                        if (matriceTemporaire[ligne, colonne] == 2 || matriceTemporaire[ligne, colonne] == -3)
                        {
                            matriceOriginale[ligne, colonne] = 2;
                        }
                        else
                        {
                            matriceOriginale[ligne, colonne] = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Permet de renvoyer les données de la matrice temporaire vers la matrice principale pour N population
        /// </summary>
        /// <param name="matriceOriginale"></param>
        /// <param name="matriceTemporaire"></param>
        static void EntrerLesNouvellesValeursNPopulations(int[,] matriceOriginale, int[,] matriceTemporaire)
        {
            for (int ligne = 0; ligne < matriceOriginale.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matriceOriginale.GetLength(1); colonne++)
                {
                    if (matriceTemporaire[ligne, colonne] == 0 || matriceTemporaire[ligne, colonne] == -1)
                    {
                        matriceOriginale[ligne, colonne] = 0;
                    }
                    else
                    {
                        if (matriceTemporaire[ligne, colonne] > 0)
                        {
                            matriceOriginale[ligne, colonne] = matriceTemporaire[ligne, colonne];
                        }
                        else
                        {
                            matriceOriginale[ligne, colonne] = -matriceTemporaire[ligne, colonne] - 1;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Permet de lier les cotés de la matrice afin d'obtenir une grille circulaire et donc d'obtenir la valeur des cases "non atteignable"
        /// </summary>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <param name="matriceOriginale"></param>
        /// <returns></returns>
        static int ObtenirLaValeurDeLaCase(int ligne, int colonne, int[,] matriceOriginale)
        {
            do
            {
                if (ligne < 0)
                {
                    ligne = ligne + matriceOriginale.GetLength(0);
                }
                if (ligne >= matriceOriginale.GetLength(0))
                {
                    ligne = ligne - matriceOriginale.GetLength(0);
                }
            }
            while (ligne < 0 || ligne >= matriceOriginale.GetLength(0));
            do
            {
                if (colonne < 0)
                {
                    colonne = colonne + matriceOriginale.GetLength(1);
                }
                if (colonne >= matriceOriginale.GetLength(1))
                {
                    colonne = colonne - matriceOriginale.GetLength(1);
                }
            }
            while (colonne < 0 || colonne >= matriceOriginale.GetLength(1));
            return (matriceOriginale[ligne, colonne]);
        }

        /// <summary>
        /// Permet de compter le nombre de case voisine au rang défini égale à la valeurACompter à la case de coordonnée [ligne,colonne]
        /// </summary>
        /// <param name="rang"></param>
        /// <param name="ligne"></param>
        /// <param name="colonne"></param>
        /// <param name="valeurACompter"></param>
        /// <param name="matriceOriginale"></param>
        /// <returns></returns>
        static int CompterLesEtatsVoisins(int rang, int ligne, int colonne, int valeurACompter, int[,] matriceOriginale)
        {
            int compteur = 0;
            for (int voisinLigne = ligne - rang; voisinLigne <= ligne + rang; voisinLigne++)
            {
                for (int voisinColonne = colonne - rang; voisinColonne <= colonne + rang; voisinColonne++)
                {
                    if (voisinLigne != ligne || voisinColonne != colonne)   // On ne compte pas la case évaluée
                    {
                        if (ObtenirLaValeurDeLaCase(voisinLigne, voisinColonne, matriceOriginale) == valeurACompter) //Grâce à ObtenirLaValleurDeLaCase, il n'y a pas de dépassement d'indice dans la lecture du tableau
                        {
                            compteur++;
                        }
                    }
                }
            }
            return compteur;
        }

        /// <summary>
        /// Permet de compter le nombre d'ittération de la valeurACompter sur l'ensemble de la matrice
        /// </summary>
        /// <param name="valeurACompter"></param>
        /// <param name="matriceOriginale"></param>
        /// <returns></returns>
        static int CompterValeur(int valeurACompter, int[,] matriceOriginale)
        {
            int compteur = 0;
            for (int ligne = 0; ligne <= matriceOriginale.GetLength(0) - 1; ligne++)
            {
                for (int colonne = 0; colonne <= matriceOriginale.GetLength(1) - 1; colonne++)
                {
                    if (matriceOriginale[ligne, colonne] == valeurACompter)
                    {
                        compteur++;
                    }
                }
            }
            return compteur;
        }

        /// <summary>
        /// Permet d'afficher la grille pour 1 et 2 population de la même façon que dans l'énoncé
        /// </summary>
        /// <param name="matrice"></param>
        static void AfficherMatrice(int[,] matrice)
        {
            for (int ligne = 0; ligne <= matrice.GetLength(0) - 1; ligne++)
            {
                for (int colonne = 0; colonne <= matrice.GetLength(1) - 1; colonne++)
                { 
                    if (matrice[ligne, colonne] == 0)
                    {
                        Console.Write(".");             // Correspond à une cellule morte
                    }
                    if (matrice[ligne, colonne] == 1)
                    {
                        Console.Write("#");             // Correspond à une cellule vivante de la population 1
                    }
                    if (matrice[ligne, colonne] == 2)
                    {
                        Console.Write("x");             // Correspond à une cellule vivante de la population 2
                    }
                    if (matrice[ligne, colonne] == -3)
                    {
                        Console.Write("o");             // Correspond à une cellule morte qui va naître sous la population 2
                    }
                    if (matrice[ligne, colonne] == -2)
                    {
                        Console.Write("-");             // Correspond à une cellule morte qui va naître sous la population 1
                    }
                    if (matrice[ligne, colonne] == -1)
                    {
                        Console.Write("*");             // Correspond à une cellule vivante qui va mourir
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Permet d'afficher la grille : les famille selon leur numéro et les cellules mortes par un point.
        /// Les cellules devenant vivantes sont représentées par le numéro de leur futur famille précédé par un signe "-"
        /// </summary>
        /// <param name="matrice"></param>
        static void AfficherMatriceNPopulations(int[,] matrice)
        {
            for (int ligne = 0; ligne <= matrice.GetLength(0) - 1; ligne++)
            {
                for (int colonne = 0; colonne <= matrice.GetLength(1) - 1; colonne++)
                {
                    if (matrice[ligne, colonne] == 0)
                    {
                        Console.Write(" . ");           // Correspond à une cellule morte
                    }
                    if (matrice[ligne, colonne] == -1)
                    {
                        Console.Write(" * ");           // Correspond à une cellule vivante qui va mourir
                    }
                    if (matrice[ligne, colonne] > 0)
                    {
                        Console.Write(" "  + matrice[ligne, colonne]+ " ");     // Correspond à une cellule vivante de la population représentée par le nombre (de 1 à N)
                    }
                    if (matrice[ligne, colonne] < -1)
                    {
                        Console.Write(matrice[ligne, colonne] + 1 + " ");       // Un nombre précédé par un "-" correspond à une cellule morte qui va naitre sous la population représentée par ce nombre 
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

    }
}
