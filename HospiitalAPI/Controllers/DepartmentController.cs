using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using HospiitalAPI.Models;

namespace HospiitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult GetAllDepartments()
        {
            string query = @"
                            select * From 
                            dbo.Department
                            ";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult PostAllDepartments(Department dep)
        {
            string query = @"
                            insert into dbo.Department
                            values (@DepartmentName)
                            ";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myCon.Close();
                }
            }
            return new JsonResult("Added sucessfully");
        }
        [HttpPut]
        public JsonResult PutAllDepartments(Department dep)
        {
            string query = @"
                            update dbo.Department
                            set DepartmentName= @DepartmentName
                            where DepartmentId=@DepartmentId
                            ";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                    myCommand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myCon.Close();
                }
            }
            return new JsonResult("Updated Sucessfully");
        }
        [HttpDelete("{id}")]
        public JsonResult DeleteAllDepartments(int id)
        {
            string query = @"
                            delete from dbo.Department
                            where DepartmentId=@DepartmentId
                            ";
            DataTable table = new DataTable();
            string sqlDatasource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDatasource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@DepartmentId", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted Sucessfully");
        }
    }
}
