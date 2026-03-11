using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SID_WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplexController : ControllerBase
    {
        [HttpGet("tables")]
        public IActionResult GetTables()
        {
            var tables = new
            {
                Classificacoes = new List<string>
                {
                    "Areas",
                    "Controllers",
                    "ControllerTypes",
                    "Cabinets",
                    "EngineeringUnits",
                    "CmAnalogInputType",
                    "CmAnalogOutputType",
                    "CmMotorType",
                    "CmTotalizerType",
                    "CmValveType",
                    "CmVSDType",
                    "CmVSDVendor",
                    "EnumerationGroup"
                },
                ControlModules = new List<string>
                {
                    "CmAnalogInput",
                    "CmAnalogOutput",
                    "CmDigitalInput",
                    "CmDigitalOutput",
                    "CmMotor",
                    "CmTotalizer",
                    "CmValve",
                    "CmVSD",
                    "CmPID"
                },
                Phases = new List<string>
                {
                    "PhaseClass",
                    "PhaseAlarmWarningInfosText",
                    "PhaseAlarmWarningInfos",
                    "PhaseSoftkeys",
                    "PhaseParameter"
                }
            };

            return Ok(tables);
        }
    }
}
