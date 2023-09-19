using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace App_lourde_INDIV
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Modèles.Collaborateur> collaborateurs = new ObservableCollection<Modèles.Collaborateur>();

        public MainWindow()
        {
            InitializeComponent();

            collaborateurs.Add(new Modèles.Collaborateur
            {
                Nom = "Dupont",
                Prénom = "Jean",
                TéléphoneFixe = "0123456789",
                TéléphonePortable = "0612345678",
                Email = "jean.dupont@email.com",
                Service = "Comptabilité",
                Site = "Paris"
            });

            collaborateurs.Add(new Modèles.Collaborateur
            {
                Nom = "Martin",
                Prénom = "Alice",
                TéléphoneFixe = "0234567890",
                TéléphonePortable = "0678901234",
                Email = "alice.martin@email.com",
                Service = "Production",
                Site = "Nantes"
            });

            collaborateurListView.ItemsSource = collaborateurs;
        }

        private void txtRecherche_KeyUp(object sender, KeyEventArgs e)
        {
            string recherche = txtRecherche.Text.ToLower();

            var collaborateursFiltrés = collaborateurs.Where(c => c.Nom.ToLower().Contains(recherche)).ToList();

            collaborateurListView.ItemsSource = collaborateursFiltrés;
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Crée une fenêtre de dialogue pour ajouter un collaborateur
            AjouterCollaborateurWindow fenetreAjout = new AjouterCollaborateurWindow();

            // Affiche la fenêtre de dialogue de manière modale
            if (fenetreAjout.ShowDialog() == true)
            {
                // Récupère le collaborateur ajouté depuis la fenêtre de dialogue
                Modèles.Collaborateur nouveauCollaborateur = fenetreAjout.NouveauCollaborateur;

                // Ajoute le nouveau collaborateur à la liste
                collaborateurs.Add(nouveauCollaborateur);
            }
        }
    }
}
