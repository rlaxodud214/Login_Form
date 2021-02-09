using Login_window;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace database
{
    class Insert
    {
        Column column = new Column();

        // 경로 readonly 이후에 값을 변경할 수 없고 읽기만 가능하다.
        private readonly string currentDirectory = Environment.CurrentDirectory;
        // 서버에서 출력문, 변수명은 통일하는 값

        // Data Source = {0}, {1}; Initial Catalog = {2};, User ID = {3}; Password = {4};
        // 0, 1 : ip, port / 2 : db이름 / 3 : 아이디, / 4 : 비번                                                                          // 0, 1 : ip, port / 2 : db이름 / 3 : 아이디, / 4 : 비번
        private readonly string connectionStr = string.Format("Data Source = {0}, {1}; Initial Catalog = {2}; User ID = {3}; Password = {4};", 
            "127.0.0.1", 1433, "User", "sa", "1234");

        public void Run()
        {
            // 디렉토리에 에러 텍스트 만드는 메서드
            CheckedDriectory();
            // 데이터베이스 연결
            inserts();
        }

        public void any(Column user)
        {
            column = user;
            Run();
        }

        //internal Column Register(Column user) { return user;  }

        private void CheckedDriectory()
        {
            // currentDirectory : 솔루션 경로
            // @"\Errorlog" : 경로에 폴더 생성
            DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory + @"\Errorlog");
            // @"\Errorlog" : 없으면 만들기
            if (!directoryInfo.Exists) // 디렉토리가 존재하지 않으면
                directoryInfo.Create(); // 디렉토리를 생성해라
        }

        public void inserts()
        {
            // sql 연결
            SqlConnection connection = new SqlConnection(connectionStr);
            // 에러 파일 생성 파일명은 년월일시분초.log
            string fileName = string.Format(@"\Errorlog\Text_{0}.log", DateTime.Now.ToString("yyyy년MM월dd일 HH시mm분ss초"));

            using (StreamWriter sw = new StreamWriter(currentDirectory + fileName, true))
            {
                // Open하고 Close안하면 권한이 꼬이므로 꼭 같이 세트로 적어줄 것
                sw.WriteLine("[{0}] 데이터베이스 연결 시도.", DateTime.Now.ToString("yyyy년MM월dd일 HH시mm분ss초"));
                connection.Open(); // 데이터 베이스 연결
                sw.WriteLine("[{0}] 데이터베이스 연결 성공.", DateTime.Now.ToString("yyyy년MM월dd일 HH시mm분ss초"));

                // insertSQL 행 삽입 
                string insertSQL = string.Format("INSERT INTO " +
                                                 "[USER] (ID, PW, NAME, PHONEN, AGE) " +
                                                 "VALUES ('{0}', '{1}', '{2}', '{3}', {4})",
                                                  column.ID, column.PW, column.NAME, column.PHONEN, column.AGE);

                using (SqlCommand command = new SqlCommand(insertSQL, connection))
                {
                    var activeNumber = command.ExecuteNonQuery();
                }

                sw.WriteLine("[{0}] 데이터베이스 연결 종료 시도.", DateTime.Now.ToString("yyyy년MM월dd일 HH시mm분ss초"));
                connection.Close(); // 데이터 베이스 연결 종료
                sw.WriteLine("[{0}] 데이터베이스 연결 종료 성공.", DateTime.Now.ToString("yyyy년MM월dd일 HH시mm분ss초"));
            }
        }
    }
}
