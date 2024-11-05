using System.Data.SQLite;

public class PresupuestoRepository : IPresupuestoRepository
{
    private string _cadenaDeConexion;
    public PresupuestoRepository(string cadenaDeConexion)
    {
        _cadenaDeConexion = cadenaDeConexion;
    }
    public List<Presupuestos> ListarPresupuestos()
    {
        var presupuestos = new List<Presupuestos>();
        var query = "SELECT * FROM Presupuestos";
        using (SQLiteConnection conexion = new SQLiteConnection(_cadenaDeConexion))
        {
            conexion.Open();
            var command = new SQLiteCommand(query, conexion);

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Presupuestos presupuesto = new Presupuestos()
                    {
                        IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]),
                        NombreDestinatario = reader["NombreDestinatario"].ToString(),
                        FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"])
                    };
                    presupuestos.Add(presupuesto);
                }
            }
            conexion.Close();
        }
        return presupuestos;
    }

    public Presupuestos ObtenerPresupuesto(int idPresupuesto)
    {
        Presupuestos presupuesto = null;
        var query = "SELECT P.idPresupuesto, P.NombreDestinatario, P.FechaCreacion, PR.idProducto, " +
                    "PR.Descripcion AS Producto, PR.Precio,PD.Cantidad " +
                    "FROM Presupuestos P " +
                    "INNER JOIN PresupuestosDetalle PD ON P.idPresupuesto = PD.idPresupuesto " +
                    "INNER JOIN Productos PR ON PD.idProducto = PR.idProducto " +
                    "WHERE P.idPresupuesto = @idPresupuesto";
        using (SQLiteConnection conexion = new SQLiteConnection(_cadenaDeConexion))
        {
            conexion.Open();
            var command = new SQLiteCommand(query, conexion);
            command.Parameters.Add(new SQLiteParameter("@idPresupuesto", idPresupuesto));
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (presupuesto == null)
                        {
                            presupuesto = new Presupuestos()
                            {
                                IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]),
                                NombreDestinatario = reader["NombreDestinatario"].ToString(),
                                FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]),
                                Detalles = new List<PresupuestoDetalle>()
                            };
                        }

                        PresupuestoDetalle detalle = new PresupuestoDetalle()
                        {
                            Producto = new Producto()
                            {
                                IdProducto = Convert.ToInt32(reader["idProducto"]),
                                Descripcion = reader["Producto"].ToString(),
                                Precio = Convert.ToInt32(reader["Precio"])
                            },
                            Cantidad = Convert.ToInt32(reader["Cantidad"])
                        };

                        presupuesto.Detalles.Add(detalle);
                    }
                }
            }
            conexion.Close();
        }
        return presupuesto;
    }
}