namespace Api.CrossCutting.Helpers
{
    public static class ServiceCollectionHelper
    {
        public static IConfigurationSection GetSection(this string[] Elements, IConfiguration configuration)
        {
            IConfigurationSection section = null;
            //recorremos los elementos...
            Elements.ToList().ForEach(element =>
            {
                //Si es null obtenemos la primera sección
                section = section == null ? configuration.GetSection(element) : section.GetSection(element);
            });
            return section;
        }
    }
}
