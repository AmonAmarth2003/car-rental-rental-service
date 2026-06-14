using Template.Data;

namespace Template.Infra
{
    public static class GeradorDeServicos
    {
        public static IServiceProvider ServiceProvider;

        public static DataContext CarregarContexto()
        {
            return ServiceProvider.GetService<DataContext>();
        }
    }
}
