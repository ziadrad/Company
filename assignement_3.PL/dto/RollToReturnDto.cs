namespace assignement_3.PL.dto
{
    public class RollToReturnDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }

        public bool CreatePermission { get; set; }
        public bool EditPermission { get; set; }
        public bool DeletePermission { get; set; }
        public bool ShowRolePage { get; set; }
        public bool ShowUserPage { get; set; }
    }
}
