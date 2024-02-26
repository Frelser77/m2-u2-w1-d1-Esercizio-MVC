using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;

namespace MVCTest.Models
{
    public class Pagamento
    {
        [Display(Name = "ID Pagamento")]
        public int PagamentoID { get; set; }

        [Display(Name = "Periodo Pagamento dal")]
        public DateTime PeriodoPagamentoInizio { get; set; }

        [Display(Name = "Periodo Pagamento al")]
        public DateTime PeriodoPagamentoFine { get; set; }

        public decimal Ammontare { get; set; }

        [Display(Name = "Acconto | Intero")]
        public string Tipo { get; set; }

        [Display(Name = "Numero Dipendente")]
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