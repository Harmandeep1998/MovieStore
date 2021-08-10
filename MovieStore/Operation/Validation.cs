using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Operation
{
    public static class Validation
    {
        // Function to check Given Value is Number or Not
        public static bool IsNumber(string value)
        {
            try
            {
                // COnvert String to int
                int.Parse(value.Trim());
                return true; 
            }catch(Exception ex)
            {
                return false;
            }
        }

        // Check Given Value is Float or Not
        public static bool IsFloat(string value)
        {
            try
            {
                // COnvert String to float
                float.Parse(value.Trim());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Check Given String is Empty or NOt
        public static bool IsEmpty(string value)
        {
            return value.Trim().Length == 0;
        }

        // Check Given String contains Only digit
        public static bool IsOnlyDigitInString(string value)
        {
            if(value.Trim().Length == 0 )
            {
                return false;
            }
            for (int index = 0; index < value.Length; index++)
            {
                if (!char.IsDigit(value[index]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
