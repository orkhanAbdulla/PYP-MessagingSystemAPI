using AutoMapper;
using MessagingSystemApp.Application.Common.Dtos.AttachmentDtos;
using MessagingSystemApp.Application.Common.Dtos.PostDtos;
using MessagingSystemApp.Application.Common.Dtos.ReactionDtos;
using MessagingSystemApp.Application.CQRS.Commands.Request.ConnectionRequest;
using MessagingSystemApp.Application.CQRS.Queries.Response.ConnectionResponse;
using MessagingSystemApp.Application.CQRS.Queries.Response.MessagingResponse;
using MessagingSystemApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MessagingSystemApp.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(T), GetType()).ReverseMap();
        }
    }
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Post, GetPostByConnectionIdQueryResponse>();
            CreateMap<Attachment, AttachmentGetDto>();
            CreateMap<Post, ReplyPostGetDto>();
            CreateMap<Reaction, ReactionGetDto>();
            CreateMap<Post, GetRepliesByPostIdQueryResponse>();
            CreateMap<Connection, GetDirectMessagesListByUserQueryRespose>().ForMember(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.UserName)).ForMember(dest => dest.SenderSignalRId, opt => opt.MapFrom(src => src.Sender.SignalRId)).ForMember(dest => dest.ReciverName, opt => opt.MapFrom(src => src.Reciver.UserName)).ForMember(dest => dest.ReciverSignalRId, opt => opt.MapFrom(src => src.Reciver.SignalRId));
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }
        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var mapFromType = typeof(IMapFrom<>);

            var mappingMethodName = nameof(IMapFrom<object>.Mapping);

            bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;

            var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

            var argumentTypes = new Type[] { typeof(Profile) };

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod(mappingMethodName);

                if (methodInfo != null)
                {
                    methodInfo.Invoke(instance, new object[] { this });
                }
                else
                {
                    var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                    if (interfaces.Count > 0)
                    {
                        foreach (var @interface in interfaces)
                        {
                            var interfaceMethodInfo = @interface.GetMethod(mappingMethodName, argumentTypes);

                            interfaceMethodInfo?.Invoke(instance, new object[] { this });
                        }
                    }
                }
            }
        }
    }
}
