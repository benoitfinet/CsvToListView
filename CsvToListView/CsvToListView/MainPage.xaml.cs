using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace CsvToListView
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            LoadData();
        }

        // Classe générique (https://learn.microsoft.com/fr-fr/dotnet/csharp/programming-guide/generics/generic-classes)
        // En gros, ici je parcours ma liste pour créer des groupes d'objets, leur donner une clé unique (K Key) 
        public class Grouping<K, T> : ObservableCollection<T>
        {
            public K Key { get; private set; }

            // IEnumerable<T> : Expose l’énumérateur, qui prend en charge une itération simple sur une collection d’un type spécifié (Doc. Microsoft)
            public Grouping(K key, IEnumerable<T> items)
            {
                Key = key;

                // Me permet d'ajouter pour chaque "groupe" une clé et les objets de ce groupe
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
        }

        // Chargement des datas via le fichier CSV
        private void LoadData()
        {
            // Récupération des données (via AndroidCsvFileLoader/ICsvLoader)
            var itemList = DependencyService.Get<ICsvLoader>().LoadData();

            // Vérification du chargement des données
            if (itemList != null && itemList.Count > 0)
            {
                // Affiche les données chargées dans le debug
                foreach (var item in itemList)
                {
                    System.Diagnostics.Debug.WriteLine($"CODE PDV : {item.CODE_PDV}, RAISON SOCIALE : {item.RAISON_SOCIALE}, COMMERCIAL : {item.COMMERCIAL}, ID : {item.ID_UNIQUE}");
                }

            }
            else
            {
                // Message en cas d'echec
                System.Diagnostics.Debug.WriteLine("Echec du chargement du fichier CSV");
            }

            // Source de la liste
            listView.ItemsSource = itemList;
        }
    }
}
