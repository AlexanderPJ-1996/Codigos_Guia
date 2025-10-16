using System;
using System.Data.SqlClient;
using System.IO;

class Program
{
    static void Main()
    {
        string rutaCsv = @"C:\ruta\datos.csv";
        string cadenaConexion = @"Server=TU_SERVIDOR;Database=TU_BASE;Trusted_Connection=True;";

        using (var lector = new StreamReader(rutaCsv))
        {
            // Saltar encabezado
            string encabezado = lector.ReadLine();

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();

                while (!lector.EndOfStream)
                {
                    string linea = lector.ReadLine();
                    string[] campos = linea.Split(';');

                    int id = int.Parse(campos[0]);
                    string nombre = campos[1];
                    int edad = int.Parse(campos[2]);

                    string query = "INSERT INTO Personas (Id, Nombre, Edad) VALUES (@Id, @Nombre, @Edad)";
                    using (var comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@Id", id);
                        comando.Parameters.AddWithValue("@Nombre", nombre);
                        comando.Parameters.AddWithValue("@Edad", edad);
                        comando.ExecuteNonQuery();
                    }
                }
            }
        }

        Console.WriteLine("Datos insertados correctamente.");
    }
}