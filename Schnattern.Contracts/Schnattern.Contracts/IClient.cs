using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace Schnattern.Contracts
{
    [ServiceContract]
    public interface IClient
    {
        [OperationContract(IsOneWay = true)]
        void ShowText(string text);

        [OperationContract(IsOneWay = true)]
        void ShowImage(Stream image);

        [OperationContract(IsOneWay = true)]
        void ShowUsers(IEnumerable<string> users);

        [OperationContract(IsOneWay = true)]
        void LoginOk();

        [OperationContract(IsOneWay = true)]
        void LoginFailed(string msg);
    }
}
