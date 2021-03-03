using System;

namespace App.Domain.SeedWork
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class IgnoreMemberAttribute: Attribute
    {
        
    }
}