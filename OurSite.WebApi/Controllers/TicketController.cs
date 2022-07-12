﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OurSite.Core.DTOs.TicketsDtos;
using OurSite.Core.Services.Interfaces;
using OurSite.Core.Utilities;

namespace OurSite.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        #region constructor

        private ITicketService ticketService;
        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }
        #endregion


        #region ListTickets
        [HttpGet("Ticket-list")]
        public async Task<IActionResult> GetAllTicket()
        {
            var list = await ticketService.GetAllTicket();
            if (list.Any())
            {
                return JsonStatusResponse.Success(message: ("موفق"), ReturnData: list);

            }

            return JsonStatusResponse.NotFound(message: "تیکتی پیدا نشد");
        }

        #endregion


        #region found ticket by id
        
        [HttpGet("view-ticket/{ticketId}")]
        public async Task<IActionResult> FindTicket([FromRoute] long ticketId)
        {
            var res = await ticketService.FindTicketById(ticketId);
            if (res != null)
                return JsonStatusResponse.Success(res, "موفق");
            HttpContext.Response.StatusCode = 404;
            return JsonStatusResponse.NotFound("تیکت پیدا نشد ");
        }
        #endregion


        #region createTickes
        [HttpPost("create-ticket")]
        public async Task<IActionResult> CreateTicket([FromForm] TicketDto ticketDto)
        {
            if (ticketDto.SubmittedTicketFile != null)
            {

                var uploadFileResponse = await FileUploader.UploadFile(PathTools.FileUploadPath, ticketDto.SubmittedTicketFile, 10);

                switch (uploadFileResponse.Status)
                {
                    case resFileUploader.Success:
                        ticketDto.SubmittedTicketFileName = uploadFileResponse.FileName;
                        break;
                    case resFileUploader.Failure:
                        return JsonStatusResponse.Error("ارسال فایل با خطا مواجه شد");
                    case resFileUploader.ToBig:
                        return JsonStatusResponse.Error("حجم فایل انتخابی بیش از حد مجاز می‌باشد");
                    case resFileUploader.NoContent:
                        return JsonStatusResponse.Error("فایلی برای ارسال انتخاب نشده است");
                    case resFileUploader.InvalidExtention:
                        return JsonStatusResponse.Error("فرمت فایل انتخابی نامناسب می‌باشد");
                    default:
                        return JsonStatusResponse.Error("ارسال فایل با خطا مواجه شد");
                }
            }

            var res = await ticketService.createTicket(ticketDto);

            switch (res.resTicket)
            {
                case ResTicket.Success:
                    return JsonStatusResponse.Success("تیکت با موفقیت ارسال شد");

                default:
                    return JsonStatusResponse.Error("ارسال تیکت با خطا مواجه شد");
            }
        }
        #endregion
    }
}
