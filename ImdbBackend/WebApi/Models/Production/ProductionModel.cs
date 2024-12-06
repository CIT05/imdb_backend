namespace WebApi.Models.Production
{
    public class ProductionModel
    {
        public string Url { get; set; }
        public int RoleId { get; set; }

        public string PrimaryName { get; set; } = string.Empty;
    }
}
