using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Windows.Forms;

public static class DatabaseHelper
{
    // HÀM LẤY CHUỖI KẾT NỐI (TỪ appsettings.json)
    private static string GetConnectionString()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        IConfigurationRoot configuration = builder.Build();
        return configuration.GetConnectionString("MyConnectionString");
    }

    // HÀM KIỂM TRA ĐĂNG NHẬP (CHO Form1)
    // Sẽ trả về "User", "Admin", hoặc "Invalid"
    public static string CheckLogin(string username, string password)
    {
        string role = "Invalid"; // Mặc định là sai

        string query = "SELECT Role FROM TaiKhoan WHERE Username = @User AND Password = @Pass";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@User", username);
                cmd.Parameters.AddWithValue("@Pass", password); 

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar(); // Lấy 1 ô duy nhất (Role)

                    if (result != null)
                    {
                        role = result.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi CSDL khi đăng nhập: " + ex.Message);
                }
            }
        }
        return role;
    }

    // HÀM ĐĂNG KÝ TÀI KHOẢN (CHO Form_sign_up)
    // Sẽ trả về true (thành công) hoặc false (thất bại)
    public static string RegisterUser(string username, string password, string role)
    {
        string queryCheck = "SELECT COUNT(*) FROM TaiKhoan WHERE Username = @User";
        string queryRegister = "INSERT INTO TaiKhoan (Username, Password, Role) VALUES (@User, @Pass, @Role)";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            try
            {
                conn.Open();

                using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn))
                {
                    cmdCheck.Parameters.AddWithValue("@User", username);
                    int userCount = (int)cmdCheck.ExecuteScalar();

                    if (userCount > 0)
                        return "EXISTED";
                }

                using (SqlCommand cmdRegister = new SqlCommand(queryRegister, conn))
                {
                    cmdRegister.Parameters.AddWithValue("@User", username);
                    cmdRegister.Parameters.AddWithValue("@Pass", password);
                    cmdRegister.Parameters.AddWithValue("@Role", role);

                    int rows = cmdRegister.ExecuteNonQuery();
                    return rows > 0 ? "SUCCESS" : "FAILED";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi CSDL khi đăng ký: " + ex.Message);
                return "ERROR";
            }
        }
    }
}