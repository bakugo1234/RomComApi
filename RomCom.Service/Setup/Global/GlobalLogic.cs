using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RomCom.Service.Data.Model.Contract;

namespace RomCom.Service.Setup.Global
{
    public class GlobalLogic
    {
        public IServiceResult BuildServiceResult(IServiceResult _serviceResultResponse, bool status)
        {
            _serviceResultResponse.Status = status;
            _serviceResultResponse.Message = status ? Global.Success : Global.Failed;
            _serviceResultResponse.StatusCode = status ? 200 : 501;
            return _serviceResultResponse;
        }

        public static string GetJsonAuditLogValues<T1, T2>(T1 newObject, T2 oldObject)
        {
            try
            {
                var changes = new List<AuditChange>();
                
                Type type1 = typeof(T1);
                Type type2 = typeof(T2);

                PropertyInfo[] properties1 = type1.GetProperties();
                PropertyInfo[] properties2 = type2.GetProperties();

                foreach (PropertyInfo prop1 in properties1)
                {
                    PropertyInfo prop2 = properties2.FirstOrDefault(p => p.Name == prop1.Name);
                    if (prop2 != null)
                    {
                        var value1 = prop1.GetValue(newObject);
                        var value2 = prop2.GetValue(oldObject);

                        if (!Equals(value1, value2))
                        {
                            changes.Add(new AuditChange
                            {
                                PropertyName = prop1.Name,
                                OldValue = value2?.ToString() ?? "null",
                                NewValue = value1?.ToString() ?? "null"
                            });
                        }
                    }
                }

                return JsonConvert.SerializeObject(changes);
            }
            catch (Exception)
            {
                return "[]";
            }
        }

        private class AuditChange
        {
            public string PropertyName { get; set; }
            public string OldValue { get; set; }
            public string NewValue { get; set; }
        }
    }
}

