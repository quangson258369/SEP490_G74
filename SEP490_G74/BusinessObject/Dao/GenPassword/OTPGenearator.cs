using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HcsBE.Dao.GenPassword
{
    public class OTPGenearator
    {
        public string OTPGenearatorRandom(int length = 6) 
        {
            const string validChars = "1234567890";

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(validChars.Length);
                password.Append(validChars[index]);
            }

            return password.ToString();
        }
    }
}
