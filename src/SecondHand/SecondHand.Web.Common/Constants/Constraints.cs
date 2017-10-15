using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Web.Common.Constants
{
    public static class Constraints
    {
        public const int MIN_TITLE_LEN = 5;
        public const int MAX_TITLE_LEN = 20;

        public const int MIN_DESCRIPTION_LEN = 20;
        public const int MAX_DESCRIPTION_LEN = 1000;

        public const int MIN_NAME_LEN = 3;
        public const int MAX_NAME_LEN = 20;

        public const int MIN_PWD_LEN = 6;
        public const int MAX_PWD_LEN = 100;
    }
}
