using System;

namespace NLayerApp.BLL.Infrastructure
{
    public class OperationResult : Exception
    {

        public string Property { get; protected set; }
        public bool Status { get; protected set; }
        public OperationResult(bool status, string message, string property = null) : base(message)
        {
            Property = property;
            Status = status;
        }
    }
}