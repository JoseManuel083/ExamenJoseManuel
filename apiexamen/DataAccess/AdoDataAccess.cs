using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using apiexamen.Interfaces;
using WsApiexamen.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using apiexamen.Models;
using System.Security.Cryptography.Xml;

public class AdoDataAccess : IDataAccess
{
    private readonly string _connectionString;

    public AdoDataAccess(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<TblExaman>> ObtenerExamenes()
    {
        try
        {
            var examenes = new List<TblExaman>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("sPConsultar", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                examenes.Add(new TblExaman
                {
                    IdExamen = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Descripcion = reader.GetString(2)
                });
            }

            return examenes;
        }
        catch (Exception)
        {
            return null;
        }
     
    }

    public async Task<List<TblExaman>> ConsultarExamen(int id)
    {
        try
        {
            var examenes = new List<TblExaman>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SELECT * FROM tblExamen WHERE IdExamen = @id", connection);
            command.Parameters.AddWithValue("@id", id);

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                examenes.Add(new TblExaman
                {
                    IdExamen = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    Descripcion = reader.GetString(2)
                });
            }

            return examenes;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> AgregarExamen(TblExaman examen)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("spAgregar", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Id", examen.IdExamen);
            command.Parameters.AddWithValue("@Nombre", examen.Nombre);
            command.Parameters.AddWithValue("@Descripcion", examen.Descripcion);

            connection.Open();

            using var reader = await command.ExecuteReaderAsync();

            Retorno retorno = new Retorno();

            reader.Read();
            retorno.Codigo = reader.GetInt32(0);

            reader.NextResult();
            reader.Read();
            retorno.Descripcion = reader.GetString(0);

            return retorno.Codigo == 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> ActualizarExamen(TblExaman examen)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("spActualizarExamen", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@id", examen.IdExamen);
            command.Parameters.AddWithValue("@nombre", examen.Nombre);
            command.Parameters.AddWithValue("@descripcion", examen.Descripcion);

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();

            Retorno retorno = new Retorno();

            reader.Read();
            retorno.Codigo = reader.GetInt32(0);

            reader.NextResult();
            reader.Read();
            retorno.Descripcion = reader.GetString(0);

            return retorno.Codigo == 0;
        }
        catch (SqlException ex)
        {
            return false;
        }

    }

    public async Task<bool> EliminarExamen(int id)
    {
        try
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("spEliminar", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = await command.ExecuteReaderAsync();

            Retorno retorno = new Retorno();

            reader.Read();
            retorno.Codigo = reader.GetInt32(0);

            reader.NextResult();
            reader.Read();
            retorno.Descripcion = reader.GetString(0);

            return retorno.Codigo == 0;
        }
        catch (SqlException ex)
        {
            return false; 
        }
    }
}