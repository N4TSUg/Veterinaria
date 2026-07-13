namespace Veterinaria.ViewModels.Dashboard
{
    public class DashboardVM
    {
        public int TotalClientes { get; set; }
        public int TotalMascotas { get; set; }
        public int ConsultasMes { get; set; }
        public int ProximasCitas { get; set; }
        public int VacunasPorVencer { get; set; }
        public IEnumerable<Models.Consulta> ListaProximasCitas { get; set; } = new List<Models.Consulta>();
    }
}
