﻿namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TownsController : AdministrationController
    {
        public IActionResult Create()
        {
            return this.View();
        }
    }
}
