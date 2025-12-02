using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using schedule_set_up_app;
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
    //=============================================================
    // HÀM KIỂM TRA ĐĂNG NHẬP (CHO Form1)
    // Sẽ trả về "User", "Admin", hoặc "Invalid" hoặc "Bị khóa"
    //=============================================================
    // Hàm hỗ trợ mã hóa nhanh bằng MyRSA giải thuật mã hóa RSA
    public static string EncryptPassword(string password)
    {
        // 1. Khởi tạo RSA (Nó sẽ tự dùng p, q cứng mà bạn đã cài)
        MyRSA rsa = new MyRSA();

        // 2. Mã hóa mật khẩu
        try
        {
            return rsa.Encrypt(password);
        }
        catch (Exception ex)
        {
            // Nếu mật khẩu quá dài so với khóa n, báo lỗi
            MessageBox.Show("Mật khẩu quá dài để mã hóa! " + ex.Message);
            return null;
        }
    }
    //Kiểm tra định dạng đăng nhập, bắt đầu kiểm tra ở form_login, sau đó kiểm tra trên CSDL
    public static string CheckLogin(string username, string password)
    {
        // 1. Lấy thông tin của Username đó (Chưa cần kiểm tra password vội)
        string query = "SELECT Password, Role, SoLanSai FROM TaiKhoan WHERE Username = @User";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@User", username);
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // A. NẾU USERNAME KHÔNG TỒN TẠI
                        if (!reader.Read())
                        {
                            return "Invalid"; // Không tìm thấy user
                        }

                        // Lấy dữ liệu từ CSDL
                        string dbPassword = reader["Password"].ToString();
                        string dbRole = reader["Role"].ToString();
                        int soLanSai = (reader["SoLanSai"] != DBNull.Value) ? Convert.ToInt32(reader["SoLanSai"]) : 0;

                        // B. KIỂM TRA XEM TÀI KHOẢN CÓ BỊ KHÓA KHÔNG?
                        if (dbRole == "Đã khóa")
                        {
                            return "Locked"; // Tài khoản đang bị khóa
                        }

                        // C. KIỂM TRA MẬT KHẨU (DÙNG RSA)

                        // 1. Mã hóa mật khẩu người dùng vừa nhập bằng RSA
                        string inputPasswordHash = EncryptPassword(password);

                        // 2. So sánh chuỗi vừa mã hóa với chuỗi trong Database
                        // (Lưu ý: Nếu inputPasswordHash là null do lỗi quá dài, coi như sai pass)
                        if (inputPasswordHash != null && dbPassword == inputPasswordHash)
                        {
                            // --- ĐĂNG NHẬP THÀNH CÔNG ---
                            reader.Close();

                            // Reset số lần sai về 0
                            ResetLoginAttempts(username);

                            return dbRole; // Trả về Role (Admin/User)
                        }
                        else
                        {
                            // --- ĐĂNG NHẬP THẤT BẠI (SAI PASS) ---
                            reader.Close(); // Đóng reader


                            //Admin chỉ báo sai pass, không tăng số lần sai và không khóa tài khoản của Admin
                            if (dbRole == "Admin")
                            {
                                return "WrongPass|Admin"; // Báo sai pass nhưng không làm gì thêm
                            }
                            // Tăng số lần sai
                            soLanSai++;
                            UpdateLoginAttempts(username, soLanSai);

                            // Kiểm tra nếu sai quá 5 lần
                            if (soLanSai >= 5)
                            {
                                LockUserAccount(username);
                                return "LockedNow"; // Vừa mới bị khóa tức thì
                            }

                            return "WrongPass|"+soLanSai; // Sai pass nhưng chưa bị khóa
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi đăng nhập: " + ex.Message);
                    return "Error";
                }
            }
        }
    }
    // Hàm reset số lần sai về 0
    private static void ResetLoginAttempts(string username)
    {
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            string query = "UPDATE TaiKhoan SET SoLanSai = 0 WHERE Username = @User";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@User", username);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    // Hàm cập nhật số lần sai, +1 với mỗi lần sai
    private static void UpdateLoginAttempts(string username, int count)
    {
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            string query = "UPDATE TaiKhoan SET SoLanSai = @Count WHERE Username = @User";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Count", count);
                cmd.Parameters.AddWithValue("@User", username);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    // Hàm khóa tài khoản (Đổi Role thành "Đã khóa")
    private static void LockUserAccount(string username)
    {
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            string query = "UPDATE TaiKhoan SET Role = N'Đã khóa' WHERE Username = @User";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@User", username);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
    //=============================================================
    // HÀM ĐĂNG KÝ TÀI KHOẢN (CHO Form_sign_up)
    //=============================================================
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
                    //cmdRegister.Parameters.AddWithValue("@Pass", password);
                    // 1. Mã hóa trước
                    string encryptedPass = EncryptPassword(password);

                    // 2. Lưu chuỗi đã mã hóa vào SQL
                    cmdRegister.Parameters.AddWithValue("@Pass", encryptedPass);
                    cmdRegister.Parameters.AddWithValue("@Role", role);

                    int rows = cmdRegister.ExecuteNonQuery();
                    return rows > 0 ? "SUCCESS" : "FAILED"; //>0 là thành công, con lại là thất bại
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi CSDL khi đăng ký: " + ex.Message);
                return "ERROR";
            }
        }
    }
    //=============================================================
    // HÀM DÀNH CHO Form_trang_chu_admin
    //=============================================================
    // HÀM LẤY SỐ LIỆU CHO BIỂU ĐỒ Cho trang chu Admin
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
    // Biểu đồ tròn hiển thị số lượng lịch hẹn theo buổi trong ngày (sáng, chiều, tối)
    //trong form _trang_chu Admin
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
    // (Hàm này dùng cho form_trang_chu_admin)
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
    //trong form_trang_chu_admin
    public static int GetPendingAppointmentCount()
    {
        int count = 0;
        string query = "SELECT COUNT(*) FROM LichHen WHERE TrangThai != @TrangThaiDaDat";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
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
    // (Hàm này dùng cho form_trang_chu_admin)
    public static int GetNewAccountsCurrentWeek()
    {
        int count = 0;

        // Logic lấy ngày đầu tuần
        DateTime homNay = DateTime.Today;
        int daysToSubtract = (homNay.DayOfWeek == DayOfWeek.Sunday) ? 6 : (int)homNay.DayOfWeek - (int)DayOfWeek.Monday;
        DateTime ngayDauTuan = homNay.AddDays(-daysToSubtract);
        DateTime ngaySauChuNhat = ngayDauTuan.AddDays(7);

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

    // Hàm này lấy TOÀN BỘ lịch sử để hiển thị lên dataGridView
    //cho form_trang_chu_admin
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
            -- 1. Ưu tiên 'chưa duyệt' lên đầu (Gán giá trị 0, các cái khác là 1)
            CASE WHEN TrangThai = N'chưa duyệt' THEN 0 ELSE 1 END ASC,
        
            -- 2. Sau đó mới sắp xếp theo ngày mới nhất
            ThoiGianBatDau DESC";

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
    // HÀM XÓA LỊCH HẸN (CHO Form_trang_chu_admin)
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
    //=============================================================
    // HÀM DÀNH CHO Form_Profile
    //=============================================================
    //Lấy các thông tin Họ tên, pass,... từ CSDL để tự động hiện lên form_profile
    // HÀM LẤY CHI TIẾT TÀI KHOẢN (CHO Form_Profile)
    public static DataTable GetTaiKhoanDetails(string username)
    {
        DataTable dt = new DataTable();
        string query = "SELECT Password, Hoten, Email, Role FROM TaiKhoan WHERE Username = @Username";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải chi tiết tài khoản: " + ex.Message);
                }
            }
        }
        return dt;
    }
    //sửa tài khoản
    // HÀM Cập nhật tài khoản (cho Form_Profile)
    //cho phép thay đổi Password, Họ tên, Email, không cho đổi Username
    //cho form_profile
    public static bool UpdateTaiKhoan(string username, string newPassword, string newHoten, string newEmail)
    {
        int rowsAffected = 0;
        string query = @"
        UPDATE TaiKhoan 
        SET Password = @Password, Hoten = @Hoten, Email = @Email 
        WHERE Username = @Username";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);

                // LOGIC kiểm tra:
                //+độ dài mật khẩu < 100: Chắc chắn là người dùng mới nhập mật khẩu mới -> Mã hóa ngay
                //+độ dài mật khẩu >=100: Chắc chắn là chuỗi RSA cũ -> Giữ nguyên không mã hóa lại
                string passwordToSave = newPassword;

                // Kiểm tra: Nếu chuỗi nhập vào KHÔNG RỖNG và ĐỘ DÀI < 100 ký tự
                // -> Nghĩa là đây là mật khẩu thường (Plain text) -> CẦN MÃ HÓA
                if (!string.IsNullOrEmpty(newPassword) && newPassword.Length < 100)
                {
                    passwordToSave = EncryptPassword(newPassword);
                }
                // Ngược lại: Nếu độ dài >= 100 -> Đây là chuỗi RSA cũ -> Giữ nguyên
                // độ dài của RSA hiện tại là 128 ký tự

                cmd.Parameters.AddWithValue("@Password", passwordToSave);
                // -----------------------------------------------------

                cmd.Parameters.AddWithValue("@Hoten", newHoten);
                cmd.Parameters.AddWithValue("@Email", newEmail);
                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật tài khoản: " + ex.Message);
                    return false;
                }
            }
        }
        return rowsAffected > 0;
    }
    //đếm tài khoản Admin, nếu chỉ còn lại duy nhất 1 tài khoản thuộc Role Admin --> tuyệt đối cấm xóa
    //cho form_profile
    public static int GetAdminAccountCount()
    {
        int count = 0;
        string query = "SELECT COUNT(*) FROM TaiKhoan WHERE Role = 'Admin'";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    count = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi đếm tài khoản admin: " + ex.Message);
                }
            }
        }
        return count;
    }

    // HÀM thực hiện Xóa tài khoản (cho Form_Profile)
    public static bool DeleteTaiKhoan(string username)
    {
        int rowsAffected = 0;
        string query = "DELETE FROM TaiKhoan WHERE Username = @Username";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa tài khoản: " + ex.Message);
                    return false;
                }
            }
        }
        return rowsAffected > 0;
    }
    //Hàm lấy chi tiết lịch hẹn theo ID (lấy tất cả từ LichHen)
    public static DataTable GetLichHenDetailsByID(int lichHenID)
    {
        DataTable dt = new DataTable();
        // Lấy tất cả thông tin
        string query = "SELECT * FROM LichHen WHERE ID = @ID";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID", lichHenID);
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải chi tiết lịch hẹn: " + ex.Message);
                }
            }
        }
        return dt;
    }
    // HÀM cập nhật lịch hẹn (quyền hạn cho Admin)
    //chỉ có quyền sửa ThoiGianBatDau và TrangThai, còn NoiDung thì không được sửa
    //trong form_trang_chu_admin
    public static bool UpdateLichHen_Admin(int lichHenID, DateTime thoiGianMoi, string trangThaiMoi)
    {
        int rowsAffected = 0;
        string query = @"
        UPDATE LichHen 
        SET 
            ThoiGianBatDau = @ThoiGian, 
            TrangThai = @TrangThai 
        WHERE ID = @ID";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ThoiGian", thoiGianMoi);
                cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);
                cmd.Parameters.AddWithValue("@ID", lichHenID);
                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật lịch hẹn: " + ex.Message);
                    return false;
                }
            }
        }
        return rowsAffected > 0;
    }
    // HÀM Chỉ cập nhật trạng thái (Dùng cho sửa hàng loạt)
    public static bool UpdateStatusOnly(int id, string trangThaiMoi)
    {
        int rowsAffected = 0;
        string query = "UPDATE LichHen SET TrangThai = @TrangThai WHERE ID = @ID";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);
                cmd.Parameters.AddWithValue("@ID", id);
                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Thông báo lỗi nếu cần thiết
                    MessageBox.Show("Lỗi cập nhật trạng thái: " + ex.Message);
                    return false;
                }
            }
        }
        return rowsAffected > 0;
    }
    // HÀM Lấy TOÀN BỘ thông tin tài khoản (cho Admin xem trên datagridview)
    public static DataTable GetAllTaiKhoan()
    {
        DataTable dt = new DataTable();
        // Lấy tất cả trừ mật khẩu
        string query = "SELECT Username, Hoten, Email, Role, NgayTao FROM TaiKhoan";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách tài khoản: " + ex.Message);
                }
            }
        }
        return dt;
    }

    // HÀM Cập nhật VAI TRÒ (Role: User/ Admin --> do Admin thực hiện)
    public static bool UpdateUserRole(string username, string newRole)
    {
        int rowsAffected = 0;
        string query = "UPDATE TaiKhoan SET Role = @Role WHERE Username = @Username";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Role", newRole);
                cmd.Parameters.AddWithValue("@Username", username);
                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật vai trò: " + ex.Message);
                    return false;
                }
            }
        }
        return rowsAffected > 0;
    }
    // HÀM Dành cho Admin xem TOÀN BỘ báo cáo được gửi từ khách hàng (truy vấn từ Tabel BaoCao trên sql)
    // trong form_trang_chu_admin, Admin có quyền duyệt báo cáo (duyệt xem phản hồi của khách hàng đã được giải quyết chưa?)
    public static DataTable GetAllReportsForAdmin()
    {
        DataTable dt = new DataTable();
        // Lấy tất cả các cột cần thiết, sắp xếp theo ngày gửi mới nhất lên trên
        string query = @"
        SELECT 
            ID,
            Username_NguoiGui, 
            LoaiBaoCao, 
            NoiDung, 
            NgayGui, 
            TrangThai 
        FROM 
            BaoCao 
        ORDER BY 
            NgayGui DESC"; // Sắp xếp theo ngày mới nhất

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách báo cáo: " + ex.Message);
                }
            }
        }
        return dt;
    }
    //Hàm cập nhật trạng thái báo cáo: trạng thái đã duyệt/ chưa duyệt (do Admin thực hiện)
    public static bool UpdateReportStatus(int reportID, string newStatus)
    {
        int rowsAffected = 0;
        string query = "UPDATE BaoCao SET TrangThai = @Status WHERE ID = @ID";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Status", newStatus);
                cmd.Parameters.AddWithValue("@ID", reportID);
                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật trạng thái báo cáo: " + ex.Message);
                }
            }
        }
        return rowsAffected > 0;
    }

    public static DataTable SearchReportsForAdmin(string loaiBaoCaoFilter = null)
    {
        DataTable dt = new DataTable();

        // chọn Username_NguoiGui trực tiếp từ bảng BaoCao trong sql
        //BC là: được khai báo ở SQL: FROM BaoCao AS BC (là bí danh)
        string query = @"
        SELECT 
            BC.ID, 
            BC.Username_NguoiGui,
            BC.LoaiBaoCao, 
            BC.NoiDung, 
            BC.NgayGui, 
            BC.TrangThai 
        FROM 
            BaoCao AS BC 
        WHERE 
            (@LoaiBaoCaoFilter IS NULL OR BC.LoaiBaoCao LIKE '%' + @LoaiBaoCaoFilter + '%')
        ORDER BY 
            BC.NgayGui DESC";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                if (string.IsNullOrWhiteSpace(loaiBaoCaoFilter))
                {
                    cmd.Parameters.AddWithValue("@LoaiBaoCaoFilter", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LoaiBaoCaoFilter", loaiBaoCaoFilter);
                }

                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải danh sách báo cáo: " + ex.Message);
                }
            }
        }
        return dt;
    }
    //=============================================================
    //Tất cả hàm danh cho Form_Booking (Khách hàng tự đặt lịch)
    //=============================================================
    // HÀM Dành cho Form_Booking (Khách hàng tự đặt lịch)
    public static bool TaoLichHenMoi(string username, DateTime thoiGianHen, string noiDung)
    {
        int rowsAffected = 0;

        // Khi khách hàng tạo, TrangThai luôn mặc định là "chưa duyệt"
        string query = @"INSERT INTO LichHen (Username_KhachHang, ThoiGianBatDau, NoiDung, TrangThai) 
                     VALUES (@Username, @ThoiGian, @NoiDung, @TrangThai)";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Thêm tham số
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@ThoiGian", thoiGianHen);
                cmd.Parameters.AddWithValue("@NoiDung", noiDung);
                cmd.Parameters.AddWithValue("@TrangThai", "chưa duyệt"); // Trạng thái mặc định

                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tạo lịch hẹn: " + ex.Message);
                    return false;
                }
            }
        }

        // Trả về true nếu chèn thành công (rowsAffected > 0)
        return (rowsAffected > 0);
    }
    // HÀM Lấy lịch hẹn cá nhân của User đang đăng nhập (cho Form_Booking)
    //tài khoản của ai thì chỉ lấy lịch của người đó, không lấy lịch của người khác chèn vào
    public static DataTable GetLichHenCaNhan(string username)
    {
        DataTable dt = new DataTable();

        // Lấy các cột cần thiết và đổi tên (AS) cho dễ đọc trên GridView
        // Sắp xếp theo lịch mới nhất lên trước
        string query = @"
        SELECT 
            ID AS MaLich, 
            NoiDung AS DichVu, 
            ThoiGianBatDau AS NgayHen, 
            TrangThai 
        FROM 
            LichHen 
        WHERE 
            Username_KhachHang = @Username
        ORDER BY 
            ThoiGianBatDau DESC";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Chỉ lấy lịch của user đang đăng nhập
                cmd.Parameters.AddWithValue("@Username", username);
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt); // Đổ dữ liệu vào DataTable
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải lịch hẹn cá nhân: " + ex.Message);
                }
            }
        }
        return dt; // Trả về bảng dữ liệu
    }

    // HÀM Hủy lịch hẹn (do User thực hiện)
    public static bool HuyLichHenUser(int lichHenID)
    {
        int rowsAffected = 0;
        // Đây là 1 Cập nhật (UPDATE), không phải Xóa (DELETE)
        string query = "UPDATE LichHen SET TrangThai = N'Đã hủy' WHERE ID = @ID";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ID", lichHenID);
                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi hủy lịch hẹn: " + ex.Message);
                    return false;
                }
            }
        }
        // Trả về true nếu cập nhật thành công (rowsAffected > 0)
        return rowsAffected > 0;
    }


    //Form_Report
    // HÀM Gửi báo cáo (do User thực hiện), báo cáo sẽ được lưu vào bảng BaoCao, sau đó Admin sẽ xem và xử lý
    public static bool SubmitReport(string username, string loaiBaoCao, string noiDung)
    {
        int rowsAffected = 0;
        string query = @"
        INSERT INTO BaoCao (Username_NguoiGui, LoaiBaoCao, NoiDung) 
        VALUES (@Username, @Loai, @NoiDung)";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Loai", loaiBaoCao);
                cmd.Parameters.AddWithValue("@NoiDung", noiDung);
                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi gửi báo cáo: " + ex.Message);
                    return false;
                }
            }
        }
        return rowsAffected > 0;
    }

    // HÀM Lấy lịch sử báo cáo của User đang đăng nhập (cho Form_Report), chỉ lấy báo cáo của chính người đó
    public static DataTable GetMyReports(string username)
    {
        DataTable dt = new DataTable();
        // Sắp xếp theo ngày mới nhất lên trên
        string query = @"
        SELECT 
            ID, 
            LoaiBaoCao, 
            NoiDung, 
            NgayGui, 
            TrangThai 
        FROM 
            BaoCao 
        WHERE 
            Username_NguoiGui = @Username 
        ORDER BY 
            NgayGui DESC";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Chỉ lấy báo cáo của user đang đăng nhập, tài khoản của ai thì chỉ lấy của người đó
                cmd.Parameters.AddWithValue("@Username", username);
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải lịch sử báo cáo: " + ex.Message);
                }
            }
        }
        return dt;
    }
    //=============================================================
    // Hàm này dành cho Form_Booking    
    //=============================================================
    //hàm lấy lịch hẹn của 1 ngày cụ thể mà người dùng Double-click vào
    // 1. Lấy lịch hẹn trong ngày được Double-click (dành cho User khách hàng tương tác)
    public static DataTable GetLichHenTrongNgay(string username, DateTime ngay)
    {
        DataTable dt = new DataTable();
        string query = @"SELECT ID, ThoiGianBatDau, NoiDung, TrangThai 
                     FROM LichHen 
                     WHERE Username_KhachHang = @Username 
                       AND CONVERT(date, ThoiGianBatDau) = @Ngay
                     ORDER BY ThoiGianBatDau";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                //lấy theo username và ngày cụ thể đang được Click
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Ngay", ngay.Date);
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải lịch hẹn ngày: " + ex.Message);
                }
            }
        }
        return dt;
    }

    // 2. Sửa lịch hẹn (tái sử dụng dành cho User khách hàng tương tác)
    public static bool UpdateLichHen(int lichHenID, DateTime thoiGianMoi, string noiDungMoi)
    {
        int rowsAffected = 0; // Khởi tạo

        string query = @"UPDATE LichHen 
                     SET ThoiGianBatDau = @ThoiGian, NoiDung = @NoiDung 
                     WHERE ID = @ID";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                //lấy theo ID lịch hẹn cần sửa, được phép sửa ThoiGianBatDau và NoiDung
                cmd.Parameters.AddWithValue("@ThoiGian", thoiGianMoi);
                cmd.Parameters.AddWithValue("@NoiDung", noiDungMoi);
                cmd.Parameters.AddWithValue("@ID", lichHenID);

                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật lịch hẹn: " + ex.Message);
                    return false;
                }
            }
        }
        // rowsAffected > 0 mới được trả về TRUE
        return (rowsAffected > 0);
    }
    // Kiểm tra trùng lịch hẹn (Ngẳn chặn ngay từ khi có ý định "thêm" lập lịch)
    //Nếu có lịch trùng thì trả về TRUE, không cho phép tạo lịch đến khi không bị trùng nữa
    public static bool KiemTraLichTrung(string username, DateTime thoiGianHen, int lichHenID_CanLoaiTru = -1)
    {
        int count = 0;

        // query đếm xem có lịch nào bị trùng thời gian không.
        string query = @"SELECT COUNT(*) 
                     FROM LichHen 
                     WHERE Username_KhachHang = @Username 
                       AND ThoiGianBatDau = @ThoiGian
                       AND ID != @ID_CanLoaiTru"; 

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                //lấy theo username và thời gian lịch hẹn, loại trừ ID nếu có
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@ThoiGian", thoiGianHen);
                cmd.Parameters.AddWithValue("@ID_CanLoaiTru", lichHenID_CanLoaiTru);
                try
                {
                    conn.Open();
                    // ExecuteScalar dùng để lấy 1 giá trị (số lượng)
                    count = (int)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kiểm tra lịch trùng: " + ex.Message);
                }
            }
        }

        // Nếu count > 0, nghĩa là đã tìm thấy 1 lịch khác bị trùng
        return (count > 0);
    }

    // HÀM Lấy lịch hẹn trong 1 khoảng thời gian (1 tuần)
    // (Hàm này dùng cho form_trang_chu khach hang?)
    public static DataTable GetLichHenTrongTuan(string username, DateTime startDate, DateTime endDate)
    {
        DataTable dt = new DataTable();

        // Lấy các lịch hẹn trong khoảng [startDate, endDate)
        string query = @"SELECT ID, ThoiGianBatDau, NoiDung, TrangThai 
                     FROM LichHen 
                     WHERE Username_KhachHang = @Username 
                       AND ThoiGianBatDau >= @StartDate 
                       AND ThoiGianBatDau < @EndDate
                     ORDER BY ThoiGianBatDau";

        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@StartDate", startDate.Date); // Chỉ lấy ngày, không lấy tháng, năm, giờ, phút
                cmd.Parameters.AddWithValue("@EndDate", endDate.Date);   // Chỉ lấy ngày, không lấy tháng, năm, giờ, phút
                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải lịch hẹn tuần: " + ex.Message);
                }
            }
        }
        return dt;
    }
}