using DonVo.Services.Enums;
using DonVo.Services.Helpers;
using System.Collections.Generic;

namespace DonVo.Services.ActualResults
{
    public class ActualResult
    {
        public IEnumerable<string> ErrorsList { get; set; }
        public bool IsValid { get; set; }

        public ActualResult()
        {
            IsValid = true;
            ErrorsList = new List<string>();
        }

        public ActualResult(string error)
        {
            IsValid = false;
            ErrorsList = new List<string> { error };
        }

        public ActualResult(IEnumerable<string> errors)
        {
            IsValid = false;
            ErrorsList = errors;
        }

        public ActualResult(Errors error)
        {
            IsValid = false;
            ErrorsList = new List<string> { DescriptionErrorsHelper.DescriptionError(error) };
        }

        public ActualResult(IEnumerable<Errors> errors)
        {
            IsValid = false;
            ErrorsList = DescriptionErrorsHelper.DescriptionErrors(errors);
        }
    }
}