﻿using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Infrastructure.Persistence.Contexts;
using MessagingSystemApp.Infrastructure.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.Persistence.Repositories
{
    public class ReactionRepository:Repository<Reaction,int>,IReactionRepository
    {
        public ReactionRepository(ApplicationDbContext context):base(context)
        {

        }
    }
}
