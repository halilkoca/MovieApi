using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.DbTrackers
{
    public partial interface IRepository<TEntity> where TEntity : BaseEntity
    {
    }
}
