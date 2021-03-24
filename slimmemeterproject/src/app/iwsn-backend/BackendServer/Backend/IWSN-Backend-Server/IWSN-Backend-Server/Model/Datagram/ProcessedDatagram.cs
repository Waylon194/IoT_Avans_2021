using DsmrParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IWSN_Backend_Server.Model.Sensor
{
    /// <summary>
    /// Class which contains the processed (parsed/converted) (RawDatagram) model 
    /// </summary>
    public class ProcessedDatagram
    {
        public List<Telegram> ParsedTelegrams { get; set; }
        public string Signature { get; set; } // signature of the datagram
        public SObject S0 { get; set; } // (Currently) car-charger object 
        public SObject S1 { get; set; } // (Currently) solar-panel object
    }
}
