﻿using DonVo.IdentityJwtBearer.Entities;
using DonVo.IdentityJwtBearer.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DonVo.IdentityJwtBearer.EF
{
    public sealed class DonVoIdentityContext : IdentityDbContext<User>
    {
        public DonVoIdentityContext(DbContextOptions<DonVoIdentityContext> options) : base(options)
        {
            if (Database.EnsureCreated())
            {
                //InitializeDefaultUsers();
            }
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        private void InitializeDefaultUsers()
        {
            //--Credentials
            //--stewie.griffin@test.com  P@ssw0rd_admin
            //--accountant@test.com      P@ssw0rd_accountant
            //--deputy@test.com          P@ssw0rd_deputy

            const string sql = "INSERT INTO AspNetUsers(Id, FirstName, LastName, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnd, LockoutEnabled, AccessFailedCount) VALUES " +
                               "('80c3b9e5-48f0-43dc-9226-dc05f13cd471','Griffin','Stewie','stewie.griffin@test.com','STEWIE.GRIFFIN@TEST.COM','stewie.griffin@test.com','STEWIE.GRIFFIN@TEST.COM',0,'AQAAAAEAACcQAAAAEIpy4VbmDRxJOayhY6V2VSnGY+TtihLeiAxuHHcfpw++bIz5Qh1Zt/J3fQIU7MmojA==','7W3OKJNGBY43TZIF4B2U5QY5GRDG2WAX','03e08ee0-4d03-42d0-81a7-9f80d4f5151a',NULL,0,0,NULL,1,0), " +
                               "('bce59a46-2f93-4ab2-9e71-a023f7a851db','Griffin','Peter','accountant@test.com','ACCOUNTANT@TEST.COM','accountant@test.com','ACCOUNTANT@TEST.COM',0,'AQAAAAEAACcQAAAAEJmfjCDspGDCNMU0UKXmZ4aOqJ/kZr37HrVv1MWBDlHjSSfPBEqYBFrFTJYMFcUf2w==','O7GGVB4T3E2N4QRC6C2PC6GOML6WQ4VT','b320746f-de84-4ca9-ad12-be70aa943a20',NULL,0,0,NULL,1,0), " +
                               "('a2fd029e-f6d0-49aa-b3ca-c49d645bf41f','Griffin','Lois','deputy@test.com','DEPUTY@TEST.COM','deputy@test.com','DEPUTY@TEST.COM',0,'AQAAAAEAACcQAAAAEEHFo6yQWrGPFQwPUOFDtZ0V7C+F/oajrxJHWeeiWFZUVeP6AQKZ03+D66p0/wtQqQ==','N7SFWMK4W2XGIK4DFK3XAQMCW6FOQCXL','68f3773b-d59a-45fa-8763-519f60816d3d',NULL,0,0,NULL,1,0); " +

                               "INSERT INTO AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp) VALUES " +
                               "('c2c50d82-63d5-4764-a1b0-2919a826a4e6','Admin','ADMIN','7f5ebe86-e5d5-4c70-9f4e-467ff3c4d2d3'), " +
                               "('0c277213-cf17-4be6-966f-316233db2dc5','Accountant','ACCOUNTANT','f61ce026-bd54-4882-85c8-fe6ec3cc8227'), " +
                               "('121b555e-1037-4d80-90cc-1b5101d1af6d','Deputy','DEPUTY','80e5d186-5075-4cc4-ab95-5b70dbef7f2e'); " +

                               "INSERT INTO AspNetUserRoles(UserId, RoleId) VALUES " +
                               "('80c3b9e5-48f0-43dc-9226-dc05f13cd471','c2c50d82-63d5-4764-a1b0-2919a826a4e6'), " +
                               "('bce59a46-2f93-4ab2-9e71-a023f7a851db','0c277213-cf17-4be6-966f-316233db2dc5'), " +
                               "('a2fd029e-f6d0-49aa-b3ca-c49d645bf41f','121b555e-1037-4d80-90cc-1b5101d1af6d');";

            using (var dr = Database.ExecuteSqlQuery(sql))
            {
                dr.Close();
            }

            Clients.Add(new Client
            {
                ClientId = "WEB-APPLICATION",
                RefreshTokenLifeTime = 86400 //60 days
            });

            Clients.Add(new Client
            {
                ClientId = "DESKTOP-APPLICATION",
                RefreshTokenLifeTime = 86400 //60 days
            });

            Clients.Add(new Client
            {
                ClientId = "MOBILE-APPLICATION",
                RefreshTokenLifeTime = 86400 //60 days
            });

            SaveChanges();
        }
    }
}