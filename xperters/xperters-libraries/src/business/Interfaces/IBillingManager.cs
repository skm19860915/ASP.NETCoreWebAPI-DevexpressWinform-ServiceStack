using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;
using xperters.models;

namespace xperters.business.Interfaces
{
  public interface IBillingManager
    {
        ResultModel<CardDto> AddCard(CardDto cardDto);
        ResultModel AddAccountDetail(AccountDetailDto accountDetailDto);
        ResultModel<AccountDetailDto> GetAccountDetail(Guid userId);

        decimal GetBalanceByUserId();
        ResultModel<UserPaymentView> GetUserPaymentsList(int pageNo, int pageSize);
    }
}
