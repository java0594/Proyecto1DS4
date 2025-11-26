using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using Proyecto2.Models;


namespace Proyecto2.DAL
{
    public class OperacionesDAL
    {
        private string cnn = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;

       
        public List<Operacion> GetOperaciones()
        {
            var lista = new List<Operacion>();
            using (var con = new SqlConnection(cnn))
            {
                con.Open();
                string query = @"
                    SELECT o.Id, o.Expresion, o.Resultado, o.IdTipo, t.Nombre as NombreTipo
                    FROM Operaciones o
                    INNER JOIN TipoCalculo t ON o.IdTipo = t.Id";
                using (var cmd = new SqlCommand(query, con))
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Operacion
                        {
                            Id = (int)dr["Id"],
                            Expresion = dr["Expresion"].ToString(),
                            Resultado = (double)dr["Resultado"],
                            IdTipo = (int)dr["IdTipo"],
                            NombreTipo = dr["NombreTipo"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        
        public List<Operacion> GetOperacionesPorTipo(int idTipo)
        {
            var lista = new List<Operacion>();
            using (var con = new SqlConnection(cnn))
            {
                con.Open();
                string query = @"
                    SELECT o.Id, o.Expresion, o.Resultado, o.IdTipo, t.Nombre as NombreTipo
                    FROM Operaciones o
                    INNER JOIN TipoCalculo t ON o.IdTipo = t.Id
                    WHERE o.IdTipo = @tipo";
                using (var cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@tipo", idTipo);
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Operacion
                            {
                                Id = (int)dr["Id"],
                                Expresion = dr["Expresion"].ToString(),
                                Resultado = (double)dr["Resultado"],
                                IdTipo = (int)dr["IdTipo"],
                                NombreTipo = dr["NombreTipo"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        
    }
}






