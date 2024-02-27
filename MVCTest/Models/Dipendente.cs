using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;

namespace MVCTest.Models
{
    // classe per la gestione dei dipendenti nel database
    public class Dipendente
    {
        [Key]
        [Display(Name = "Numero Dipendente")]
        public int DipendenteID { get; set; }

        [Required(ErrorMessage = "Il campo Nome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il campo Cognome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il cognome non può superare i 50 caratteri.")]
        public string Cognome { get; set; }

        [StringLength(100, ErrorMessage = "L'indirizzo non può superare i 100 caratteri.")]
        public string Indirizzo { get; set; }

        [Required(ErrorMessage = "Il campo Codice Fiscale è obbligatorio.")]
        [StringLength(16, ErrorMessage = "Il codice fiscale deve essere di 16 caratteri.", MinimumLength = 16)]
        public string CodiceFiscale { get; set; }

        [Display(Name = "Sei coniugato?")]
        public bool Coniugato { get; set; }

        [Display(Name = "Figli a carico")]
        [Range(0, int.MaxValue, ErrorMessage = "Il numero di figli deve essere un numero positivo.")]
        [DefaultValue(0)]
        public int NumeroFigli { get; set; }

        [Required(ErrorMessage = "Il campo Mansione è obbligatorio.")]
        [StringLength(100, ErrorMessage = "La mansione non può superare i 100 caratteri.")]
        public string Mansione { get; set; }

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["GestionaleEdile"].ConnectionString;

        // metodo per ottenere tutti i dipendenti
        public List<Dipendente> GetTuttiIDipendenti()
        {
            List<Dipendente> listaDipendenti = new List<Dipendente>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SELECT * FROM Dipendenti", con);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Dipendente dipendente = new Dipendente()
                        {
                            DipendenteID = Convert.ToInt32(reader["DipendenteID"]),
                            Nome = reader["Nome"].ToString(),
                            Cognome = reader["Cognome"].ToString(),
                            Indirizzo = reader["Indirizzo"].ToString(),
                            CodiceFiscale = reader["CodiceFiscale"].ToString(),
                            Coniugato = Convert.ToBoolean(reader["Coniugato"]),
                            NumeroFigli = Convert.ToInt32(reader["NumeroFigli"]),
                            Mansione = reader["Mansione"].ToString()
                        };
                        listaDipendenti.Add(dipendente);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Errore durante la lettura dei dipendenti", ex);
            }
            return listaDipendenti;
        }

        // metodo per ottenere un dipendente per ID
        public Dipendente GetDipendenteById(int id)
        {
            Dipendente dipendente = null;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Dipendenti WHERE DipendenteID = @DipendenteID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@DipendenteID", id);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        dipendente = new Dipendente
                        {
                            DipendenteID = Convert.ToInt32(reader["DipendenteID"]),
                            Nome = reader["Nome"].ToString(),
                            Cognome = reader["Cognome"].ToString(),
                            Indirizzo = reader["Indirizzo"].ToString(),
                            CodiceFiscale = reader["CodiceFiscale"].ToString(),
                            Coniugato = Convert.ToBoolean(reader["Coniugato"]),
                            NumeroFigli = Convert.ToInt32(reader["NumeroFigli"]),
                            Mansione = reader["Mansione"].ToString()
                        };
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ApplicationException($"Errore durante la lettura del dipendente con ID {id}", ex);
            }
            return dipendente;
        }

        // metodo per aggiungere un dipendente
        public void AddDipendente(Dipendente dipendente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "INSERT INTO Dipendenti (Nome, Cognome, Indirizzo, CodiceFiscale, Coniugato, NumeroFigli, Mansione) VALUES (@Nome, @Cognome, @Indirizzo, @CodiceFiscale, @Coniugato, @NumeroFigli, @Mansione)";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.Parameters.AddWithValue("@Nome", dipendente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", dipendente.Cognome);
                cmd.Parameters.AddWithValue("@Indirizzo", dipendente.Indirizzo);
                cmd.Parameters.AddWithValue("@CodiceFiscale", dipendente.CodiceFiscale);
                cmd.Parameters.AddWithValue("@Coniugato", dipendente.Coniugato);
                cmd.Parameters.AddWithValue("@NumeroFigli", dipendente.NumeroFigli);
                cmd.Parameters.AddWithValue("@Mansione", dipendente.Mansione);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // metodo per aggiornare un dipendente
        public void UpdateDipendente(Dipendente dipendente)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = @"
                UPDATE Dipendenti 
                SET Nome = @Nome, 
                Cognome = @Cognome, 
                Indirizzo = @Indirizzo, 
                CodiceFiscale = @CodiceFiscale, 
                Coniugato = @Coniugato, 
                NumeroFigli = @NumeroFigli, 
                Mansione = @Mansione 
                WHERE DipendenteID = @DipendenteID";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.Parameters.AddWithValue("@Nome", dipendente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", dipendente.Cognome);
                cmd.Parameters.AddWithValue("@Indirizzo", dipendente.Indirizzo);
                cmd.Parameters.AddWithValue("@CodiceFiscale", dipendente.CodiceFiscale);
                cmd.Parameters.AddWithValue("@Coniugato", dipendente.Coniugato);
                cmd.Parameters.AddWithValue("@NumeroFigli", dipendente.NumeroFigli);
                cmd.Parameters.AddWithValue("@Mansione", dipendente.Mansione);
                cmd.Parameters.AddWithValue("@DipendenteID", dipendente.DipendenteID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // metodo per eliminare un dipendente
        public void DeleteDipendente(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "DELETE FROM Dipendenti WHERE DipendenteID = @DipendenteID";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.Parameters.AddWithValue("@DipendenteID", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}