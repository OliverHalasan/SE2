using ClubBAIST.TechnicalServices;

namespace ClubBAIST.Model
{
    public class CLB
    {
        public Member Login(string username, string password)
        {

            Members MemberManager = new();
            Member Login;
            Login = MemberManager.MemberLogin(username, password);
            return Login;
        }


        public bool NewMemberApplication(string firstName, string lastName, string address, string postalCode, string phone, string alterPhone, string companyName, string addressLine1, string addressLine2, string email, DateTime dateOfBirth, int membershipTypeID, string passwordHash, string role, string occupation)
        {
            bool Confirmation;

            Members MemberManager = new Members();
            Confirmation = MemberManager.MemberApplication(firstName, lastName, address, postalCode, phone, alterPhone, companyName, addressLine1, addressLine2, email, dateOfBirth, membershipTypeID, passwordHash, role, occupation);
            return Confirmation;
        }

        public List<Member> ListApplication()
        {
            Members MembersManager = new();
            List<Member> WaitingList;
            WaitingList = MembersManager.ApplicationList();
            return WaitingList;
        }

        public bool newTeeTime(string memberNumber1, string name1, string memberNumber2, string name2, string memberNumber3, string name3, string memberNumber4, string name4, TimeOnly requestedTeeTime, DateOnly requestedDate)
        {
            bool Confirmation;
            TeeTimes TeeTimeManager = new TeeTimes();
            Confirmation = TeeTimeManager.BookTeeTime(memberNumber1, name1, memberNumber2, name2, memberNumber3, name3, memberNumber4, name4, requestedTeeTime, requestedDate);
            return Confirmation;
        }

        public List<TeeTimeLists> FindTeeTime(int memberID)
        {

            TeeTimes TeeTimeManager = new();
            List<TeeTimeLists> FindTeeTime;

            FindTeeTime = TeeTimeManager.GetsTeeTime(memberID);

            return FindTeeTime;
        }

        public bool ModifyTeeTime(int standingID, string memberNumber1, string name1, string memberNumber2, string name2, string memberNumber3, string name3, string memberNumber4, string name4, TimeOnly requestedTeeTime, DateOnly requestedDate)
        {
            bool Confirmation;
            TeeTimes TeeTimeManager = new TeeTimes();
            Confirmation = TeeTimeManager.UpdateTeeTime( standingID,memberNumber1, name1, memberNumber2, name2, memberNumber3, name3, memberNumber4, name4, requestedTeeTime, requestedDate);
            return Confirmation;
        }

        public TeeTimeLists GetStandingTeeTime(int standingID)
        {
            TeeTimes TeeTimeManger = new();
            TeeTimeLists ActiveTeeTime;
            ActiveTeeTime = TeeTimeManger.GetTeeTime(standingID);
            return ActiveTeeTime;
        }

    }
}
