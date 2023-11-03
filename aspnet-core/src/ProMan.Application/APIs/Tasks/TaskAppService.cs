using Microsoft.AspNetCore.Mvc;
using ProMan.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProMan.APIs.Tasks
{
    public class TaskAppService : ProManAppServiceBase
    {
        public TaskAppService(IContext context) : base(context) { }

    }
}
