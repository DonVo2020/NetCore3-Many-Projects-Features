using DonVo.Services.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DonVo.Services.Helpers
{
    internal sealed class DescriptionErrorsHelper
    {
        internal static string DescriptionError(Errors error)
        {
            switch (error)
            {
                case Errors.ConnectionLost:
                    return "Database connection lost!";

                case Errors.ApplicationError:
                    return "Oops! Unknown error occurred!";

                case Errors.DataBaseError:
                    return "A database error has occurred!";

                case Errors.UserNotFound:
                    return "User not found, may have been deleted!";

                case Errors.DuplicateEmail:
                    return "This Email is Already Used!";

                case Errors.DuplicateData:
                    return "This entry already exists!";

                case Errors.TupleDeletedOrUpdated:
                    return "The record has been modified or deleted by another user!";

                case Errors.TupleDeleted:
                    return "The record was deleted!";

                case Errors.EmployeeDeleted:
                    return "Employee has been deleted!";

                case Errors.DataUsed:
                    return "Data is in use!";

                case Errors.NotFound:
                    return "Nothing was found at your request!";

                case Errors.FileNotFound:
                    return "File not found!";

                case Errors.IncorrectHashId:
                    return "Invalid ID";

                default:
                    return "Oops! An unknown error has occurred!";
            }
        }

        internal static List<string> DescriptionErrors(IEnumerable<Errors> errors)
        {
            return errors.Select(DescriptionError).ToList();
        }
    }
}
