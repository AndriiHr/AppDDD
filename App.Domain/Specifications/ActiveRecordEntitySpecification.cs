using App.Domain.Enums;
using App.Domain.SeedWork;
using System;
using System.Linq.Expressions;

namespace App.Domain.Specifications
{
    public class ActiveRecordEntitySpecification : Specification<Entity> 
    {
        private RecordStatus _recordStatus;

        public ActiveRecordEntitySpecification(bool isActive)
        {
            _recordStatus = isActive ? RecordStatus.Active : RecordStatus.Inactive;
        }

        public override Expression<Func<Entity, bool>> ToExpression()
        {
            return user => user.RecordStatus == _recordStatus;
        }
    }
}
