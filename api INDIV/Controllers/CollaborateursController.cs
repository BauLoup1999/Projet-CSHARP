using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;


namespace APINegoSud.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CollaborateursController : ControllerBase
    {
        private readonly ILogger<CollaborateursController> _logger;
        private readonly IConfiguration _configuration;

        public CollaborateursController(ILogger<CollaborateursController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet(Name = "GetCollaborateurs")]
        public IEnumerable<Collaborateur> Get()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                List<Collaborateur> listCollaborateurs = new List<Collaborateur>();
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM dbo.Salaries;";
                    MySqlCommand getCollaborateursCmd = new MySqlCommand(query, connection);
                    DbDataReader reader = getCollaborateursCmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        string nom = reader["Nom"].ToString();
                        string prenom = reader["Prenom"].ToString();
                        string telephoneFixe = reader["Téléphone_fixe"].ToString();
                        string telephonePortable = reader["Téléphone_portable"].ToString();
                        string email = reader["Email"].ToString();
                        int serviceId = Convert.ToInt32(reader["Service_id"]);
                        int siteId = Convert.ToInt32(reader["Site_id"]);

                        Collaborateur collaborateur = new Collaborateur
                        {
                            Id = id,
                            Nom = nom,
                            Prenom = prenom,
                            TelephoneFixe = telephoneFixe,
                            TelephonePortable = telephonePortable,
                            Email = email,
                            ServiceId = serviceId,
                            SiteId = siteId
                        };

                        listCollaborateurs.Add(collaborateur);
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erreur de connexion à la base de données : " + ex.Message);
                }

                _logger.LogInformation("Retourne " + listCollaborateurs.Count + " Collaborateurs");
                return listCollaborateurs;
            }
        }

        [HttpGet("{id}", Name = "GetCollaborateur")]
        public ActionResult<Collaborateur> Get(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                Collaborateur collaborateur = null;
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM dbo.Salaries WHERE Id = @id;";
                    MySqlCommand getCollaborateurCmd = new MySqlCommand(query, connection);
                    getCollaborateurCmd.Parameters.AddWithValue("@id", id);
                    DbDataReader reader = getCollaborateurCmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int collaborateurId = Convert.ToInt32(reader["Id"]);
                        string nom = reader["Nom"].ToString();
                        string prenom = reader["Prenom"].ToString();
                        string telephoneFixe = reader["Téléphone_fixe"].ToString();
                        string telephonePortable = reader["Téléphone_portable"].ToString();
                        string email = reader["Email"].ToString();
                        int serviceId = Convert.ToInt32(reader["Service_id"]);
                        int siteId = Convert.ToInt32(reader["Site_id"]);

                        collaborateur = new Collaborateur
                        {
                            Id = collaborateurId,
                            Nom = nom,
                            Prenom = prenom,
                            TelephoneFixe = telephoneFixe,
                            TelephonePortable = telephonePortable,
                            Email = email,
                            ServiceId = serviceId,
                            SiteId = siteId
                        };
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erreur de connexion à la base de données : " + ex.Message);
                }

                if (collaborateur != null)
                {
                    return collaborateur;
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        public ActionResult<Collaborateur> Post([FromBody] Collaborateur collaborateur)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO dbo.Salaries (Nom, Prenom, Téléphone_fixe, Téléphone_portable, Email, Service_id, Site_id) " +
                                   "VALUES (@nom, @prenom, @telephoneFixe, @telephonePortable, @email, @serviceId, @siteId);";

                    MySqlCommand insertCmd = new MySqlCommand(query, connection);
                    insertCmd.Parameters.AddWithValue("@nom", collaborateur.Nom);
                    insertCmd.Parameters.AddWithValue("@prenom", collaborateur.Prenom);
                    insertCmd.Parameters.AddWithValue("@telephoneFixe", collaborateur.TelephoneFixe);
                    insertCmd.Parameters.AddWithValue("@telephonePortable", collaborateur.TelephonePortable);
                    insertCmd.Parameters.AddWithValue("@email", collaborateur.Email);
                    insertCmd.Parameters.AddWithValue("@serviceId", collaborateur.ServiceId);
                    insertCmd.Parameters.AddWithValue("@siteId", collaborateur.SiteId);

                    int rowsAffected = insertCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        collaborateur.Id = (int)insertCmd.LastInsertedId;
                        return CreatedAtRoute("GetCollaborateur", new { id = collaborateur.Id }, collaborateur);
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erreur de connexion à la base de données : " + ex.Message);
                    return BadRequest();
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Collaborateur collaborateur)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE dbo.Salaries " +
                                   "SET Nom = @nom, Prenom = @prenom, Téléphone_fixe = @telephoneFixe, Téléphone_portable = @telephonePortable, " +
                                   "Email = @email, Service_id = @serviceId, Site_id = @siteId " +
                                   "WHERE Id = @id;";

                    MySqlCommand updateCmd = new MySqlCommand(query, connection);
                    updateCmd.Parameters.AddWithValue("@id", id);
                    updateCmd.Parameters.AddWithValue("@nom", collaborateur.Nom);
                    updateCmd.Parameters.AddWithValue("@prenom", collaborateur.Prenom);
                    updateCmd.Parameters.AddWithValue("@telephoneFixe", collaborateur.TelephoneFixe);
                    updateCmd.Parameters.AddWithValue("@telephonePortable", collaborateur.TelephonePortable);
                    updateCmd.Parameters.AddWithValue("@email", collaborateur.Email);
                    updateCmd.Parameters.AddWithValue("@serviceId", collaborateur.ServiceId);
                    updateCmd.Parameters.AddWithValue("@siteId", collaborateur.SiteId);

                    int rowsAffected = updateCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erreur de connexion à la base de données : " + ex.Message);
                    return BadRequest();
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM dbo.Salaries WHERE Id = @id;";
                    MySqlCommand deleteCmd = new MySqlCommand(query, connection);
                    deleteCmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = deleteCmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return NoContent();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erreur de connexion à la base de données : " + ex.Message);
                    return BadRequest();
                }
            }
        }
    }
}
