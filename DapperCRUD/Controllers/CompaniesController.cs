using DapperCRUD.Contracts;
using DapperCRUD.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepo)
        {
            _companyRepository = companyRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetCompanies()
        {
            try {
                var company = await _companyRepository.GetCompanies();
                return Ok(company);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet,Route("~/GetCompanies1")]
        public async Task<ActionResult> GetCompanies1()
        {
            try
            {
                var company = await _companyRepository.GetCompanies();
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{id}", Name = "CompanyById")]
        public async Task<ActionResult> GetCompany(int id)
        {
            try
            {
                var company = await _companyRepository.GetCompany(id);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost,Route("CreateCompany")]
        public async Task<ActionResult> CreateCompany(CompanyForCreationDto company)
        {
            try 
            {
                
                var createdCompany = await _companyRepository.CreateCompany(company);
                return Ok(200);
                //return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost,Route("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyForUpdateDto company)
        {
            try
            {
                var dbCompany = await _companyRepository.GetCompany(id);
                if (dbCompany == null)
                    return NotFound();
                await _companyRepository.UpdateCompany(id, company);
                return NoContent();

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost, Route("DeleteCompany")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                var dbCompany = await _companyRepository.GetCompany(id);
                if (dbCompany == null)
                    return NotFound();

                await _companyRepository.DeleteCompany(id);
                return NoContent();

            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
