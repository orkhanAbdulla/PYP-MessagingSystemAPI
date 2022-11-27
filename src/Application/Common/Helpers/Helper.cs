using MediatR;
using MessagingSystemApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Common.Helpers
{
    public static class Helper
    {
        public static Emoji IsExsistEmoji(Emoji emoji)
        {
            Emoji result;
            switch (emoji)
            {
                case Emoji.Completed:
                    result = Emoji.Completed;
                    break;
                case Emoji.Look:
                    result = Emoji.Look;
                    break;
                case Emoji.NicelyDone:
                    result = Emoji.NicelyDone;
                    break;
                case Emoji.Smile:
                    result = Emoji.Smile;
                    break;
                case Emoji.Joy:
                    result = Emoji.Joy;
                    break;
                case Emoji.Kissing_Heart:
                    result = Emoji.Kissing_Heart;
                    break;
                case Emoji.Chek:
                    result = Emoji.Chek;
                    break;
                default:
                    result = 0;
                    break;
            }
            return result;
        }
    }
}
