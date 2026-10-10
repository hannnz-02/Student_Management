using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Student_Management.Models;

namespace Student_Management.Data
{
    public class StudentRepository
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Student_Management_DB;Integrated Security=True;TrustServerCertificate=True;";
        public List<Student> GetAllStudents(string keyword = "")
        {
            List<Student> students = new List<Student>();
            string query = "SELECT Id, Nama, NRP, Jurusan, Email FROM Mahasiswa";

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query += " WHERE Nama LIKE @Keyword OR NRP LIKE @Keyword OR Jurusan LIKE @Keyword";
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrWhiteSpace(keyword))
                    {
                        cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    }

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(new Student
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nama = reader["Nama"].ToString(),
                                NRP = reader["NRP"].ToString(),
                                Jurusan = reader["Jurusan"].ToString(),
                                Email = reader["Email"].ToString()
                            });
                        }
                    }
                }
            }
            return students;
        }

        public void AddStudent(Student student)
        {
            string query = "INSERT INTO Mahasiswa (Nama, NRP, Jurusan, Email) VALUES (@Nama, @NRP, @Jurusan, @Email)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nama", student.Nama);
                    cmd.Parameters.AddWithValue("@NRP", student.NRP);
                    cmd.Parameters.AddWithValue("@Jurusan", student.Jurusan);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateStudent(Student student)
        {
            string query = "UPDATE Mahasiswa SET Nama=@Nama, NRP=@NRP, Jurusan=@Jurusan, Email=@Email WHERE Id=@Id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", student.Id);
                    cmd.Parameters.AddWithValue("@Nama", student.Nama);
                    cmd.Parameters.AddWithValue("@NRP", student.NRP);
                    cmd.Parameters.AddWithValue("@Jurusan", student.Jurusan);
                    cmd.Parameters.AddWithValue("@Email", student.Email);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteStudent(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string queryDelete = "DELETE FROM Mahasiswa WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(queryDelete, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }

                string queryCount = "SELECT COUNT(*) FROM Mahasiswa";
                using (SqlCommand cmdCount = new SqlCommand(queryCount, conn))
                {
                    int count = (int)cmdCount.ExecuteScalar();
                    if (count == 0)
                    {
                        string queryReset = "DBCC CHECKIDENT ('Mahasiswa', RESEED, 0)";
                        using (SqlCommand cmdReset = new SqlCommand(queryReset, conn))
                        {
                            cmdReset.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public int GetStudentCountByDepartment(string jurusan = "")
        {
            string query = string.IsNullOrWhiteSpace(jurusan)
                ? "SELECT COUNT(*) FROM Mahasiswa"
                : "SELECT COUNT(*) FROM Mahasiswa WHERE Jurusan = @Jurusan";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrWhiteSpace(jurusan))
                    {
                        cmd.Parameters.AddWithValue("@Jurusan", jurusan);
                    }
                    return (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}