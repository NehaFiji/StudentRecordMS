namespace StudentRecordMS.Models
{
    public class TblRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? IsActive { get; set; } = true;
    }
}
