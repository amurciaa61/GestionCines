using Microsoft.Data.Sqlite;
using System;
using System.Collections.ObjectModel;

namespace GestionCines
{
    class ServicioBaseDatos
    {
        const int MAX_SESIONES_POR_SALA = 3;
        private readonly SqliteConnection conexion;
        public SqliteCommand comando, comando1, comando2;
        private string FECHADELDIA { get; set; }

        public ServicioBaseDatos()
        {
            conexion = new SqliteConnection("Data Source=cines.db");
            FECHADELDIA = FormatearFechaDelDia();
        }
        public ObservableCollection<Sala> ObtenerSalas(bool soloDisponibles, bool insertarFilaVacia)
        {
            ObservableCollection<Sala> salas = new ObservableCollection<Sala>();
            if (insertarFilaVacia)
            {
                salas.Add(new Sala());
                salas[0].NUMERO = "Todas";
            }
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * from salas ";
            // Si solo disponibles, además comprobaremos que no tengan más de MAX_SESIONES_POR_SALA
            if (soloDisponibles)
            {
                comando.CommandText += "WHERE disponible = true " +
                                       "and (select count(*) from sesiones where sala = idSala) < " + MAX_SESIONES_POR_SALA;
            }
            comando.CommandText += " ORDER BY numero";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    salas.Add(new Sala(lector.GetInt32(0), lector.GetString(1), lector.GetInt32(2), lector.GetBoolean(3)));
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
        // No puede haber dos salas con el mismo número
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
        public string FormatearFechaDelDia()
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
            // Copiamos sesiones a sesionescopia para luego recuperarlas
            comando.CommandText = "INSERT INTO sesionescopia SELECT * FROM sesiones";
            comando.ExecuteNonQuery();
            comando.CommandText = "DELETE FROM sesiones";
            comando.ExecuteNonQuery();
            comando.CommandText = "DELETE FROM peliculas";
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public ObservableCollection<Pelicula> ObtenerPeliculas(bool insertarFilaVacia)
        {
            ObservableCollection<Pelicula> peliculas = new ObservableCollection<Pelicula>();
            if (insertarFilaVacia)
            {
                peliculas.Add(new Pelicula());
                peliculas[0].TITULO = "Todas";
            }
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT * FROM peliculas ORDER BY titulo";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    peliculas.Add(new Pelicula(lector.GetInt32(0), lector.GetString(1), lector.GetString(2),
                        lector.GetInt32(3), lector.GetString(4), lector.GetString(5)));
                }
            }
            lector.Close();
            conexion.Close();
            return peliculas;
        }
        public ObservableCollection<Sesion> ObtenerSesiones()
        {
            ObservableCollection<Sesion> sesiones = new ObservableCollection<Sesion>();
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "select idsesion,p.*,s.*,hora from sesiones " +
                                  "join peliculas p " +
                                  "on p.idPelicula = sesiones.pelicula " +
                                  "join salas s " +
                                  "on sesiones.sala = s.idSala " +
                                  "ORDER BY p.titulo,hora";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    sesiones.Add(new Sesion(lector.GetInt32(0),
                        new Pelicula(lector.GetInt32(1), lector.GetString(2), lector.GetString(3), lector.GetInt32(4), lector.GetString(5), lector.GetString(6)),
                        new Sala(lector.GetInt32(7), lector.GetString(8), lector.GetInt32(9), lector.GetBoolean(10)),
                        lector.GetString(11)));
                }
            }
            lector.Close();
            conexion.Close();
            return sesiones;
        }

        public ObservableCollection<string> ObtenerSesionesFiltro()
        {
            ObservableCollection<string> sesiones = new ObservableCollection<string>();
            sesiones.Add("Todas");
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "select hora from sesiones " +
                                  "GROUP BY hora " +
                                  "ORDER BY hora";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    sesiones.Add(lector.GetString(0));
                }
            }
            lector.Close();
            conexion.Close();
            return sesiones;
        }
        public void InsertarSesion(Sesion sesionFormulario)
        {
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO sesiones VALUES(null,@pelicula,@sala,@hora)";
            comando.Parameters.Add("@pelicula", SqliteType.Integer);
            comando.Parameters["@pelicula"].Value = sesionFormulario.PELICULA.ID;
            comando.Parameters.Add("@sala", SqliteType.Integer);
            comando.Parameters["@sala"].Value = sesionFormulario.SALA.IDSALA;
            comando.Parameters.Add("@hora", SqliteType.Text);
            comando.Parameters["@hora"].Value = sesionFormulario.HORA;
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        // restauramos las sesiones desde sesionescopia tras la eliminación de la cartelera
        public void RestaurarSesiones()
        {
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO sesiones SELECT * FROM sesionescopia " +
                                  "WHERE EXISTS " +
                                  "(SELECT * FROM peliculas " +
                                  "WHERE pelicula = idpelicula) ";
            comando.ExecuteNonQuery();
            // Borramos copia de sesiones
            comando.CommandText = "DELETE FROM sesionescopia";
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public void ActualizarSesion(Sesion sesionFormulario)
        {
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "UPDATE sesiones SET pelicula = @pelicula, sala = @sala, hora = @hora" +
                " WHERE idSesion = @idSesion";
            comando.Parameters.Add("@idSesion", SqliteType.Integer);
            comando.Parameters["@idSesion"].Value = sesionFormulario.IDSESION;
            comando.Parameters.Add("@pelicula", SqliteType.Integer);
            comando.Parameters["@pelicula"].Value = sesionFormulario.PELICULA.ID;
            comando.Parameters.Add("@sala", SqliteType.Integer);
            comando.Parameters["@sala"].Value = sesionFormulario.SALA.IDSALA;
            comando.Parameters.Add("@hora", SqliteType.Text);
            comando.Parameters["@hora"].Value = sesionFormulario.HORA;
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public void BorrarSesion(Sesion sesionFormulario)
        {
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "DELETE FROM sesiones WHERE idSesion = @idSesion";
            comando.Parameters.Add("@idSesion", SqliteType.Integer);
            comando.Parameters["@idSesion"].Value = sesionFormulario.IDSESION;
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public ObservableCollection<Informe> ObtenerInformeGeneral(string filtro)
        {
            ObservableCollection<Informe> listado = new ObservableCollection<Informe>();
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "select se.hora,p.*,s.*,se.idSesion from peliculas p " +
                      "LEFT join sesiones se " +
                      "on p.idPelicula = se.pelicula " +
                      "LEFT join salas s " +
                      "on se.sala = s.idSala ";
            comando.CommandText += filtro +
                      " ORDER BY p.titulo,se.hora,se.sala";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                Sala sala;
                Pelicula pelicula;
                string hora;
                int ventas;
                while (lector.Read())
                {
                    pelicula = new Pelicula(lector.GetInt32(1), lector.GetString(2), lector.GetString(3), lector.GetInt32(4), lector.GetString(5), lector.GetString(6));

                    if (PeliculaTieneSesiones(lector.GetInt32(1)))
                    {
                        sala = new Sala(lector.GetInt32(7), lector.GetString(8), lector.GetInt32(9), lector.GetBoolean(10));
                        hora = lector.GetString(0);
                        ventas = ObtenerVentasPorSesion(lector.GetInt32(11));
                    }
                    else
                    {
                        sala = new Sala();
                        hora = "";
                        ventas = 0;
                    }
                    listado.Add(new Informe(pelicula, sala, hora, ventas));
                }
            }
            lector.Close();
            conexion.Close();
            return listado;
        }
        public bool PeliculaTieneSesiones(int iDpelicula)
        {
            int resultado;
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT COUNT(*) FROM sesiones WHERE pelicula = @pelicula";
            comando.Parameters.Add("@pelicula", SqliteType.Integer);
            comando.Parameters["@pelicula"].Value = iDpelicula;
            if (Convert.IsDBNull(comando.ExecuteScalar()))
                resultado = 0;
            else
                resultado = Convert.ToInt32(comando.ExecuteScalar());
            return resultado > 0;
        }
        public int ObtenerVentasPorSesion(int sesionId)
        {
            int resultado;
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "SELECT SUM(cantidad) FROM ventas WHERE sesion = @sesion";
            comando.Parameters.Add("@sesion", SqliteType.Integer);
            comando.Parameters["@sesion"].Value = sesionId;
            if (Convert.IsDBNull(comando.ExecuteScalar()))
                resultado = 0;
            else
                resultado = Convert.ToInt32(comando.ExecuteScalar());
            return resultado;
        }
        public ObservableCollection<Informe> ObtenerInformeDetalle()
        {
            ObservableCollection<Informe> listado = new ObservableCollection<Informe>();
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "select se.hora,p.titulo,s.numero,v.idVenta,v.cantidad,v.pago from peliculas p " +
                      "JOIN sesiones se " +
                      "on p.idPelicula = se.pelicula " +
                      "JOIN salas s " +
                      "on se.sala = s.idSala " +
                      "JOIN ventas v " +
                      "on v.sesion = se.idSesion";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    listado.Add(new Informe(lector.GetString(1), lector.GetString(0), lector.GetString(2), lector.GetInt32(3),
                                            lector.GetInt32(4), lector.GetString(5)));
                }
            }
            lector.Close();
            conexion.Close();
            return listado;
        }
        public ObservableCollection<OfertaDisponible> ObtenerOfertaDisponible()
        {
            ObservableCollection<OfertaDisponible> ofertas = new ObservableCollection<OfertaDisponible>();
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "select se.hora,p.*,s.*,se.idSesion from peliculas p " +
                      "join sesiones se " +
                      "on p.idPelicula = se.pelicula " +
                      "join salas s " +
                      "on se.sala = s.idSala " +
                      "WHERE s.disponible = true";  // Solo salas disponibles
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    int capacidad = lector.GetInt32(9);  // capacidad de la sala
                    int disponibilidad = capacidad - ObtenerVentasPorSesion(lector.GetInt32(11)); // entradas disponibles en la sesion
                    if (disponibilidad > 0)
                        ofertas.Add(new OfertaDisponible(lector.GetString(2), lector.GetString(0), lector.GetString(8),
                                                         disponibilidad, lector.GetInt32(11), lector.GetString(3)));
                }
            }
            lector.Close();
            conexion.Close();
            return ofertas;
        }
        public ObservableCollection<string> ObtenerFormaDePago()
        {
            ObservableCollection<string> formaDePago = new ObservableCollection<string>();
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "select * FROM formadepago";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    formaDePago.Add(lector.GetString(0));
                }
            }
            lector.Close();
            conexion.Close();
            return formaDePago;
        }
        public void InsertarVenta(OfertaDisponible ventaFormulario)
        {
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "INSERT INTO ventas VALUES(null,@sesion,@cantidad,@pago)";
            comando.Parameters.Add("@sesion", SqliteType.Integer);
            comando.Parameters["@sesion"].Value = ventaFormulario.IDSESION;
            comando.Parameters.Add("@cantidad", SqliteType.Integer);
            comando.Parameters["@cantidad"].Value = ventaFormulario.CANTIDAD;
            comando.Parameters.Add("@pago", SqliteType.Text);
            comando.Parameters["@pago"].Value = ventaFormulario.PAGO;
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public ObservableCollection<string> ObtenerDatosFiltro(string campo)
        {
            ObservableCollection<string> datos = new ObservableCollection<string>();
            datos.Add("Todas");
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "select " + campo + " from peliculas " +
                      "GROUP BY 1 " +
                      "ORDER BY 1";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    datos.Add(lector.GetString(0));
                }
            }
            lector.Close();
            conexion.Close();
            return datos;
        }
        public ObservableCollection<string> ObtenerHoras(bool insertarFilaVacia)
        {
            ObservableCollection<string> horas = new ObservableCollection<string>();
            if (insertarFilaVacia)
                horas.Add("Todas");
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "select hora from horas " +
                      "ORDER BY 1";
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    horas.Add(lector.GetString(0));
                }
            }
            lector.Close();
            conexion.Close();
            return horas;
        }
        public Sala ObtenerSalaPorNumero(string numero)
        {
            Sala sala = null;
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "select * from salas " +
                      "WHERE numero = @numero LIMIT 1";

            comando.Parameters.Add("@numero", SqliteType.Text);
            comando.Parameters["@numero"].Value = numero;
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    sala = new Sala(lector.GetInt32(0), lector.GetString(1), lector.GetInt32(2), lector.GetBoolean(3));
                }
            }
            lector.Close();
            conexion.Close();
            return sala;
        }
        public Pelicula ObtenerPeliculaPorTitulo(string titulo)
        {
            Pelicula pelicula = null;
            conexion.Open();
            comando = conexion.CreateCommand();
            comando.CommandText = "select * from peliculas " +
                      "WHERE titulo = @titulo LIMIT 1";

            comando.Parameters.Add("@titulo", SqliteType.Text);
            comando.Parameters["@titulo"].Value = titulo;
            SqliteDataReader lector = comando.ExecuteReader();
            if (lector.HasRows)
            {
                while (lector.Read())
                {
                    pelicula = new Pelicula(lector.GetInt32(0),
                        lector.GetString(1),
                        lector.GetString(2),
                        lector.GetInt32(3),
                        lector.GetString(4),
                        lector.GetString(5));
                }
            }
            lector.Close();
            conexion.Close();
            return pelicula;
        }
    }
}
