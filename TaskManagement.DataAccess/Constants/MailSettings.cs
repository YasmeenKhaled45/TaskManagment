﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.DataAccess.Constants
{
    public class MailSettings
    {
        [Required, EmailAddress]
        public string Mail { get; set; } = string.Empty;
        [Required]
        public string DisplayName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Host { get; set; } = string.Empty;
        [Required]
        [Range(100, 999)]
        public int Port { get; set; }
    }
}
