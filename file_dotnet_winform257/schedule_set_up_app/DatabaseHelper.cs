using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.IO;
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

    // HÀM LẤY SỐ LIỆU CHO BIỂU ĐỒ
    public static Dictionary<DateTime, int> GetAppointmentCountsForCurrentWeek()
    {
        var counts = new Dictionary<DateTime, int>();

        // tạo logic chỉ tính ngày trong tuần hiện tại
        DateTime homNay = DateTime.Today;
        int daysToSubtract = 0;
        if (homNay.DayOfWeek == DayOfWeek.Sunday) { daysToSubtract = 6; }
        else { daysToSubtract = (int)homNay.DayOfWeek - (int)DayOfWeek.Monday; }
        DateTime ngayDauTuan = homNay.AddDays(-daysToSubtract); // (Thứ 2)
        DateTime ngaySauChuNhat = ngayDauTuan.AddDays(7); // (Thứ 2 tuần sau)

        //thay đổi bảng truy vấn bất cứ khi nào ta muốn đổi "LichHen" thành tên bảng đã tạo trong CSDL
        string query = @"
        SELECT 
            CAST(ThoiGianBatDau AS DATE) AS Ngay, 
            COUNT(*) AS SoLuong
        FROM 
            LichHen
        WHERE 
            ThoiGianBatDau >= @NgayDauTuan AND ThoiGianBatDau < @NgaySauChuNhat
        GROUP BY 
            CAST(ThoiGianBatDau AS DATE);
        ";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NgayDauTuan", ngayDauTuan);
                cmd.Parameters.AddWithValue("@NgaySauChuNhat", ngaySauChuNhat);
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            counts.Add((DateTime)reader["Ngay"], (int)reader["SoLuong"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy dữ liệu biểu đồ tuần: " + ex.Message);
                }
            }
        }
        return counts;
    }
    public static Dictionary<string, int> GetAppointmentCountsByTimeOfDay()
    {
        var counts = new Dictionary<string, int>();
        //thử nghiệm tạo biểu đồ tròn hiển thị có bao nhiêu phần trăm khách đặt vào các buổi sáng,chiều,tối; trong tuần hiện tại
        //  tạo logic chỉ lấy dữ liệu của tuần hiện tại
        DateTime homNay = DateTime.Today;
        int daysToSubtract = 0;
        if (homNay.DayOfWeek == DayOfWeek.Sunday) { daysToSubtract = 6; }
        else { daysToSubtract = (int)homNay.DayOfWeek - (int)DayOfWeek.Monday; }
        DateTime ngayDauTuan = homNay.AddDays(-daysToSubtract);
        DateTime ngaySauChuNhat = ngayDauTuan.AddDays(7);

        // Câu lệnh SQL này dùng CASE để phân loại giờ (HOUR)
        string query = @"
        SELECT 
            -- 1. Phân loại
            CASE
                WHEN DATEPART(HOUR, ThoiGianBatDau) BETWEEN 0 AND 11 THEN 'BuoiSang'
                WHEN DATEPART(HOUR, ThoiGianBatDau) BETWEEN 12 AND 17 THEN 'BuoiChieu'
                ELSE 'BuoiToi'
            END AS Buoi,
            
            -- 2. Đếm
            COUNT(*) AS SoLuong
        FROM
            LichHen
        WHERE
            ThoiGianBatDau >= @NgayDauTuan AND ThoiGianBatDau < @NgaySauChuNhat
        GROUP BY
            -- 3. Nhóm theo phân loại
            CASE
                WHEN DATEPART(HOUR, ThoiGianBatDau) BETWEEN 0 AND 11 THEN 'BuoiSang'
                WHEN DATEPART(HOUR, ThoiGianBatDau) BETWEEN 12 AND 17 THEN 'BuoiChieu'
                ELSE 'BuoiToi'
            END;
        ";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NgayDauTuan", ngayDauTuan);
                cmd.Parameters.AddWithValue("@NgaySauChuNhat", ngaySauChuNhat);
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Thêm vào Dictionary (Ví dụ: Key="BuoiSang", Value=5)
                            counts.Add((string)reader["Buoi"], (int)reader["SoLuong"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy dữ liệu biểu đồ tròn: " + ex.Message);
                }
            }
        }
        return counts;
    }
    // 1. ĐẾM TỔNG SỐ LỊCH HẸN TRONG TUẦN HIỆN TẠI
    public static int GetTotalAppointmentsCurrentWeek()
    {
        int count = 0;

        // Logic lấy ngày đầu tuần
        DateTime homNay = DateTime.Today;
        int daysToSubtract = (homNay.DayOfWeek == DayOfWeek.Sunday) ? 6 : (int)homNay.DayOfWeek - (int)DayOfWeek.Monday;
        DateTime ngayDauTuan = homNay.AddDays(-daysToSubtract);
        DateTime ngaySauChuNhat = ngayDauTuan.AddDays(7);

        string query = @"
        SELECT COUNT(*) 
        FROM LichHen
        WHERE ThoiGianBatDau >= @NgayDauTuan AND ThoiGianBatDau < @NgaySauChuNhat;
    ";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NgayDauTuan", ngayDauTuan);
                cmd.Parameters.AddWithValue("@NgaySauChuNhat", ngaySauChuNhat);
                try
                {
                    conn.Open();
                    count = (int)cmd.ExecuteScalar(); // Lấy 1 con số duy nhất
                }
                catch (Exception ex) { MessageBox.Show("Lỗi đếm tổng lịch hẹn: " + ex.Message); }
            }
        }
        return count;
    }


    // 2. ĐẾM SỐ LỊCH HẸN CHỜ DUYỆT (TRẠNG THÁI KHÔNG PHẢI LÀ 'Đã đặt (đã duyệt)')
    public static int GetPendingAppointmentCount()
    {
        int count = 0;
        string query = "SELECT COUNT(*) FROM LichHen WHERE TrangThai != @TrangThaiDaDat";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // SỬA DÒNG NÀY:
                cmd.Parameters.AddWithValue("@TrangThaiDaDat", "Đã duyệt");

                try
                {
                    conn.Open();
                    count = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi đếm lịch chờ: " + ex.Message); }
            }
        }
        return count;
    }


    // 3. ĐẾM SỐ TÀI KHOẢN MỚI TRONG TUẦN NÀY
    public static int GetNewAccountsCurrentWeek()
    {
        int count = 0;

        // Logic lấy ngày đầu tuần
        DateTime homNay = DateTime.Today;
        int daysToSubtract = (homNay.DayOfWeek == DayOfWeek.Sunday) ? 6 : (int)homNay.DayOfWeek - (int)DayOfWeek.Monday;
        DateTime ngayDauTuan = homNay.AddDays(-daysToSubtract);
        DateTime ngaySauChuNhat = ngayDauTuan.AddDays(7);

        // (Quan trọng: Giả sử bạn có cột 'NgayTao' trong bảng 'TaiKhoan')
        string query = @"
        SELECT COUNT(*) 
        FROM TaiKhoan
        WHERE NgayTao >= @NgayDauTuan AND NgayTao < @NgaySauChuNhat;
    ";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@NgayDauTuan", ngayDauTuan);
                cmd.Parameters.AddWithValue("@NgaySauChuNhat", ngaySauChuNhat);
                try
                {
                    conn.Open();
                    count = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex) { MessageBox.Show("Lỗi đếm tài khoản mới: " + ex.Message); }
            }
        }
        return count;
    }
    // (Trong file DatabaseHelper.cs)

    // Hàm này lấy TOÀN BỘ lịch sử để hiển thị lên GridView
    public static DataTable GetLichSuDatLich()
    {
        DataTable dt = new DataTable();

        //sắp xếp theo mới nhất lên DatagridView
        string query = @"
        SELECT 
            ID, 
            Username_KhachHang, 
            ThoiGianBatDau, 
            NoiDung, 
            TrangThai 
        FROM 
            LichHen 
        ORDER BY 
            ThoiGianBatDau DESC"; // Sắp xếp theo ngày mới nhất

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    // Dùng SqlDataAdapter để lấy dữ liệu về DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt); // Đổ dữ liệu vào dt
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải lịch sử: " + ex.Message);
                }
            }
        }
        return dt; // Trả về bảng dữ liệu
    }
    //xóa dữ liệu trong bảng
    public static bool DeleteLichHen(int lichHenID)
    {
        int rowsAffected = 0;
        string query = "DELETE FROM LichHen WHERE ID = @LichHenID";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LichHenID", lichHenID);
                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa lịch hẹn: " + ex.Message);
                    return false;
                }
            }
        }
        return rowsAffected > 0;
    }
}