using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2.PaidTimeOffBLL.Framework
{
    public class ENTValidationError
    {
        public ENTValidationError() { }
        public string ErrorMessage { get; set; }
    }

    public class ENTValidationErrors : List<ENTValidationError>
    {
        public void Add(string errorMessage)
        {
            base.Add(new ENTValidationError { ErrorMessage = errorMessage });
        }
    }
}
