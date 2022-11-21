﻿using MessagingSystemApp.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.CQRS.Commands.Response.EmployeeResponse
{
    public class ChangePasswordCommandResponse
    {
        public Token Token { get; set; } = null!;
    }
}