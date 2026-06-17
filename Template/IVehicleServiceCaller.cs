namespace Retail.API.Servicos
{
    public interface IVehicleServiceCaller
    {
        public Task<bool> VehicleExists(int id);
    }
}
