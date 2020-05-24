﻿using App.Core.DbTrackers;

namespace App.Core
{
    public abstract partial class BaseEntity : IEntity
    {
        public bool IsDeleted { get; set; }
    }
}
