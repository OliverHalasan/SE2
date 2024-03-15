namespace ClubBAIST.Model
{
    public class Member
    {
        public int MemberID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address {  get; set; } = string.Empty;
        public string PostalCode {  get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string AlterPhone { get; set; } = string.Empty;
        public string Occupation { get; set; }  = string.Empty;
        public string CompanyName { get;  set; } = string.Empty;
        public string AddressLine1 { get; set; } = string.Empty;
        public string AddressLine2 { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public int MembershipTypeID { get; set; }
        public string Role {  get; set; } = string.Empty;
        public string PasswordHash {  get; set; } = string.Empty;
        public string Approved {  get; set; } = string.Empty;

    }
}
