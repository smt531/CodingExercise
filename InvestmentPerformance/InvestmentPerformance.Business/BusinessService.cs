﻿using InvestmentPerformance.Business.Models;
using InvestmentPerformance.Business.Models.APIResponses;
using InvestmentPerformance.Data;
using InvestmentPerformance.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPerformance.Business
{
    public class BusinessService : IBusinessService
    {
        public async Task<GetUserInvestmentsByUserResponse> GetUserInvestmentsByUser(int userId)
        {
            var response = new GetUserInvestmentsByUserResponse
            {
                Investments = new List<UserInvestmentVM>()
            };

            using (var context = new InvestmentPerformanceContext())
            {
                var user = context.Users.Find(userId);

                if (user == null)
                {
                    throw new Exception($"User with ID {userId} not found!");
                }

                var userInvestments = await context.UserInvestments
                    .Include(ui => ui.Listing)
                    .Where(x => x.UserId == userId)
                    .ToListAsync();

                response.User = new UserVM().MapFrom(user);
                response.Investments = userInvestments.Select(x => new UserInvestmentVM().MapFrom(x)).ToList();
            }

            return response;
        }

        public async Task<GetUserInvestmentsDetailsResponse> GetUserInvestmentsDetails(int userInvestmentId)
        {
            var response = new GetUserInvestmentsDetailsResponse();

            using (var context = new InvestmentPerformanceContext())
            {
                var userInvestment = await context.UserInvestments
                    .Include(ui => ui.Listing)
                    .Where(x => x.Id == userInvestmentId)
                    .FirstOrDefaultAsync();

                if (userInvestment == null)
                {
                    throw new Exception($"Investment ID {userInvestmentId} not found!");
                }

                response.UserInvestment = new UserInvestmentDetailsVM().MapFrom(userInvestment);
            }

            return response;
        }

    }
}
