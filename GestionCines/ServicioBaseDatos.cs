using Microsoft.Data.Sqlite;
using System;
using System.Collections.ObjectModel;

namespace GestionCines
{
    class ServicioBaseDatos
    {
        private readonly SqliteConnection conexion;
        private SqliteCommand comando;
        private string FECHADELDIA { get; set; }

        public ServicioBaseDatos()
        {
            conexion = new SqliteConnection("Data Source=cines.db");
            FECHADELDIA = formatearFechaDelDia();
        }
        public ObservableCollection<Sala> ObtenerSalas()
        {
            ObservableCollection<Sala> salas = new ObservableCollection<Sala>();
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * from salas";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    salas.Add(new Sala(lector.GetInt32(0), lector.GetString(1), lector.GetInt32(2),lector.GetBoolean(3)));
                }
            }
            lector.Close();
            conexion.Close();
            return salas;
        }
        public void InsertarSala(Sala salaFormulario)
        {
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO Salas VALUES(null,@numero,@capacidad,@disponible)";
            comando.Parameters.Add("@numero", SqliteType.Text);
            comando.Parameters["@numero"].Value = salaFormulario.NUMERO;
            comando.Parameters.Add("@capacidad", SqliteType.Integer);
            comando.Parameters["@capacidad"].Value = salaFormulario.CAPACIDAD;
            comando.Parameters.Add("@disponible", SqliteType.Integer);
            comando.Parameters["@disponible"].Value = salaFormulario.DISPONIBLE;
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public void ActualizarSala(Sala salaFormulario)
        {
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE salas SET numero = @numero, capacidad = @capacidad, disponible = @disponible" +
                " WHERE idSala = @idSala";
            comando.Parameters.Add("@idSala", SqliteType.Integer);
            comando.Parameters["@idSala"].Value = salaFormulario.IDSALA;
            comando.Parameters.Add("@numero", SqliteType.Text);
            comando.Parameters["@numero"].Value = salaFormulario.NUMERO;
            comando.Parameters.Add("@capacidad", SqliteType.Integer);
            comando.Parameters["@capacidad"].Value = salaFormulario.CAPACIDAD;
            comando.Parameters.Add("@disponible", SqliteType.Integer);
            comando.Parameters["@disponible"].Value = salaFormulario.DISPONIBLE;
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public bool ExisteSala(Sala salaFormulario)
        {
            bool existe = false;
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT numero FROM salas WHERE numero = @numero AND idSala <> @idSala";
            comando.Parameters.Add("@numero", SqliteType.Text);
            comando.Parameters["@numero"].Value = salaFormulario.NUMERO;
            comando.Parameters.Add("@idSala", SqliteType.Integer);
            comando.Parameters["@idSala"].Value = salaFormulario.IDSALA;
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read() && !existe)
                {
                    existe = true;
                }
            }
            lector.Close();
            conexion.Close();
            return existe;
        }
        public string formatearFechaDelDia()
        {
            DateTime hoy = DateTime.Today;
            return hoy.ToString("yyyy-MM-dd");
        }
        public bool ComprobarCargaPeliculas()
        {
            bool cargadas = false;
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT fechacarga FROM cargapeliculas WHERE Date(fechacarga) = @fechacarga";
            comando.Parameters.Add("@fechacarga", SqliteType.Text);
            comando.Parameters["@fechacarga"].Value = FECHADELDIA;
            string resultado = Convert.ToString(comando.ExecuteScalar());
            if (resultado.Length > 0)
                cargadas = true;
            conexion.Close();
            return cargadas;
        }
        public void InsertarControlCargaPeliculas()
        {
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO cargapeliculas VALUES(@fechacarga)";
            comando.Parameters.Add("@fechacarga", SqliteType.Text);
            comando.Parameters["@fechacarga"].Value = FECHADELDIA;
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public void EliminarControlesCargaPeliculas()
        {
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM cargapeliculas WHERE Date(fechacarga) < @fechacarga";
            comando.Parameters.Add("@fechacarga", SqliteType.Text);
            comando.Parameters["@fechacarga"].Value = FECHADELDIA;
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public void CargarPeliculas(ObservableCollection<Pelicula> peliculas)
        {
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO peliculas VALUES(@idPelicula,@titulo,@cartel,@año,@genero,@calificacion)";
            comando.Parameters.Add("@idPelicula", SqliteType.Text);
            comando.Parameters.Add("@titulo", SqliteType.Text);
            comando.Parameters.Add("@cartel", SqliteType.Text);
            comando.Parameters.Add("@año", SqliteType.Text);
            comando.Parameters.Add("@genero", SqliteType.Text);
            comando.Parameters.Add("@calificacion", SqliteType.Text);
            foreach (var pelicula in peliculas)
            {
                comando.Parameters["@idPelicula"].Value = pelicula.ID;
                comando.Parameters["@titulo"].Value = pelicula.TITULO;
                comando.Parameters["@cartel"].Value = pelicula.CARTEL;
                comando.Parameters["@año"].Value = pelicula.AÑO;
                comando.Parameters["@genero"].Value = pelicula.GENERO;
                comando.Parameters["@calificacion"].Value = pelicula.CALIFICACION;
                comando.ExecuteNonQuery();
            }
            conexion.Close();
        }
        public void EliminarCartelera()
        {
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM ventas";
            comando.ExecuteNonQuery();
            comando.CommandText = "DELETE FROM sesiones";
            comando.ExecuteNonQuery();
            comando.CommandText = "DELETE FROM peliculas";
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public ObservableCollection<Pelicula> ObtenerPeliculas()
        {
            ObservableCollection<Pelicula> peliculas = new ObservableCollection<Pelicula>();
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM peliculas";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    peliculas.Add(new Pelicula(lector.GetInt32(0),lector.GetString(1),lector.GetString(2),
                        lector.GetInt32(3),lector.GetString(4),lector.GetString(5)));
                }
            }
            lector.Close();
            conexion.Close();
            return peliculas;
        }

    }
}
