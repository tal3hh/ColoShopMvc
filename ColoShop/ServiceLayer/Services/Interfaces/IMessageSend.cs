using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities;

namespace ServiceLayer.Services.Interfaces
{
    public interface IMessageSend
    {
        void MimeKitConfrim(AppUser appUser, string url, string code);
        void MimeMessageResetPassword(AppUser user, string url, string token);
    }
}
