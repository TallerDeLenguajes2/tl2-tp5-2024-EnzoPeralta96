public interface IPresupuestoRepository
{
    public List<Presupuestos> ListarPresupuestos();
    public Presupuestos ObtenerPresupuesto(int idPresupuesto);
}