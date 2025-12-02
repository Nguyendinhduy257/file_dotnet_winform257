using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;//sinh số ngẫu nhiên
using System.Numerics;

namespace schedule_set_up_app
{
    public class MyRSA
    {
            // khóa công khai (n,e)
            public BigInteger n { get; private set; }
            public BigInteger e { get; private set; }
            // khóa bí mật (d)
            private BigInteger d;
        public MyRSA()
        {
            // 1. HARDCODE 2 SỐ NGUYÊN TỐ LỚN (P và Q)
            // dài như vạn lý trường thành để đảm bảo an toàn

            // Số P (Khoảng 77 chữ số)
            BigInteger p = BigInteger.Parse("9296615740417937402016480838198305739810899125740356525143329381657613767");

            // Số Q (Khoảng 77 chữ số)
            BigInteger q = BigInteger.Parse("8549666792348575001948842490549449852234033281987515152549282305716183307");

            // 2. Tính n (Modulus)
            n = p * q;

            // 3. Tính Phi(n)
            BigInteger phi = (p - 1) * (q - 1);

            // 4. Chọn e (Số mũ công khai)
            // e= 65537 là tiêu chuẩn quốc tế, hiệu quả và an toàn
            e = 65537;

            // 5. Tính d (Số mũ bí mật - Private Key)
            // Hàm ModInverse bạn giữ nguyên ở phía dưới class
            d = FindD_ByLoop(e, phi);
        }
        // HÀM TÌM D BẰNG CÁCH THỬ K=1,2,3,4,...==> cho đến khi D == số nguyên không có phân số
        // Công thức: d = (k * phi + 1) / e
        private BigInteger FindD_ByLoop(BigInteger e, BigInteger phi)
        {
            BigInteger k = 1;

            while (true)
            {
                // 1. Tính tử số: (k * phi) + 1
                BigInteger numerator = (k * phi) + 1;

                // 2. Kiểm tra xem tử số có chia hết cho e không?
                BigInteger remainder;
                //kiểm tra chia hết hay không
                BigInteger quotient = BigInteger.DivRem(numerator, e, out remainder);

                if (remainder == 0)
                {
                    // Nếu số dư bằng 0 -> Đã tìm thấy d là số nguyên!
                    return quotient; // Đây chính là d
                }

                // 3. Nếu không chia hết, tăng k lên thử tiếp
                k++;

                // (Lưu ý: Với e=65537, vòng lặp này chạy tối đa 65537 lần là ra, hiệu suất rất nhanh)
            }
        }
        // --- HÀM MÃ HÓA (ENCRYPT) ---
        // Công thức: C = M^e mod n
        public string Encrypt(string plainText)
            {
                // Chuyển chuỗi thành số (BigInteger)
                byte[] bytes = Encoding.UTF8.GetBytes(plainText);
                BigInteger m = new BigInteger(bytes);

                // Nếu số m lớn hơn n thì RSA không hoạt động đúng (với bản implementation đơn giản này)
                if (m >= n) throw new Exception("Tin nhắn quá dài so với khóa n!");

                // Tính toán lũy thừa module
                BigInteger c = BigInteger.ModPow(m, e, n);

                return c.ToString(); // Trả về chuỗi số đã mã hóa
            }

            // --- HÀM GIẢI MÃ (DECRYPT) ---
            // Công thức: M = C^d mod n
            public string Decrypt(string cipherText)
            {
                BigInteger c = BigInteger.Parse(cipherText);

                // Tính toán giải mã
                BigInteger m = BigInteger.ModPow(c, d, n);

                // Chuyển số ngược lại thành chuỗi
                byte[] bytes = m.ToByteArray();
                return Encoding.UTF8.GetString(bytes);
            }
        }
    }
