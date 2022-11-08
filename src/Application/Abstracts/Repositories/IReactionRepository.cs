﻿using MessagingSystemApp.Application.Abstracts.Repositories.Base;
using MessagingSystemApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Abstracts.Repositories
{
    public interface IReactionRepository:IRepository<Reaction,int>
    {
    }
}
