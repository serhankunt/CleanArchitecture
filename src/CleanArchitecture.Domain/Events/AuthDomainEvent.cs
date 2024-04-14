using CleanArchitecture.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Events;
public sealed class AuthDomainEvent : INotification
{
    public readonly AppUser _user;
    public AuthDomainEvent(AppUser user)
    {
        _user = user;
    }
}
