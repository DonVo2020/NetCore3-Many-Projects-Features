namespace DonVo.ViewModels.DTOs
{
    public class MainDirectoryDTO
    {
        public string HashId { get; set; }
        public string Name { get; set; }
        public string RowVersion { get; set; }
    }

    public class RolesDTO : MainDirectoryDTO
    {

    }
}
