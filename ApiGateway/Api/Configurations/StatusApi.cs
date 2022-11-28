namespace Api.Configurations
{
    public sealed class StatusApi
    {
        private readonly static StatusApi _instance = new StatusApi();
        public bool IsActive = true;
        
        public static StatusApi Current
        {
            get
            {
                return _instance;
            }
        }

        private StatusApi()
        {
            
        }
    }
}
