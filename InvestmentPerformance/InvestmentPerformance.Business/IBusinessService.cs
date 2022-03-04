﻿using InvestmentPerformance.Business.Models;
using InvestmentPerformance.Business.Models.APIResponses;
using InvestmentPerformance.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentPerformance.Business
{
    public interface IBusinessService
    {
        Task<decimal> GetCurrentValue(Listing listing, UserInvestmentDetailsVM userInvestment);

        Task<decimal> GetGainLoss(Listing listing, UserInvestmentDetailsVM userInvestment);

        Task<GetUserInvestmentsByUserResponse> GetUserInvestmentVMs(int userId);

        Task<GetUserInvestmentsDetailsResponse> GetUserInvestmentsDetails(int investmentId);
    }
}
