namespace Api.Configurations.AppSettings
{
    public class ApisConfig
    {
        public string ReconocimientoBaseUrl { get; set; }
        public string MultasBaseUrl { get; set; }
        public string PagosBaseUrl { get; set; }
    }
    public class FullApisConfig
    {
        public ApisConfig _apisConfig { get; set; }
        public FullApisConfig(ApisConfig apisConfig)
        {
            _apisConfig = apisConfig;
        }
        public string PatentesEndpoint { get { return _apisConfig.ReconocimientoBaseUrl + "api/patentes/"; } }
        public string MultasEndpoint { get { return _apisConfig.MultasBaseUrl + "api/multas/"; } }
        public string PagosEndpoint { get { return _apisConfig.PagosBaseUrl + "api/pagos/"; } }
    }
}
