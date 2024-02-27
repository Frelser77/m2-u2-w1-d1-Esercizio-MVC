using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;

namespace MVCTest.Models
{
    public class Pagamento
    {
        [Key]
        [Display(Name = "Numero Pagamento")]
        public int PagamentoID { get; set; }

        [Display(Name = "Inizio Periodo Lavorativo")]
        [Required(ErrorMessage = "La data di inizio del periodo di pagamento è obbligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La data non è in un formato valido.")]
        public DateTime PeriodoPagamentoInizio { get; set; }

        [Display(Name = "Fine Periodo Lavorativo")]
        [Required(ErrorMessage = "La data di fine del periodo di pagamento è obbligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La data non è in un formato valido.")]
        public DateTime PeriodoPagamentoFine { get; set; }

        [Required(ErrorMessage = "L'ammontare del pagamento è obbligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "L'ammontare deve essere un numero positivo.")]
        [DataType(DataType.Currency, ErrorMessage = "L'ammontare non è in un formato valido.")]
        public decimal Ammontare { get; set; }

        [Required(ErrorMessage = "Il tipo di pagamento è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il tipo di pagamento non può superare i 50 caratteri.")]
        [Display(Name = "Acconto | Intero")]
        public string Tipo { get; set; }

        [Display(Name = "Numero Dipendente")]
        [Required(ErrorMessage = "Il numero del dipendente è obbligatorio.")]
        public int DipendenteID { get; set; }

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["GestionaleEdile"].ConnectionString;

        // metodo per ottenere tutti i pagamenti
        public List<Pagamento> GetAllPagamenti()
        {
            List<Pagamento> listaPagamenti = new List<Pagamento>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Pagamenti", con);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pagamento = new Pagamento()
                        {
                            PagamentoID = Convert.ToInt32(reader["PagamentoID"]),
                            DipendenteID = Convert.ToInt32(reader["DipendenteID"]),
                            PeriodoPagamentoInizio = Convert.ToDateTime(reader["PeriodoPagamentoInizio"]),
                            PeriodoPagamentoFine = Convert.ToDateTime(reader["PeriodoPagamentoFine"]),
                            Ammontare = Convert.ToDecimal(reader["Ammontare"]),
                            Tipo = reader["Tipo"].ToString()
                        };
                        listaPagamenti.Add(pagamento);
                    }
                }
            }

            return listaPagamenti;
        }

        // metodo per ottenere un pagamento specifico
        public Pagamento GetPagamentoById(int id)
        {
            Pagamento pagamento = null;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Pagamenti WHERE PagamentoID = @PagamentoID", con);
                cmd.Parameters.AddWithValue("@PagamentoID", id);

                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pagamento = new Pagamento()
                        {
                            PagamentoID = Convert.ToInt32(reader["PagamentoID"]),
                            DipendenteID = Convert.ToInt32(reader["DipendenteID"]),
                            PeriodoPagamentoInizio = Convert.ToDateTime(reader["PeriodoPagamentoInizio"]),
                            PeriodoPagamentoFine = Convert.ToDateTime(reader["PeriodoPagamentoFine"]),
                            Ammontare = Convert.ToDecimal(reader["Ammontare"]),
                            Tipo = reader["Tipo"].ToString()
                        };
                    }
                }
            }

            return pagamento;
        }

        // metodo per aggiungere un nuovo pagamento
        public void AddPagamento(Pagamento pagamento)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Pagamenti (DipendenteID, PeriodoPagamentoInizio, PeriodoPagamentoFine, Ammontare, Tipo) VALUES (@DipendenteID, @PeriodoPagamentoInizio, @PeriodoPagamentoFine, @Ammontare, @Tipo)", con);
                cmd.Parameters.AddWithValue("@DipendenteID", pagamento.DipendenteID);
                cmd.Parameters.AddWithValue("@PeriodoPagamentoInizio", pagamento.PeriodoPagamentoInizio);
                cmd.Parameters.AddWithValue("@PeriodoPagamentoFine", pagamento.PeriodoPagamentoFine);
                cmd.Parameters.AddWithValue("@Ammontare", pagamento.Ammontare);
                cmd.Parameters.AddWithValue("@Tipo", pagamento.Tipo);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // metodo per aggiornare un pagamento esistente
        public void UpdatePagamento(Pagamento pagamento)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = @"
                UPDATE Pagamenti 
                SET DipendenteID = @DipendenteID, 
                PeriodoPagamentoInizio = @PeriodoPagamentoInizio, 
                PeriodoPagamentoFine = @PeriodoPagamentoFine, 
                Ammontare = @Ammontare, 
                Tipo = @Tipo 
                WHERE PagamentoID = @PagamentoID";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.Parameters.AddWithValue("@DipendenteID", pagamento.DipendenteID);
                cmd.Parameters.AddWithValue("@PeriodoPagamentoInizio", pagamento.PeriodoPagamentoInizio);
                cmd.Parameters.AddWithValue("@PeriodoPagamentoFine", pagamento.PeriodoPagamentoFine);
                cmd.Parameters.AddWithValue("@Ammontare", pagamento.Ammontare);
                cmd.Parameters.AddWithValue("@Tipo", pagamento.Tipo);
                cmd.Parameters.AddWithValue("@PagamentoID", pagamento.PagamentoID);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // metodo per eliminare un pagamento
        public void DeletePagamento(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Pagamenti WHERE PagamentoID = @PagamentoID", con);
                cmd.Parameters.AddWithValue("@PagamentoID", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}