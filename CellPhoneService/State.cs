using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellPhoneService
{
    public class RegisterState
    {
        public static int Success = 1;
        public static int UsernameExist = 2;
        public static int InfoInValid = 3;
        public static int Error = 4;
    }
}