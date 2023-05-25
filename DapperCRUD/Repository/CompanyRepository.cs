using Dapper;
using DapperCRUD.Context;
using DapperCRUD.Contracts;
using DapperCRUD.DTO;
using DapperCRUD.Entities;
using System.Data;
using System.Reflection.Metadata;

namespace DapperCRUD.Repository
{
    public class CompanyRepository:ICompanyRepository
    {
        private readonly DapperContext _context;
        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        //public async Task<Company> CreateCompany(CompanyForCreationDto company)
        //{
        //    var query = "Insert into Company(Name,Address,Country) VALUES(@Name,@Address,@Country)"+ "SELECT CAST(SCOPE_IDENTITY() as int)";
        //    var parameters = new DynamicParameters();
        //    parameters.Add("Name", company.Name, DbType.String);
        //    parameters.Add("Address", company.Address, DbType.String);
        //    parameters.Add("Country", company.Country, DbType.String);

        //    using(var connection = _context.CreateConnection())
        //    {
        //        var id =await connection.ExecuteAsync(query, parameters);
        //        var createdCompany = new Company
        //        {
        //            Id=id,
        //            Name= company.Name,
        //            Address=company.Address,
        //            Country=company.Country
        //        };
        //        return createdCompany;
        //    }
        //}

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            var query = "Select * from Company";
            using(var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<Company>(query);
                return companies.ToList();
            }
        }

        public async Task<Company> GetCompany(int id)
        {
            var query = "Select * from Company where id=@id";
            using(var connection = _context.CreateConnection())
            {
                var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new {id});
                return company;
            }
        }

        public async Task<Company> CreateCompany(CompanyForCreationDto company)
        {
            //string Name = company.Name;
            //string Address = company.Address;
            //string Country = company.Country;
            //var parameters = new DynamicParameters();

            //parameters.Add("Name", company.Name, DbType.String);
            //parameters.Add("Address", company.Address, DbType.String);
            //parameters.Add("Country", company.Country, DbType.String);
            var query = "Insert into Company(Name,Address,Country) VALUES(@Name,@Address,@Country)" + "SELECT CAST(SCOPE_IDENTITY() as int)";


            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, new { company.Name, company.Address, company.Country });
                var createdCompany = new Company
                {
                    Id = id,
                    Name = company.Name,
                    Address = company.Address,
                    Country = company.Country
                };
                return createdCompany;
            }
        }

        public async Task UpdateCompany(int id, CompanyForUpdateDto company)
        {
            var query = "Update Company set Name=@Name, Address=@Address, Country=@Country where Id=@Id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id,DbType.Int32);
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async Task DeleteCompany(int id)
        {
            var query = "Delete from Company where Id=@Id";
            using(var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, new { id });
            }
        }


    }
}
