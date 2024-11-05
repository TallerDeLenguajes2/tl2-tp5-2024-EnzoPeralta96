public class Presupuestos
{
    public int IdPresupuesto { get; set; }
    public string NombreDestinatario { get; set; }
    public DateTime FechaCreacion { get; set; }
    public List<PresupuestoDetalle> Detalles { get; set; }

    public int MontoPresupuesto()
    {
        int montoTotal = 0;
        foreach (var detalle in Detalles)
        {
            montoTotal+= detalle.Cantidad*detalle.Producto.Precio;
        }
        return montoTotal;
    }
    

    public double MontoPresupuestoConIva()
    {
        return MontoPresupuesto()*1.21;
    }
    
    public int CantidadProductos()
    {
        int cantidadProductos = 0;
        foreach (var detalle in Detalles)
        {
            cantidadProductos += detalle.Cantidad;
        }
        return cantidadProductos;
    }

}

