using System.ServiceModel;

namespace wcf_chat
{
    class ServerUser
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public OperationContext operationcontext { get; set; }


    }
}
