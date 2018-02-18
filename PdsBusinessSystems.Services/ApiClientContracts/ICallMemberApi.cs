using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PdsBusinessSystems.Services.Contracts
{
   public interface ICallMemberApi
    {
        Task<string> GetMemeberDetails(int memebrId);
    }
}
