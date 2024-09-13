using CSCoffeeConsoleApp.Databases;
using CSCoffeeConsoleApp.Services;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSCoffeeConsoleApp.Dappers;

public class DapperUser
{
    public void Run()
    {
        Console.WriteLine("Hello!");
        //Create("soesoe", "user", false, "224455%");
        //Search(1);
        //Search(4);
        //Update(3, "moemoe", "admin", true, "15652&");
        //Delete(3);
        Read();

        Console.WriteLine("Thank you.");
    }
    private void Read()
    {
        using IDbConnection db = new SqlConnection(ConnectionStrings._connectionStringBuilder.ConnectionString);
        db.Open();
        List<CSUserDto> lst = db.Query<CSUserDto>("SELECT * FROM tbl_user").ToList();
        foreach (CSUserDto item in lst)
        {
            Console.WriteLine("ID: " + item.userId);
            Console.WriteLine("username: " + item.userName);
            Console.WriteLine("userrole: " + item.userRole);
            Console.WriteLine("password: " + item.password);
            Console.WriteLine("isAdmin: " + item.isAdmin);
            Console.WriteLine("----------------------------------------");
        }
    }
    private void Create(string username, string userrole, bool isAdmin, string password)
    {
        string query = @"INSERT INTO [dbo].[tbl_user]
           ([userName]
           ,[userRole]
           ,[isAdmin]
           ,[password])
     VALUES
           (@userName
           ,@userRole
           ,@isAdmin
           ,@password)";

        var item = new CSUserDto
        {
            userName = username,
            userRole = userrole,
            isAdmin = isAdmin,
            password = password
        };
        using IDbConnection db = new SqlConnection(ConnectionStrings._connectionStringBuilder.ConnectionString);
        int result = db.Execute(query, item);

        string msg = result > 0 ? "Creating Successful!" : "Creating Fail!";
        Console.WriteLine(msg);
    }
    private void Search(int id)
    {
        using IDbConnection db = new SqlConnection(ConnectionStrings._connectionStringBuilder.ConnectionString);
        var item = db.Query("SELECT * FROM tbl_user WHERE userId = @userId", new CSUserDto { userId = id }).FirstOrDefault();
        if (item != null)
        {
            Console.WriteLine("ID: " + item.userId);
            Console.WriteLine("username: " + item.userName);
            Console.WriteLine("userrole: " + item.userRole);
            Console.WriteLine("password: " + item.password);
            Console.WriteLine("isAdmin: " + item.isAdmin);
            Console.WriteLine("----------------------------------------");
        }
        else
        {
            Console.WriteLine("Not data found!");
            return;
        }
    }
    private void Update(int id, string username, string userrole, bool isAdmin, string password)
    {
        string query = @"UPDATE [dbo].[tbl_user]
   SET [userName] = @userName
      ,[userRole] = @userRole
      ,[isAdmin] = @isAdmin
      ,[password] = @password
 WHERE userId = @userId;";

        var item = new CSUserDto
        {
            userId = id,
            userName = username,
            userRole = userrole,
            password = password,
            isAdmin = isAdmin
        };

        using IDbConnection db = new SqlConnection(ConnectionStrings._connectionStringBuilder.ConnectionString);
        int result = db.Execute(query, item);

        string msg = result > 0 ? "Updating Successful." : "Updating Fail!";
        Console.WriteLine(msg);

    }
    private void Delete(int id)
    {
        string query = @"DELETE FROM [dbo].[tbl_user]
      WHERE userId = @userId;";
        var item = new CSUserDto
        {
            userId = id
        };
        using IDbConnection db = new SqlConnection(ConnectionStrings._connectionStringBuilder.ConnectionString);
        int result = db.Execute(query, item);

        string msg = result > 0 ? "Deleting Successful!" : "Deleting Fail!";
        Console.WriteLine(msg);
    }

}
