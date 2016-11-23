using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataAccess
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class CreditCardAttribute : DataTypeAttribute
    {
        public CreditCardAttribute() : base(DataType.CreditCard)
        {
            ErrorMessage = "";
        }
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            var text = value as string;
            if (text == null)
            {
                return false;
            }
            text = text.Replace("-", "");
            text = text.Replace(" ", "");
            var num = 0;
            var flag = false;
            foreach (var current in text.Reverse<char>())
            {
                if (current < '0' || current > '9')
                {
                    return false;
                }
                var i = (int)((current - '0') * (flag ? '\u0002' : '\u0001'));
                flag = !flag;
                while (i > 0)
                {
                    num += i % 10;
                    i /= 10;
                }
            }
            return num % 10 == 0;
        }
    }
}