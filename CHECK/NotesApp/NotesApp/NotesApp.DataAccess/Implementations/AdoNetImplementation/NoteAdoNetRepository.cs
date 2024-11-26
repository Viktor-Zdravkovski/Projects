using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NotesApp.Domain.Enums;
using NotesApp.Domain.Models;

namespace NotesApp.DataAccess.Implementations.AdoNetImplementation
{
    public class NoteAdoNetRepository : IRepository<Note>
    {
        private readonly string _connectionString;

        public NoteAdoNetRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("NotesAppSqlExpress");
        }

        public List<Note> GetAll()
        {
            var notes = new List<Note>();

            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string query = @"SELECT n.Id, n.Text, n.Priority, n.Tag, n.UserId, u.FirstName, u.LastName
                                 FROM dbo.Note n
                                 JOIN dbo.[User] u ON n.UserId = u.Id";

                using SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                using SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    var note = new Note()
                    {
                        Id = sqlDataReader.GetInt32(0),
                        Text = sqlDataReader.GetString(1),
                        Priority = (Priority)sqlDataReader.GetInt32(2),
                        Tag = (Tag)sqlDataReader.GetInt32(3),
                        UserId = sqlDataReader.GetInt32(4),
                        User = new User
                        {
                            FirstName = sqlDataReader.GetString(5),
                            LastName = sqlDataReader.GetString(6),
                        }
                    };
                    notes.Add(note);
                }
            }
            return notes;
        }

        public Note GetById(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string query = @"SELECT n.Id, n.Text, n.Priority, n.Tag, n.UserId, u.FirstName, u.LastName
                                 FROM dbo.Note n
                                 JOIN dbo.[User] u ON n.UserId = u.Id
                                 WHERE n.Id = @NoteId";

                using SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@NoteId", id);

                using SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.Read())
                {
                    var note = new Note
                    {
                        Id = sqlDataReader.GetInt32(0),
                        Text = sqlDataReader.GetString(1),
                        Priority = (Priority)sqlDataReader.GetInt32(2),
                        Tag = (Tag)sqlDataReader.GetInt32(3),
                        UserId = sqlDataReader.GetInt32(4),
                        User = new User
                        {
                            FirstName = sqlDataReader.GetString(5),
                            LastName = sqlDataReader.GetString(6),
                        }
                    };

                    return note;
                }
                return null;
            }
        }

        public void Add(Note entity)
        {
            using (SqlConnection sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();

                string query = "INSERT INTO dbo.Note (Text, Priority, Tag, UserId) " +
                               "VALUES (@Text, @Priority, @TagEnum, @UserId)";

                using SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@Text", entity.Text);
                sqlCommand.Parameters.AddWithValue("@Priority", entity.Priority);
                sqlCommand.Parameters.AddWithValue("@TagEnum", entity.Tag);
                sqlCommand.Parameters.AddWithValue("@UserId", entity.UserId);

                sqlCommand.ExecuteNonQuery();
            }
        }

        public void Update(Note entity)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"UPDATE dbo.Note
                             SET Text = @Text, Priority = @Priority, Tag = @Tag, UserId = @UserId
                             WHERE Id = @NoteId";

            using SqlCommand sqlCommand = new SqlCommand( query, connection);
            sqlCommand.Parameters.AddWithValue("@NoteId", entity.Id);
            sqlCommand.Parameters.AddWithValue("@Text", entity.Text);
            sqlCommand.Parameters.AddWithValue("@Priority", entity.Priority);
            sqlCommand.Parameters.AddWithValue("@Tag", entity.Tag);
            sqlCommand.Parameters.AddWithValue("@UserId", entity.UserId);

            sqlCommand.ExecuteNonQuery();
        }

        public void Delete(Note entity)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "DELETE FROM dbo.Note WHERE Id = @noteId";
            using SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@noteId", entity.Id);

            sqlCommand.ExecuteNonQuery();
        }

    }
}
