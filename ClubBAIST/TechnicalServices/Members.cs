using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using ClubBAIST.Model;

namespace ClubBAIST.TechnicalServices
{
    internal class Members
    {
        private string? _connectionString; //nullable reference type

        public Members()
        {
            ConfigurationBuilder DatabaseUsersBuilder = new();
            DatabaseUsersBuilder.SetBasePath(Directory.GetCurrentDirectory());
            DatabaseUsersBuilder.AddJsonFile("appsettings.json");
            IConfiguration DatabaseUsersConfiguration = DatabaseUsersBuilder.Build();
            _connectionString = DatabaseUsersConfiguration.GetConnectionString("ClubBAIST");
        }

        public Member MemberLogin(string email, string passwordHash)
        {
            Member existingMember = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("MemberLogin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            existingMember = new Member
                            {
                                Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : null,
                                PasswordHash = reader["PasswordHash"] != DBNull.Value ? (string)reader["PasswordHash"] : null,
                                FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : null,
                                LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : null,
                                Role = reader["Role"] != DBNull.Value ? (string)reader["Role"] : null,
                                MemberID = reader["MemberID"] != DBNull.Value ? (int)reader["MemberID"] : 0

                            };
                        }
                    }
                }
            }

            return existingMember;
        }

        public bool MemberApplication(string firstName, string lastName, string address, string postalCode, string phone, string alterPhone, string companyName, string addressLine1, string addressLine2, string email, DateTime dateOfBirth, int membershipTypeID, string passwordHash, string role, string occupation, string approved = "W")
        {
            bool Succeeded = false;


            SqlConnection jhalasan1 = new();
            jhalasan1.ConnectionString = _connectionString;
            jhalasan1.Open();

            SqlCommand MemberApplicationCommand = new()
            {
                Connection = jhalasan1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddClubMember"
            };
            SqlParameter MemberApplicationParameter;

            MemberApplicationParameter = new()
            {
                ParameterName = "@FirstName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = firstName
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@LastName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = lastName
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@Address",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = address
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@PostalCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = postalCode
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@Phone",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = phone
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@AlterPhone",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(alterPhone) ? DBNull.Value : (object)alterPhone
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@Occupation",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = occupation
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@CompanyName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = companyName
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@AddressLine1",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = addressLine1
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@AddressLine2",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = string.IsNullOrEmpty(addressLine2) ? DBNull.Value : (object)addressLine2
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = email
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@DateOfBirth",
                SqlDbType = SqlDbType.Date,
                Direction = ParameterDirection.Input,
                Value = dateOfBirth
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@MembershipTypeID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = membershipTypeID
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@PasswordHash",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = passwordHash
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@Role",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = role
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationParameter = new()
            {
                ParameterName = "@Approved",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = approved
            };
            MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            //byte[] signatureBytes = Convert.FromBase64String(signatureData);

            //MemberApplicationParameter = new()
            //{
            //    ParameterName = "@SignatureData",
            //    SqlDbType = SqlDbType.VarBinary,
            //    Direction = ParameterDirection.Input,
            //    Value = signatureBytes
            //};
            //MemberApplicationCommand.Parameters.Add(MemberApplicationParameter);

            MemberApplicationCommand.ExecuteNonQuery();
            jhalasan1.Close();
            Succeeded = true;
            return Succeeded;
        }


        public List<Member> ApplicationList()
        {
            List<Member> list = new List<Member>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("MembershipApplicationList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Member member = new Member
                            {
                                MemberID = reader["MemberID"] != DBNull.Value ? (int)reader["MemberID"] : 0,
                                FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : null,
                                LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : null,
                                Address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : null,
                                PostalCode = reader["PostalCode"] != DBNull.Value ? (string)reader["PostalCode"] : null,
                                Phone = reader["Phone"] != DBNull.Value ? (string)reader["Phone"] : null,
                                AlterPhone = reader["AlterPhone"] != DBNull.Value ? (string)reader["AlterPhone"] : null,
                                Occupation = reader["Occupation"] != DBNull.Value ? (string)reader["Occupation"] : null,
                                CompanyName = reader["CompanyName"] != DBNull.Value ? (string)reader["CompanyName"] : null,
                                AddressLine1 = reader["AddressLine1"] != DBNull.Value ? (string)reader["AddressLine1"] : null,
                                AddressLine2 = reader["AddressLine2"] != DBNull.Value ? (string)reader["AddressLine2"] : null,
                                Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : null,
                                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)reader["DateOfBirth"]) : new DateOnly(),
                                MembershipTypeID = reader["MembershipTypeID"] != DBNull.Value ? (int)reader["MembershipTypeID"] : 0,
                                Role = reader["Role"] != DBNull.Value ? (string)reader["Role"] : null,
                                Approved = reader["Approved"] != DBNull.Value ? (string)reader["Approved"] : null
                            };


                            list.Add(member);
                        }
                    }
                }
            }

            return list;
        }

    
        public Member GetMemberApplication (int memberID)
        {
            SqlConnection jhalasan1 = new();
            jhalasan1.ConnectionString = _connectionString;
            jhalasan1.Open();

            SqlCommand GetApplicationMemberCommand = new()
            {
                Connection = jhalasan1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "ViewMemberApplication"
            };

            SqlParameter GetApplicationMemberParameter = new()
            {
                ParameterName = "MemberID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = memberID
            };
            GetApplicationMemberCommand.Parameters.Add(GetApplicationMemberParameter);
            SqlDataReader reader = GetApplicationMemberCommand.ExecuteReader();
            Member ExisitingApplicationMember = new();

            if (reader.HasRows)
            {
                reader.Read();
                ExisitingApplicationMember.FirstName = (string)reader["FirstName"];
                ExisitingApplicationMember.LastName = (string)reader["LastName"];
                ExisitingApplicationMember.Address = (string)reader["Address"];
                ExisitingApplicationMember.PostalCode = (string)reader["PostalCode"];
                ExisitingApplicationMember.Phone = (string)reader["Phone"];
                ExisitingApplicationMember.AlterPhone = reader["AlterPhone"] != DBNull.Value ? (string)reader["AlterPhone"] : null;
                ExisitingApplicationMember.Occupation = (string)reader["Occupation"];
                ExisitingApplicationMember.CompanyName = (string)reader["CompanyName"];
                ExisitingApplicationMember.AddressLine1 = (string)reader["AddressLine1"];
                ExisitingApplicationMember.AddressLine2 = reader["AddressLine2"] != DBNull.Value ? (string)reader["AddressLine2"] : null;
                ExisitingApplicationMember.Email = (string)reader["Email"];
                ExisitingApplicationMember.DateOfBirth = DateOnly.FromDateTime((DateTime)reader["DateOfBirth"]);
                ExisitingApplicationMember.Approved = (string)reader["Approved"];
            }

            jhalasan1.Close();
            return ExisitingApplicationMember;
        }

        public bool ApproveMemberApplication(int memberID, string approved)
        {
            bool Succeded = false;


            SqlConnection jhalasan1 = new();
            jhalasan1.ConnectionString = _connectionString;
            jhalasan1.Open();

            SqlCommand ApproveMemberCommand = new()
            {
                Connection = jhalasan1,
                CommandType = CommandType.StoredProcedure,
                CommandText = "ApproveMemberApplication"
            };
            SqlParameter ApproveMemberParameter;

            ApproveMemberParameter = new()
            {
                ParameterName = "@MemberID",
                SqlDbType = SqlDbType.Int,
                Direction = ParameterDirection.Input,
                Value = memberID
            };
            ApproveMemberCommand.Parameters.Add(ApproveMemberParameter);

            ApproveMemberParameter = new()
            {
                ParameterName = "@Approved",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                Value = approved
            };
            ApproveMemberCommand.Parameters.Add(ApproveMemberParameter);

            ApproveMemberCommand.ExecuteNonQuery();
            jhalasan1.Close();
            Succeded = true;
            return Succeded;

        }

        public List<Member> ApprovedMemberApplicationList()
        {
            List<Member> list = new List<Member>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("ApprovedMembershipApplication", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Member member = new Member
                            {
                                MemberID = reader["MemberID"] != DBNull.Value ? (int)reader["MemberID"] : 0,
                                FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : null,
                                LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : null,
                                Address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : null,
                                PostalCode = reader["PostalCode"] != DBNull.Value ? (string)reader["PostalCode"] : null,
                                Phone = reader["Phone"] != DBNull.Value ? (string)reader["Phone"] : null,
                                AlterPhone = reader["AlterPhone"] != DBNull.Value ? (string)reader["AlterPhone"] : null,
                                Occupation = reader["Occupation"] != DBNull.Value ? (string)reader["Occupation"] : null,
                                CompanyName = reader["CompanyName"] != DBNull.Value ? (string)reader["CompanyName"] : null,
                                AddressLine1 = reader["AddressLine1"] != DBNull.Value ? (string)reader["AddressLine1"] : null,
                                AddressLine2 = reader["AddressLine2"] != DBNull.Value ? (string)reader["AddressLine2"] : null,
                                Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : null,
                                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)reader["DateOfBirth"]) : new DateOnly(),
                                MembershipTypeID = reader["MembershipTypeID"] != DBNull.Value ? (int)reader["MembershipTypeID"] : 0,
                                Role = reader["Role"] != DBNull.Value ? (string)reader["Role"] : null,
                                Approved = reader["Approved"] != DBNull.Value ? (string)reader["Approved"] : null
                            };


                            list.Add(member);
                        }
                    }
                }
            }

            return list;
        }
        public List<Member> RejectMemberApplicationList()
        {
            List<Member> list = new List<Member>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("RejectMembershipApplication", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Member member = new Member
                            {
                                MemberID = reader["MemberID"] != DBNull.Value ? (int)reader["MemberID"] : 0,
                                FirstName = reader["FirstName"] != DBNull.Value ? (string)reader["FirstName"] : null,
                                LastName = reader["LastName"] != DBNull.Value ? (string)reader["LastName"] : null,
                                Address = reader["Address"] != DBNull.Value ? (string)reader["Address"] : null,
                                PostalCode = reader["PostalCode"] != DBNull.Value ? (string)reader["PostalCode"] : null,
                                Phone = reader["Phone"] != DBNull.Value ? (string)reader["Phone"] : null,
                                AlterPhone = reader["AlterPhone"] != DBNull.Value ? (string)reader["AlterPhone"] : null,
                                Occupation = reader["Occupation"] != DBNull.Value ? (string)reader["Occupation"] : null,
                                CompanyName = reader["CompanyName"] != DBNull.Value ? (string)reader["CompanyName"] : null,
                                AddressLine1 = reader["AddressLine1"] != DBNull.Value ? (string)reader["AddressLine1"] : null,
                                AddressLine2 = reader["AddressLine2"] != DBNull.Value ? (string)reader["AddressLine2"] : null,
                                Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : null,
                                DateOfBirth = reader["DateOfBirth"] != DBNull.Value ? DateOnly.FromDateTime((DateTime)reader["DateOfBirth"]) : new DateOnly(),
                                MembershipTypeID = reader["MembershipTypeID"] != DBNull.Value ? (int)reader["MembershipTypeID"] : 0,
                                Role = reader["Role"] != DBNull.Value ? (string)reader["Role"] : null,
                                Approved = reader["Approved"] != DBNull.Value ? (string)reader["Approved"] : null
                            };


                            list.Add(member);
                        }
                    }
                }
            }

            return list;
        }


    }
}
