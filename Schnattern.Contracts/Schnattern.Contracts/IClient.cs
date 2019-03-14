using System.Collections.Generic;
using System.IO;
using System.ServiceModel;

namespace Schnattern.Contracts
{
    [ServiceContract]
    public interface IClient
    {
        [OperationContract]
        void ShowText(string text);

        [OperationContract]
        void ShowImage(Stream image);

        [OperationContract]
        void ShowUsers(IEnumerable<string> users);
    }
}
