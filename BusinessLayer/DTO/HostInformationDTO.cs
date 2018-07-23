namespace BusinessLayer.DTO
{
    public class HostInformationDTO
    {
        public HostInformationDTO(string name, string server)
        {
            this.Name = name;
            this.Server = server;
        }
        public string Name { get; set; }

        public string Server { get; set; }
    }
}
