﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Domain.DTOs
{
    public class CreateLoanDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }

    }
}