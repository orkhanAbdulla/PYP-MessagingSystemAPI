using MessagingSystemApp.Application.Abstracts.Repositories;
using MessagingSystemApp.Application.CQRS.Queries.Response.MessagingResponse;
using MessagingSystemApp.Domain.Entities;
using MessagingSystemApp.Infrastructure.Persistence.Contexts;
using MessagingSystemApp.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Infrastructure.Persistence.Repositories
{
    public class PostRepository:Repository<Post,int>,IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {      
        }
        public async Task<IEnumerable<Post>> GetPostByConnectionId(int ConnectionId,int RepliesCount,int ReactionsCount)
        {
            return await Table.Where(x => x.IsReply == false && x.ConnectionId == ConnectionId).Include(x => x.Attachments).Include(x => x.ReplyPosts.Take(RepliesCount)).Include(x => x.Reactions.Take(ReactionsCount)).ToListAsync();
        }
        public async Task<IEnumerable<Post>> GetRepliesByPostId(int postId, int ReactionsCount)
        {
            return await Table.Where(x => x.IsReply == true && x.ReplyPostId == postId).Include(x => x.Reactions.Take(ReactionsCount)).ToListAsync();
        }
    }
}
