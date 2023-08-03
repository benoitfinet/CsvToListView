using CsvToListView.Droid;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;

// Défini une interface pour android
[assembly: Dependency(typeof(AndroidCsvFileLoader))]
namespace CsvToListView.Droid
{
    public class AndroidCsvFileLoader : ICsvLoader
    {
        // Chargement des données via le fichier CSV
        public List<Properties> LoadData()
        {
            var itemList = new List<Properties>();

            // Récupération du fichier CSV depuis le dossier assets
            var filename = "basePdv.csv";
            var assetManager = Android.App.Application.Context.Assets;

            try
            {
                // Permet de lire le fichier CSV (via StreamReader)
                using (var streamReader = new StreamReader(assetManager.Open(filename)))
                {
                    // Permet d'ignorer la première ligne (header)
                    streamReader.ReadLine();

                    // Boucle pour lire chaque ligne du fichier CSV
                    while (!streamReader.EndOfStream)
                    {
                        var line = streamReader.ReadLine();
                        var values = line.Split(';');

                        // Je vérifie ici le format des données
                        if (values.Length == 4)
                        {
                            var column = new Properties
                            {
                                // Je créer des colonnes en fonction des données du fichier CSV
                                CODE_PDV = values[0],
                                RAISON_SOCIALE = values[1],
                                COMMERCIAL = values[2],
                                ID_UNIQUE = values[3]
                            };
                            // J'ajoute l'objet à la liste
                            itemList.Add(column); 
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                // Gestion des erreurs
                System.Diagnostics.Debug.WriteLine($"Le fichier n'a pas été lu correctement : {ex.Message}");
            }

            // Retourne la liste des objets dans les colonnes
            return itemList;
        }
    }
}
