using System;
using System.Windows;

namespace App_lourde_INDIV
{
    public partial class AjouterCollaborateurWindow : Window
    {
        public Modèles.Collaborateur NouveauCollaborateur { get; private set; }

        public AjouterCollaborateurWindow()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            // Récupère les informations saisies par l'utilisateur
            string nom = txtNom.Text;
            string prénom = txtPrenom.Text;
            string téléphoneFixe = txtTelephoneFixe.Text;
            string téléphonePortable = txtTelephonePortable.Text;
            string email = txtEmail.Text;
            string service = txtService.Text;
            string site = txtSite.Text;

            // Crée un nouvel objet Collaborateur avec les informations saisies
            NouveauCollaborateur = new Modèles.Collaborateur
            {
                Nom = nom,
                Prénom = prénom,
                TéléphoneFixe = téléphoneFixe,
                TéléphonePortable = téléphonePortable,
                Email = email,
                Service = service,
                Site = site
            };

            // Ferme la fenêtre de dialogue avec un résultat positif (true)
            DialogResult = true;
        }

        private void btnAnnuler_Click(object sender, RoutedEventArgs e)
        {
            // Ferme la fenêtre de dialogue avec un résultat négatif (false)
            DialogResult = false;
        }
    }
}
