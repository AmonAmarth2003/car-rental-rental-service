namespace Retail.API.Servicos
{
    public interface IClientServiceCaller
    {
        public Task<bool> ClientExists(int id);
    }
}