using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace SME
{
    public static class Database
    {
        private static OracleConnection oc;

        /// <summary>
        /// Het initiëren van de databaseinstellingen
        /// </summary>
        static Database()
        {
            oc = new OracleConnection();
            oc.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        }

        /// <summary>
        /// Het ophalen van informatie uit de database
        /// </summary>
        /// <param name="query"></param>
        /// <returns>En een DataTable met de opgehaalde infromatie</returns>
        public static DataTable GetData(string query, Dictionary<string, object> waardes = default(Dictionary<string, object>))
        {
            DataTable dt = new DataTable();

            try
            {
                oc.Open();
                OracleCommand command = toOracleCommand(query, waardes);
                OracleDataAdapter adapter = new OracleDataAdapter(command);

                adapter.Fill(dt);
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message, Database.ParseParameters(query, waardes));
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                oc.Close();
            }

            return dt;

        }

        /// <summary>
        /// Het uitvoeren van een SQL-commando
        /// </summary>
        /// <param name="query"></param>
        public static void Execute(string query, Dictionary<string, object> waardes = default(Dictionary<string, object>))
        {
            try
            {
                oc.Open();
                OracleCommand command = toOracleCommand(query, waardes);
                command.ExecuteNonQuery();
            }
            catch (OracleException ex)
            {
                throw new DatabaseException(ex.Message, Database.ParseParameters(query, waardes));
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                oc.Close();
            }
        }

        private static OracleCommand toOracleCommand(string query, Dictionary<string, object> waardes = default(Dictionary<string, object>))
        {
            query = ParseParameters(query, waardes);

            OracleCommand command = new OracleCommand(query, oc);

            return command;
        }

        public static string ParseParameters(string query, Dictionary<string, object> waardes = default(Dictionary<string, object>))
        {
            if (waardes != null && waardes.Count > 0)
            {
                foreach (KeyValuePair<string, object> waarde in waardes)
                {
                    string vervang = "";
                    if (waarde.Value is Int32 || waarde.Value.ToString() == "NULL")
                    {
                        vervang = waarde.Value.ToString();
                    }
                    else if (waarde.Value is List<string>)
                    {
                        int aantal = ((List<string>)waarde.Value).Count;
                        int count = 0;
                        foreach (string value in (List<string>)waarde.Value)
                        {
                            vervang += Quote(value);
                            count++;
                            if (count != aantal)
                            {
                                vervang += ",";
                            }
                        }
                    }
                    else
                    {
                        vervang = Quote(waarde.Value.ToString());
                    }

                    query = query.Replace(waarde.Key, vervang);

                    // Oracle Command werkt op een of andere manier niet, daarom custom parameters.
                    //command.Parameters.Add(new OracleParameter(waarde.Key, waarde.Value).Value);
                }
            }

            return query;
        }

        public static string Quote(string waarde)
        {
            return "'" + Escape(waarde) + "'";
        }

        public static string Escape(string waarde)
        {
            return waarde.Replace("'", "\'");
        }

        public static int GetSequence(string naam)
        {
            return Convert.ToInt32(Database.GetData("SELECT " + naam + ".nextval FROM dual").Rows[0]["NEXTVAL"]);
        }
    }

    class DatabaseException : Exception
    {
        public DatabaseException(string message, string query)
            : base(GenerateMessage(message, query))
        {

        }

        public static string GenerateMessage(string message, string query)
        {
            return message + " " + query;
        }
    }
}
