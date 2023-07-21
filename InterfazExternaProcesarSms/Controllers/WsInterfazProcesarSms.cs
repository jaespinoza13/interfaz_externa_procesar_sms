﻿using Microsoft.AspNetCore.Mvc;
using WsInterfazProcesarSms.Neg;
using WsInterfazProcesarSms.Model;
using Microsoft.Extensions.Options;

namespace InterfazExternaProcesarSms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WsInterfazProcesarSms : ControllerBase
    {
        private readonly ServiceSettings serviceSettings;

        public WsInterfazProcesarSms(IOptionsMonitor<ServiceSettings> settings)
        {
            serviceSettings = settings.CurrentValue;
        }

        [HttpPost]
        public IActionResult Action(ReqProcesarSms raw, string str_operacion)
        {
            InterfazProcesarSmsNeg objUtilidades = new(serviceSettings);
            var str_token = HttpContext.Request.Headers["Authorization"];
            object respuesta = objUtilidades.ProcesarSolicitud(raw, str_operacion, str_token);
            return Ok(respuesta);
        }
    }
}
