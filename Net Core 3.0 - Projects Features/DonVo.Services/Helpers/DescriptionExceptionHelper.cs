using DonVo.Services.Enums;
using DonVo.SpecialConfigurations.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Sockets;

namespace DonVo.Services.Helpers
{
    internal class DescriptionExceptionHelper
    {
        public static Errors GetDescriptionError(Exception exception)
        {
            if (exception.InnerException != null)
            {
                if (exception.InnerException is SqlException npgsqlException && npgsqlException.InnerException is SocketException socketException)
                {
                    if (socketException.SocketErrorCode == SocketError.ConnectionReset)
                    {
                        return Errors.ConnectionLost;
                    }
                }

                if (exception.InnerException is SqlException postgresException)
                {
                    switch (postgresException.State)
                    {
                        case 0:
                            return Errors.DataUsed;
                        case 1:
                            return Errors.DuplicateData;
                        default:
                            return Errors.DataBaseError;
                    }
                }
            }

            if (exception is DbUpdateConcurrencyException)
            {
                return Errors.TupleDeletedOrUpdated;
            }

            if (exception is SocketException socket)
            {
                if (socket.SocketErrorCode == SocketError.ConnectionRefused)
                {
                    return Errors.ConnectionLost;
                }
            }

            if (exception is DecryptHashIdException)
            {
                return Errors.IncorrectHashId;
            }

            return Errors.DataBaseError;
        }
    }
}