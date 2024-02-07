using Microsoft.Data.SqlClient;
using StateViewApplication.Models;
using System.Data;

namespace StateViewApplication.Data
{
    public class DatabaseHelper
    {
        private string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<TreeViewNode> GetNodes()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.GetNodes", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            nodes.Add(new TreeViewNode
                            {
                                id = reader["Id"].ToString(),
                                parent = reader["ParentId"].ToString(),
                                text = reader["Text"].ToString()
                            });
                        }
                    }
                }
            }

            return nodes;
        }

        public void InsertNode(TreeViewNode node)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.InsertNode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", node.id);
                    command.Parameters.AddWithValue("@ParentId", node.parent);
                    command.Parameters.AddWithValue("@Text", node.text);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateNode(TreeViewNode node)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.UpdateNode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", node.id);
                    command.Parameters.AddWithValue("@ParentId", node.parent);
                    command.Parameters.AddWithValue("@Text", node.text);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteNode(string id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("dbo.DeleteNode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
