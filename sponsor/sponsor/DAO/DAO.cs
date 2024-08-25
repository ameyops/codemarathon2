using Npgsql;
using sponsor.Model;
using System.Collections.Generic;

namespace sponsor.data
{
    public class DAO
    {
        private readonly string _connectionString;

        public DAO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgreDB");
        }

        public void AddPayment(Payment payment)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = @"INSERT INTO Payments (ContractID, PaymentDate, AmountPaid, PaymentStatus) 
                            VALUES (@ContractID, @PaymentDate, @AmountPaid, @PaymentStatus)";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@ContractID", payment.ContractID);
                    cmd.Parameters.AddWithValue("@PaymentDate", payment.PaymentDate);
                    cmd.Parameters.AddWithValue("@AmountPaid", payment.AmountPaid);
                    cmd.Parameters.AddWithValue("@PaymentStatus", payment.PaymentStatus);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        \
        public bool ValidatePaymentForMatch(int matchID)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = @"
                    SELECT COUNT(*) 
                    FROM Payments p 
                    JOIN Contracts c ON p.ContractID = c.ContractID 
                    WHERE c.MatchID = @MatchID 
                    AND p.PaymentDate IS NOT NULL 
                    AND p.AmountPaid IS NOT NULL";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@MatchID", matchID);
                    var result = (long)cmd.ExecuteScalar();
                    return result > 0;
                }
            }
        }

        
        public List<SponsorPaymentDetails> GetSponsorsWithPaymentDetails()
        {
            var sponsors = new List<SponsorPaymentDetails>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = @"
                    SELECT s.SponsorName, COUNT(p.PaymentID), SUM(p.AmountPaid), MAX(p.PaymentDate)
                    FROM Sponsors s
                    JOIN Contracts c ON s.SponsorID = c.SponsorID
                    JOIN Payments p ON c.ContractID = p.ContractID
                    GROUP BY s.SponsorName";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sponsors.Add(new SponsorPaymentDetails
                            {
                                SponsorName = reader.GetString(0),
                                PaymentCount = reader.GetInt32(1),
                                TotalPayments = reader.GetDecimal(2),
                                LatestPaymentDate = reader.GetDateTime(3)
                            });
                        }
                    }
                }
            }
            return sponsors;
        }

        
        public List<Match> GetMatchesWithPaymentDetails()
        {
            var matches = new List<Match>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = @"
                    SELECT m.MatchName, m.MatchDate, m.Location, SUM(p.AmountPaid)
                    FROM Matches m
                    JOIN Contracts c ON m.MatchID = c.MatchID
                    JOIN Payments p ON c.ContractID = p.ContractID
                    GROUP BY m.MatchName, m.MatchDate, m.Location";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            matches.Add(new Match
                            {
                                MatchName = reader.GetString(0),
                                MatchDate = reader.GetDateTime(1),
                                Location = reader.GetString(2),
                                Sum = reader.GetInt32(3)
                            });
                        }
                    }
                }
            }
            return matches;
        }

        
        public List<SponsorMatchCount> GetSponsorsWithMatchCountByYear(int year)
        {
            var sponsors = new List<SponsorMatchCount>();
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var sql = @"
                    SELECT s.SponsorName, COUNT(m.MatchID) AS MatchCount
                    FROM Sponsors s
                    JOIN Contracts c ON s.SponsorID = c.SponsorID
                    JOIN Matches m ON c.MatchID = m.MatchID
                    WHERE EXTRACT(YEAR FROM m.MatchDate) = @Year
                    GROUP BY s.SponsorName";

                using (var cmd = new NpgsqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Year", year);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sponsors.Add(new SponsorMatchCount
                            {
                                SponsorName = reader.GetString(0),
                                MatchCount = reader.GetInt32(1)
                            });
                        }
                    }
                }
            }
            return sponsors;
        }
    }
}
