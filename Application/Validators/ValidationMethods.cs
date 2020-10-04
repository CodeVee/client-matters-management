using System;
using System.Linq;

namespace Application.Validators
{
    public static class ValidationMethods
    {
        public static bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }
    }
}
