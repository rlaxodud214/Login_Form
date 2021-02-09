using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database
{
    class Column
    {
        // 데이터베이스 파라미터들
        // 아이디, 비밀번호, 이름, 폰번호, 나이
        public string ID { get; set; }
        
        public string PW { get; set; }

        public string NAME { get; set; }

        public string PHONEN { get; set; }

        public int AGE { get; set; }
    }
}

