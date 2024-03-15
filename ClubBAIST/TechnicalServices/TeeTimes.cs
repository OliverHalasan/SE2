using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using ClubBAIST.Model;

namespace ClubBAIST.TechnicalServices
{
    public class TeeTimes
    {
        private string? _connectionString; //nullable reference type

        public TeeTimes()
        {
            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();
            _connectionString = DatabaseUsersConfiguration.GetConnectionString("ClubBAIST");

        }

        public bool BookTeeTime(string memberNumber1, string name1, string memberNumber2, string name2, string memberNumber3, string name3, string memberNumber4, string name4, TimeOnly requestedTeeTime, DateOnly requestedDate)
        {
            bool Succeded = false;

            SqlConnection jhalasan1 = new();
            jhalasan1.ConnectionString = _connectionString;
            jhalasan1.Open();

            SqlCommand BookTeeTimeCommand = new()
            {
                Connection = jhalasan1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "TeeTimeRequest"
            };
            SqlParameter BookTeeTimeParameter;

            BookTeeTimeParameter = new()
            {
                ParameterName = "@MemberNumber1",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = memberNumber1
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);


            BookTeeTimeParameter = new()
            {
                ParameterName = "@Name1",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = name1
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);

            BookTeeTimeParameter = new()
            {
                ParameterName = "@MemberNumber2",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(memberNumber2) ? DBNull.Value : (object)memberNumber2
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);


            BookTeeTimeParameter = new()
            {
                ParameterName = "@Name2",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(name2) ? DBNull.Value : (object)name2
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);

            BookTeeTimeParameter = new()
            {
                ParameterName = "@MemberNumber3",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(memberNumber3) ? DBNull.Value : (object)memberNumber3
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);


            BookTeeTimeParameter = new()
            {
                ParameterName = "@Name3",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(name3) ? DBNull.Value : (object)name3
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);

            BookTeeTimeParameter = new()
            {
                ParameterName = "@MemberNumber4",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(memberNumber4) ? DBNull.Value : (object)memberNumber4
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);


            BookTeeTimeParameter = new()
            {
                ParameterName = "@Name4",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(name4) ? DBNull.Value : (object)name4
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);

            BookTeeTimeParameter = new()
            {
                ParameterName = "@RequestedTeeTime",
                SqlDbType = SqlDbType.Time,
                Direction = ParameterDirection.Input,
                Value = requestedTeeTime
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);

            BookTeeTimeParameter = new()
            {
                ParameterName = "@RequestedDate",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input,
                Value = requestedDate
            };
            BookTeeTimeCommand.Parameters.Add (BookTeeTimeParameter);

            BookTeeTimeCommand.ExecuteNonQuery();
            jhalasan1.Close();
            Succeded = true;
            return Succeded;


        }
        public List<TeeTimeLists> GetsTeeTime(int memberID)
        {

            List<TeeTimeLists> standingTeeTimes = new List<TeeTimeLists>();

            SqlConnection jhalasan1 = new();
            jhalasan1.ConnectionString = _connectionString;
            jhalasan1.Open();

            SqlCommand TeeTimeListCommand = new()
            {
                Connection = jhalasan1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "TeeTimeList"
            };
            SqlParameter TeeTimeListParameter = new()
            {
                ParameterName = "@MemberID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                SqlValue = memberID
            };
            TeeTimeListCommand.Parameters.Add(TeeTimeListParameter);

            using (SqlDataReader reader = TeeTimeListCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    TeeTimeLists standingTeeTime = new TeeTimeLists
                    {
                        RequestedDate = (DateTime)reader["RequestedDate"],
                        RequestedTeeTime = (TimeSpan)reader["RequestedTeeTime"],
                        StandingID = (int)reader["StandingID"]

                    };
                    standingTeeTimes.Add(standingTeeTime);
                }
            }
            jhalasan1.Close();

            return standingTeeTimes;

        }

        public bool UpdateTeeTime(int standingID, string memberNumber1, string name1, string memberNumber2, string name2, string memberNumber3, string name3, string memberNumber4, string name4, TimeOnly requestedTeeTime, DateOnly requestedDate)
        {
            bool Succeded = false;

            SqlConnection jhalasan1 = new();
            jhalasan1.ConnectionString = _connectionString;
            jhalasan1.Open();

            SqlCommand BookTeeTimeCommand = new()
            {
                Connection = jhalasan1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "UpdateTeeTime"
            };
            SqlParameter BookTeeTimeParameter;

            BookTeeTimeParameter = new()
            {
                ParameterName = "@StandingID",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = standingID
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);

            BookTeeTimeParameter = new()
            {
                ParameterName = "@MemberNumber1",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = memberNumber1
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);


            BookTeeTimeParameter = new()
            {
                ParameterName = "@Name1",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = name1
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);

            BookTeeTimeParameter = new()
            {
                ParameterName = "@MemberNumber2",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(memberNumber2) ? DBNull.Value : (object)memberNumber2
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);


            BookTeeTimeParameter = new()
            {
                ParameterName = "@Name2",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(name2) ? DBNull.Value : (object)name2
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);

            BookTeeTimeParameter = new()
            {
                ParameterName = "@MemberNumber3",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(memberNumber3) ? DBNull.Value : (object)memberNumber3
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);


            BookTeeTimeParameter = new()
            {
                ParameterName = "@Name3",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(name3) ? DBNull.Value : (object)name3
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);

            BookTeeTimeParameter = new()
            {
                ParameterName = "@MemberNumber4",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(memberNumber4) ? DBNull.Value : (object)memberNumber4
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);


            BookTeeTimeParameter = new()
            {
                ParameterName = "@Name4",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(name4) ? DBNull.Value : (object)name4
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);

            BookTeeTimeParameter = new()
            {
                ParameterName = "@RequestedTeeTime",
                SqlDbType = SqlDbType.Time,
                Direction = ParameterDirection.Input,
                Value = requestedTeeTime
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);

            BookTeeTimeParameter = new()
            {
                ParameterName = "@RequestedDate",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input,
                Value = requestedDate
            };
            BookTeeTimeCommand.Parameters.Add(BookTeeTimeParameter);

            BookTeeTimeCommand.ExecuteNonQuery();
            jhalasan1.Close();
            Succeded = true;
            return Succeded;


        }

        public TeeTimeLists GetTeeTime(int standingID)
        {
            SqlConnection jhalasan1 = new();
            jhalasan1.ConnectionString = _connectionString;
            jhalasan1.Open();

            SqlCommand GetTeeTimeCommand = new()
            {
                Connection = jhalasan1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "GetTeeTimeDetails"
            };
            SqlParameter GetTeeTimeParameter = new()
            {
                ParameterName = "@StandingID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = standingID
            };
            GetTeeTimeCommand.Parameters.Add(GetTeeTimeParameter);
            SqlDataReader reader = GetTeeTimeCommand.ExecuteReader();
            TeeTimeLists BookedTeeTime = new();

            if (reader.HasRows)
            {
                reader.Read();
                BookedTeeTime.StandingID = standingID;
                BookedTeeTime.MemberNumber1 = (string)reader["MemberNumber1"];
                BookedTeeTime.name1 = (string)reader["Name1"];
                BookedTeeTime.MemberNumber2 = reader["MemberNumber2"] == DBNull.Value ? null : (string)reader["MemberNumber2"];
                BookedTeeTime.name2 = reader["Name2"] == DBNull.Value ? null : (string)reader["Name2"];
                BookedTeeTime.MemberNumber3 = reader["MemberNumber3"] == DBNull.Value ? null : (string)reader["MemberNumber3"];
                BookedTeeTime.name3 = reader["Name3"] == DBNull.Value ? null : (string)reader["Name3"];
                BookedTeeTime.MemberNumber4 = reader["MemberNumber4"] == DBNull.Value ? null : (string)reader["MemberNumber4"];
                BookedTeeTime.name4 = reader["Name4"] == DBNull.Value ? null : (string)reader["Name4"];
                BookedTeeTime.RequestedDate = (DateTime)reader["RequestedDate"];
                BookedTeeTime.RequestedTeeTime = (TimeSpan)reader["RequestedTeeTime"];
            }
            jhalasan1.Close();
            return BookedTeeTime;
        }
    }
}
