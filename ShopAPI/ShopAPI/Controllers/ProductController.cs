using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using ShopAPI.Models;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;


namespace ShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public ProductController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }



        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                      select ProductId, 
                       CategoryProducts, 
                       ProductName, 
                       Price
                      ,Photo
                      ,Quantity
                      from dbo.Products";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myCon))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Product pro)
        {
            string query = @"
                      insert into dbo.Products
                        (CategoryProducts,ProductName,Price,Photo,Quantity)
                        values
                        ( 
                         '" + pro.CategoryProducts + @"'
                         ,'" + pro.ProductName + @"'
                         ,'" + pro.Price + @"'
                         ,'" + pro.Photo + @"'
                         ,'" + pro.Quantity + @"'
                           )
                          ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myCon))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added succesfully");
        }

        [HttpPut]
        public JsonResult Put(Product cat)
        {
            string query = @"
                   update dbo.Products set
                   CategoryProducts = '" + cat.CategoryProducts + @"'
                   ,ProductName = '" + cat.ProductName + @"'
                   ,Price = '" + cat.Price + @"'
                   ,Photo = ' " + cat.Photo + @"'
                   ,Quantity = '" + cat.Quantity + @"' 
                   where ProductId = " + cat.ProductId + @" 
                   ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myCon))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Update succesfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)

        {
            string query = @"
                   delete from dbo.Products
                   where ProductId = " + id + @"
                   ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myCon))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Delete succesfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("rtx.png");
            }
        }

        [Route("GetAllCategoryProducts")]
        public JsonResult GetAllCategoryProducts()
        {
            string query = @"
                      select CategoryName from dbo.Products
                     ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ShopAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myCon))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

    }

 }





