using database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_window
{
    class LoginCheck
    {
        Column column = new Column();

        // 경로 readonly 이후에 값을 변경할 수 없고 읽기만 가능하다.
        private readonly string currentDirectory = Environment.CurrentDirectory;
        // 서버에서 출력문, 변수명은 통일하는 값

        // Data Source = {0}, {1}; Initial Catalog = {2};, User ID = {3}; Password = {4};
        // 0, 1 : ip, port / 2 : db이름 / 3 : 아이디, / 4 : 비번                                                                          // 0, 1 : ip, port / 2 : db이름 / 3 : 아이디, / 4 : 비번
        public readonly string connectionStr = string.Format(
            "Data Source = {0}, {1}; Initial Catalog = {2}; User ID = {3}; Password = {4};",
            "127.0.0.1", 1433, "User", "sa", "1234");
        // Ctrl + k, d : 자동 줄맞춤

        private bool TryConnectToDatabase()
        {
            // sql 연결
            SqlConnection connection = new SqlConnection(connectionStr);

            // 에러 파일 생성 파일명은 년월일시분초.log
            string fileName = string.Format(@"\Errorlog\Text_{0}.log", DateTime.Now.ToString("yyyy년MM월dd일 HH시mm분ss초"));

            // StreamWriter = 파일에 쓰기
            // currentDirectory 이 경로에 + fileName = 파일이름     // {안에 내용}

            Column login_info = new Column(); // ID값으로 DB에서 데이터 불러와서 넣을 변수

            using (StreamWriter sw = new StreamWriter(currentDirectory + fileName, true))
            {
                // Open하고 Close안하면 권한이 꼬이므로 꼭 같이 세트로 적어줄 것
                sw.WriteLine("[{0}] 데이터베이스 연결 시도.", DateTime.Now.ToString("yyyy년MM월dd일 HH시mm분ss초"));
                connection.Open(); // 데이터 베이스 연결
                sw.WriteLine("[{0}] 데이터베이스 연결 성공.", DateTime.Now.ToString("yyyy년MM월dd일 HH시mm분ss초"));

                // 로그인 체크 코드
                // insertSQL 행 삽입 

                string SelectSQL = string.Format("SELECT * FROM [USER] WHERE ID='{0}'", column.ID);
                using (SqlConnection conn = new SqlConnection(connectionStr))
                {
                    conn.Open();

                    // TSQL문장과 Connection 객체를 지정   
                    SqlCommand cmd = new SqlCommand(SelectSQL, conn);

                    // 데이타는 서버에서 가져오도록 실행
                    SqlDataReader rdr = cmd.ExecuteReader();

                    // 다음 레코드 계속 가져와서 루핑
                    // C# 인덱서를 사용하여 필드 데이타 엑세스
                    if (rdr.Read())
                    {
                        login_info.ID = rdr["ID"] as string;
                        login_info.PW = rdr["PW"] as string;
                        login_info.NAME = rdr["NAME"] as string;
                        login_info.PHONEN = rdr["PHONEN"] as string;
                        login_info.AGE = Convert.ToInt32(rdr["AGE"] as string);

                    }
                    // 사용후 닫음
                    rdr.Close();
                }

                sw.WriteLine("[{0}] 데이터베이스 연결 종료 시도.", DateTime.Now.ToString("yyyy년MM월dd일 HH시mm분ss초"));
                connection.Close(); // 데이터 베이스 연결 종료
                sw.WriteLine("[{0}] 데이터베이스 연결 종료 성공.", DateTime.Now.ToString("yyyy년MM월dd일 HH시mm분ss초"));

                // 로그인 체크 코드
                if (login_info.PW == column.PW)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                

            }
        }

        public bool any(Column user)
        {
            column = user;
            return TryConnectToDatabase();
        }
    }
}
