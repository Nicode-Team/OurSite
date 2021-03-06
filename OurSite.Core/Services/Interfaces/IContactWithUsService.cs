using OurSite.Core.DTOs.ContactWithUsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurSite.Core.Services.Interfaces
{
    public interface IContactWithUsService : IDisposable
    {
        Task<bool> SendContactForm(ContactWithUsDto contactWithUsDto);

        Task<ResFilterContactWithUsDto> GetAllContactWithUs(ReqFilterContactWithUsDto filter);
    }
}
