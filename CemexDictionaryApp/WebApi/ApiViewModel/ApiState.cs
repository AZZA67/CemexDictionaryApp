using CemexDictionaryApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CemexDictionaryApp.WebApi.ApiModels
{
    public class ApiState
    {
        public int StateId { get; set; }
        public string StateName { get; set; }
        public List<ApiZone> ZoneList { get; set; }
    }

    public class ApiZone
    {
        public int ZoneId { get; set; }
        public string ZoneName { get; set; }
    }

    public class StateMapping
    {
        public static  List<ApiState> Mapping (List<State> states)
        {
            if (states != null)
            {
                List<ApiState> _stateList = new();
                
                foreach (var item in states)
                {
                    ApiState p = new();
                    p.StateId = item.Id;
                    p.StateName = item.Name;
                    List<ApiZone> _zonesList = new();
                    foreach (var z in item.Zones)
                    {
                        ApiZone zone = new()
                        {
                            ZoneId = z.Id,
                            ZoneName = z.Name,
                        };
                        _zonesList.Add(zone);
                    }
                    p.ZoneList = _zonesList;
                    _stateList.Add(p);
                }
                return _stateList;
            }
            return null;
        }
    }
}
