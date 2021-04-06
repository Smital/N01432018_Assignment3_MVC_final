using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using N01432018_Assignment3_MVC.Models;
using MySql.Data.MySqlClient;
 
namespace N01432018_Assignment3_MVC.Controllers
{
    public class TeacherDataController : ApiController
    {
        //create an instance of class which we create in Model folder ScoolDbContext.cs
        //School is an object of class SchoolDbContext
        private SchoolDbContext School = new SchoolDbContext();


        //httpget is used to get the data from the database
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{Searchkey}/{Number}")]

        //IEnumerable method is used to get the list of Teachers from the teachers table
        public IEnumerable<Teacher> ListTeachers(string SearchKey,string Number)
        {
            //Accessdatabse is the method of ScoolDbContext class,so use the method of the class we need 
            // to create an object of connection method.
            //So here Conn is the object of a connection MySqlConnection to access the method AccessDatabase()

            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the webserver and the database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //Sql Query
            //cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower('%@key%') or lower(teacherlname) like lower('%@Key%') or lower(concat(teacherfname,' ',teacherlname)) like lower('%@key%') or salary like '%"+Number + "%'";
            cmd.CommandText = "Select * from teachers where lower(teacherfname) like lower('%"+SearchKey+ "%') or lower(teacherlname) like lower('%" + SearchKey + "%') or lower(concat(teacherfname,' ',teacherlname)) like lower('" + SearchKey + "') or salary like '%"+Number+"%' ";
            //which parameter is to insert in a particular query.So here we are inserting searchkey
            //cmd.Parameters.AddWithValue("key", "%" + SearchKey + "%");
            //cmd.Prepare();

            //Gather the result set of query into a variable
            MySqlDataReader Resultset = cmd.ExecuteReader();

            //Create ab Empty list of teacher names
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop for each row would be accessed by the resultset
            while(Resultset.Read())
            //Read method will read series of rows
            {
                //Access Colum infromation be the Db column name as an index
                int TeacherId = (int)Resultset["teacherid"];
                string TeacherFname = (string)Resultset["teacherfname"];
                string TeacherLName = (string)Resultset["teacherlname"];
                string EmployeeNumber = (string)Resultset["employeenumber"];
                DateTime  HireDate = (DateTime)Resultset["hiredate"];
                decimal Salary = (decimal)Resultset["salary"];

                //To make an object of an teacher class
                Teacher newTeacher = new Teacher();
                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname = TeacherLName;
                newTeacher.EmployeeNumber = EmployeeNumber;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;

                //Add the TechserName to the list
                Teachers.Add(newTeacher);
            }

            //For closing the connection between Mysql dataabse and web server

            Conn.Close();

            //Return the final list of Techer Names
            return Teachers;

        }

        [HttpGet]
        public Teacher findTeacher(int id)
        {


            Teacher newTeacher = new Teacher();

            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the webserver and the database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //Sql Query
            //cmd.CommandText = "Select t.teacherfname,t.teacherlname ,c.classname from teachers t JOIN classes c ON t.teacherid = c.teacherid where .teacherid = " + id;
            cmd.CommandText = "Select * from teachers where teacherid = " + id;

            //Gather the result set of query into a variable
            MySqlDataReader Resultset = cmd.ExecuteReader();

            while (Resultset.Read())
            //Read method will read series of rows
            {
                int TeacherId = (int)Resultset["teacherid"];
                string TeacherFname = (string)Resultset["teacherfname"];
                string TeacherLName = (string)Resultset["teacherlname"];
                string EmployeeNumber = (string)Resultset["employeenumber"];
                DateTime HireDate = (DateTime)Resultset["hiredate"];
                decimal Salary = (decimal)Resultset["salary"];


                newTeacher.TeacherId = TeacherId;
                newTeacher.TeacherFname = TeacherFname;
                newTeacher.TeacherLname = TeacherLName;
                newTeacher.EmployeeNumber = EmployeeNumber;
                newTeacher.HireDate = HireDate;
                newTeacher.Salary = Salary;

            }

                return newTeacher;
        }
    }
}
