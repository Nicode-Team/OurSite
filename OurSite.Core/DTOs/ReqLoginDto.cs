﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OurSite.Core.DTOs
{
    public class ReqLoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public enum ResLoginDto
    {
        Success,
        IncorrectData,
        NotActived,
        Error
    }
}
