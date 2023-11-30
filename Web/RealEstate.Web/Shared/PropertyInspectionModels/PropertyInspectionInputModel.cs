namespace RealEstate.Web.Shared.PropertyInspectionModels
{
    // това е модел за цруд операция ориентирай се по имената input e за записване в базата а view за фронтенда
    public class PropertyInspectionInputModel
    {
        public string? UserName { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Inquiry { get; set; }

        public string? Date { get; set; }

        public string? Hour { get; set; }

        public string? PropertyId { get; set; }
    }
}
