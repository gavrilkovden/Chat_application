﻿using DataAccessLayer.Repository.generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityDB
{
    public class UserEntity : BaseEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
